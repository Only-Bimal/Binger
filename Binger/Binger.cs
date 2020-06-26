using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Binger.Properties;

namespace Binger
{
	static class Binger
	{
		public static readonly List<KeyValuePair<string, string>> Countries = new List<KeyValuePair<string, string>>();
		public static readonly List<KeyValuePair<string, string>> Markets = new List<KeyValuePair<string, string>>();

		static Binger()
		{
			Countries.Add(new KeyValuePair<string, string>("Auto", "auto"));
			Countries.Add(new KeyValuePair<string, string>("Australia", "au"));
			Countries.Add(new KeyValuePair<string, string>("Brazil", "br"));
			Countries.Add(new KeyValuePair<string, string>("Canada", "ca"));
			Countries.Add(new KeyValuePair<string, string>("China", "cn"));
			Countries.Add(new KeyValuePair<string, string>("India", "in"));
			Countries.Add(new KeyValuePair<string, string>("Germany", "de"));
			Countries.Add(new KeyValuePair<string, string>("France", "fr"));
			Countries.Add(new KeyValuePair<string, string>("Japan", "jp"));
			Countries.Add(new KeyValuePair<string, string>("New Zealand", "nz"));
			Countries.Add(new KeyValuePair<string, string>("United States", "us"));
			Countries.Add(new KeyValuePair<string, string>("United Kingdom", "uk"));

			Markets.Add(new KeyValuePair<string, string>("Global", "en-WW"));
			Markets.Add(new KeyValuePair<string, string>("United States", "en-US"));
			Markets.Add(new KeyValuePair<string, string>("China", "zh-CN"));
			Markets.Add(new KeyValuePair<string, string>("Japan", "jp-JP"));

		}

		#region Properties
		public static bool UseHttps { get; set; }

		public static string Country { get; set; }
		public static bool UseCountry { get; set; }

		#endregion

		private static void Download()
		{
			foreach (var image in Countries.Select(country => new BingImage { Country = country.Value }))
			{
				image.Download(Settings.Default.FolderPath);
			}
		}

		public static void DownloadImage(string imageFolder)
		{
			if (!Directory.Exists(imageFolder)) { Directory.CreateDirectory(imageFolder); }

			for (var i = 9; i >= 0; i -= 1)
			{
				string response = null;
				Connect(ref response, i);
				var image = GetImageData(response);

				//ProcessXml(ref response);
				//using (var client = new WebClient()) client.DownloadFile("http://www.bing.com" + response + "_1920x1200.jpg", imageFileName);
				//using (var client = new WebClient()) client.DownloadFile(image.Url, imageFileName);

				var imageFileName = Path.Combine(imageFolder, image.ImageFileName);

				if (!File.Exists(imageFileName))
				{
					using (var client = new WebClient())
					{
						client.DownloadFile(image.Url, imageFileName);
						SetProperties(image, imageFileName);
					}
				}
			}
		}

		private static void SetProperties(BingImage image, string imageFileName)
		{
			using (var imageFile = Image.FromFile(imageFileName))
			{
				imageFile.SetMetaValue(MetaProperty.Copyright, image.Copyright);
				imageFile.SetMetaValue(MetaProperty.Comment, image.Comment);
				imageFile.SetMetaValue(MetaProperty.DateTime, image.ImageDate.ToString(CultureInfo.InvariantCulture));
				imageFile.SetMetaValue(MetaProperty.Title, image.Title);
				imageFile.SetMetaValue(MetaProperty.Keywords, image.Copyright);
			}
		}

		private static void Connect(ref string res, int i)
		{
			var baseUrl = string.Format(CultureInfo.InvariantCulture, "{0}://www.bing.com/hpimagearchive.aspx?format=xml&idx={1}&n=1&mbl=1", UseHttps ? "https" : "http", i);
			var append = UseCountry ? "&cc=" + Country : "&mkt=en-ww";

			var uri = new Uri(string.Format(CultureInfo.InvariantCulture, "{0}{1}", baseUrl, append));

			//var request = (HttpWebRequest)WebRequest.Create("http://www.bing.com/hpimagearchive.aspx?format=xml&idx=" + i + "&n=1&mbl=1&mkt=en-ww");
			var request = (HttpWebRequest)WebRequest.Create(uri);

			request.KeepAlive = false;
			request.Method = "GET";
			using (var response = (HttpWebResponse)request.GetResponse())
			{
				using (var loResponseStream = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("No response from web.")))
				{
					res = loResponseStream.ReadToEnd();
				}
			}
		}

		/*
				private static void ProcessXml(ref string xmlString)
				{
					using (var reader = System.Xml.XmlReader.Create(new StringReader(xmlString)))
					{
						reader.ReadToFollowing("urlBase");
						xmlString = reader.ReadElementContentAsString();
					}
				}
		*/

		private static BingImage GetImageData(string xmlString)
		{
			var newImage = new BingImage();

			System.Xml.XmlDocument data = new System.Xml.XmlDocument();
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

						newImage.ImageDate = new DateTime(year, mon, date);
						break;
					case "URLBASE":
						newImage.Url = value;
						break;
					case "COPYRIGHT":
						newImage.Copyright = value;
						break;
					case "HEADLINE":
						newImage.Title = value;
						break;
					case "COPYRIGHTLINK":
						newImage.Comment = value;
						break;
				}
			}
			return newImage;
		}
	}
}
