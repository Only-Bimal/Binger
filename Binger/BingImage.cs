using System;
using System.Globalization;

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
		public string Title { get; set; }
		public string Copyright { get; set; }
		public DateTime ImageDate { get; set; }
		public string Comment { get; set; }

		public static string Resolution => "1920x1200";

		//var bounds = Screen.PrimaryScreen.Bounds;
		//return bounds.Width + "x" + bounds.Height;
	}
}
