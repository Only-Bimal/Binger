using System;
using System.IO;
using System.Net;

namespace Binger
{
	class Binger
	{
		public static void DownloadImage(string imageFolder)
		{
			if (!Directory.Exists(imageFolder)) { Directory.CreateDirectory(imageFolder); }

			for (var i = 8; i >= 0; i -= 1)
			{
				var imageFileName = Path.ChangeExtension(Path.Combine(imageFolder, DateTime.Today.AddDays(-i).ToString("yyyy-MM-dd")), ".jpg");

				if (!File.Exists(imageFileName))
				{
					string response = null;
					Connect(ref response, i);
					ProcessXml(ref response);
					using (var client = new WebClient()) client.DownloadFile("http://www.bing.com" + response + "_1920x1200.jpg", imageFileName);
				}
			}
		}

		private static void Connect(ref string res, int i)
		{
			var request = (HttpWebRequest)WebRequest.Create("http://www.bing.com/hpimagearchive.aspx?format=xml&idx=" + i + "&n=1&mbl=1&mkt=en-ww");
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

		private static void ProcessXml(ref string xmlString)
		{
			using (var reader = System.Xml.XmlReader.Create(new StringReader(xmlString)))
			{
				reader.ReadToFollowing("urlBase");
				xmlString = reader.ReadElementContentAsString();
			}
		}
	}
}