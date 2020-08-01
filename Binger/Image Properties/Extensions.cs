using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binger
{
	/// <summary>
	/// Some of the EXIF values for setting. To expand use complete list of EXIF values http://www.exiv2.org/tags.html
	/// </summary>
	public enum MetaProperty
	{
		Title = 40091,
		Comment = 40092,
		Author = 40093,
		Keywords = 40094,
		Subject = 40095,
		Copyright = 33432,
		Software = 11,
		DateTime = 36867
	}

	public static class Extensions
	{
		public static Image SetMetaValue(this Image sourceBitmap, MetaProperty property, string value)
		{
			if (sourceBitmap == null) return null;
			var prop = sourceBitmap.PropertyItems[0];
			if (value != null)
			{
				var iLen = value.Length + 1;
				var bTxt = new byte[iLen];
				for (var i = 0; i < iLen - 1; i++) { bTxt[i] = (byte)value[i]; }
				bTxt[iLen - 1] = 0x00;
				prop.Id = (int)property;
				prop.Type = 2;
				prop.Value = bTxt;
				prop.Len = iLen;
			}

			sourceBitmap.SetPropertyItem(prop);

			return sourceBitmap;
		}

		/*
				public static string GetMetaValue(this Image sourceBitmap, MetaProperty property)
				{
					if (sourceBitmap != null)
					{
						var propItems = sourceBitmap.PropertyItems;
						var prop = propItems.FirstOrDefault(p => p.Id == (int)property);
						return prop != null ? Encoding.UTF8.GetString(prop.Value) : null;
					}
				}
		*/

	}

}
