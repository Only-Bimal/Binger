using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Binger
{

	public static class Wallpaper
	{
		public enum Style : int
		{
			Fill,
			Fit,
			Span,
			Stretch,
			Tile,
			Center
		}

		public static void Set(Uri uri, Style style)
		{
			var s = new System.Net.WebClient().OpenRead(uri.ToString());

			var img = System.Drawing.Image.FromStream(s);
			var tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
			img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

			Set(tempPath, style);
		}

		public static void Set(string imageFilePath, Style style)
		{
			var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
			if (key == null)
			{
				throw new UnauthorizedAccessException("Unable to open the registry key for writing. Please make sure you have access to write the registry.");
			}

			switch (style)
			{
				case Style.Fill:
					key.SetValue(@"WallpaperStyle", 10.ToString());
					key.SetValue(@"TileWallpaper", 0.ToString());
					break;
				case Style.Fit:
					key.SetValue(@"WallpaperStyle", 6.ToString());
					key.SetValue(@"TileWallpaper", 0.ToString());
					break;
				// Windows 8 or newer only!
				case Style.Span:
					key.SetValue(@"WallpaperStyle", 22.ToString());
					key.SetValue(@"TileWallpaper", 0.ToString());
					break;
				case Style.Stretch:
					key.SetValue(@"WallpaperStyle", 2.ToString());
					key.SetValue(@"TileWallpaper", 0.ToString());
					break;
				case Style.Tile:
					key.SetValue(@"WallpaperStyle", 0.ToString());
					key.SetValue(@"TileWallpaper", 1.ToString());
					break;
				case Style.Center:
					key.SetValue(@"WallpaperStyle", 0.ToString());
					key.SetValue(@"TileWallpaper", 0.ToString());
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
			}

			NativeMethods.SetSystemWallpaper(imageFilePath);
		}
		public static void SetSlideShow(string path, int interval)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

			var encodedPath = NativeMethods.Encode(path);
			var content = "[Slideshow]\r\nImagesRootPIDL=" + encodedPath;

			var iniPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Themes\slideshow.ini");

			File.WriteAllText(iniPath, content);
			using (var file = File.CreateText(iniPath))
			{
				file.Write(content);
				file.Close();
			}

			var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Personalization\Desktop Slideshow", true);
			if (key == null)
			{
				throw new UnauthorizedAccessException("Unable to open the registry key for writing. Please make sure you have access to write the registry.");
			}

			key.SetValue(@"Interval", interval);
		}
	}
}