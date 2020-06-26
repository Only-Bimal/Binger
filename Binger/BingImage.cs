using System;
using System.Globalization;
using System.IO;
using System.Net;

namespace Binger
{
	public class BingImage
	{
		private Uri _imageUrl;
		public string Url
		{
			get => _imageUrl.ToString();
			set => _imageUrl = new Uri(string.Format(CultureInfo.InvariantCulture, "{0}{1}_{2}.jpg", "http://www.bing.com", value, Resolution));
		}

		public string ImageFileName => string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}.jpeg", ImageDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), Country, Resolution);

		public string Title { get; set; }
		public string Copyright { get; set; }
		public string Country { get; set; }
		public DateTime ImageDate { get; set; }
		public string Comment { get; set; }

		public static string Resolution => "1920x1200";
		public const string RootUrl = "https://www.bing.com/hpimagearchive.aspx?format=xml&n=1&mbl=1";

		public void Download(string imageFolder)
		{
			for (var i = 0; i < 9; i++)
			{
				try
				{
					GetXml(i);
					// Get Success, Download the image
					var imageFileName = Path.Combine(imageFolder, ImageFileName);

					if (File.Exists(imageFileName)) { continue; }

					using (var client = new WebClient())
					{
						client.DownloadFile(Url, imageFileName);
					}
				}
				catch { /*Ignore*/ }
			}

		}

		private void GetXml(int count)
		{
			var append = string.Format(CultureInfo.InvariantCulture, "&idx={0}&cc={1}", count, Country);

			var uri = new Uri(string.Format(CultureInfo.InvariantCulture, "{0}{1}", RootUrl, append));

			//var request = (HttpWebRequest)WebRequest.Create("http://www.bing.com/hpimagearchive.aspx?format=xml&idx=" + i + "&n=1&mbl=1&mkt=en-ww");
			var request = (HttpWebRequest)WebRequest.Create(uri);

			request.KeepAlive = false;
			request.Method = "GET";

			using (var response = (HttpWebResponse)request.GetResponse())
			{
				using (var loResponseStream = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("No response from web.")))
				{
					GetImageData(loResponseStream.ReadToEnd());
				}
			}
		}

		private void GetImageData(string xmlString)
		{
			var data = new System.Xml.XmlDocument();
			data.LoadXml(xmlString);

			var imageDta = data.GetElementsByTagName("image");
			foreach (System.Xml.XmlNode node in imageDta[0].ChildNodes)
			{
				var value = node.InnerText;
				switch (node.Name.ToUpperInvariant())
				{
					case "STARTDATE":
						var year = Convert.ToInt32(value.Substring(0, 4), CultureInfo.InvariantCulture);
						var mon = Convert.ToInt32(value.Substring(4, 2), CultureInfo.InvariantCulture);
						var date = Convert.ToInt32(value.Substring(6, 2), CultureInfo.InvariantCulture);

						ImageDate = new DateTime(year, mon, date);
						break;
					case "URLBASE":
						Url = value;
						break;
					case "COPYRIGHT":
						Copyright = value;
						break;
					case "HEADLINE":
						Title = value;
						break;
					case "COPYRIGHTLINK":
						Comment = value;
						break;
				}
			}
		}
	}
}
