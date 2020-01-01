using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Binger
{
	internal static class NativeMethods
	{
		const int SPI_SETDESKWALLPAPER = 20;
		const int SPIF_UPDATEINIFILE = 0x01;
		const int SPIF_SENDWININICHANGE = 0x02;

		public enum ProcessDpiAwareness
		{
			ProcessDpiUnaware = 0,
			ProcessSystemDpiAware = 1,
			ProcessPerMonitorDpiAware = 2
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		[DllImport("shcore.dll")]
		private static extern int SetProcessDpiAwareness(ProcessDpiAwareness value);

		public static bool EnableDpiAwareness()
		{
			try
			{
				if (Environment.OSVersion.Version.Major < 6)
				{
					return false;
				}

				SetProcessDpiAwareness(ProcessDpiAwareness.ProcessPerMonitorDpiAware);
				return true;
			}
			catch (Exception e1)
			{
				Console.WriteLine(e1);
				return false;
			}
		}

		public static bool SetSystemWallpaper(string wallpaperFilePath)
		{
			try
			{
				SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallpaperFilePath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
				return true;
			}
			catch
			{
				return false;
			}
		}

		// That string is encoded in Base64, without headers. There is a Windows API that encrypt binary arrays (the PIDL) to Base64, CryptBinaryToString. The dwFlags parameter should be set to CRYPT_STRING_BASE64
		// Our problem is the inverse, given a Base64 string (the encoded PIDL), get the decoded PIDL. There's an API for that, too!, CryptStringToBinary. The dwFlags parameter should also be set to CRYPT_STRING_BASE64.
		// I think it is not hard to write a small program that uses those API's (and some more to get a "readable" path from the PIDL); ask in the MSDN forums to get directions if you get stuck.
		// http://www.ms-windows.info/Help/windows-photo-screensaver-settings-19334.aspx
		// http://msdn.microsoft.com/en-us/library/aa379887(v=vs.85).aspx
		// BOOL WINAPI CryptBinaryToString(
		// _In_       const BYTE *pbBinary,
		// _In_       DWORD cbBinary,
		// _In_       DWORD dwFlags,
		// _Out_opt_  LPTSTR pszString,
		// _Inout_    DWORD *pcchString
		//);
		// http://msdn.microsoft.com/en-us/library/aa380285(v=vs.85).aspx
		// BOOL WINAPI CryptStringToBinary(
		// _In_     LPCTSTR pszString,
		// _In_     DWORD cchString,
		// _In_     DWORD dwFlags,
		// _In_     BYTE *pbBinary,
		// _Inout_  DWORD *pcbBinary,
		// _Out_    DWORD *pdwSkip,
		// _Out_    DWORD *pdwFlags
		//);


		[DllImport("shell32.dll")]
		public static extern Int32 SHGetDesktopFolder(out IShellFolder ppshf);

		[DllImport("shell32.dll")]
		public static extern bool SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);

		[DllImport("crypt32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptBinaryToString(IntPtr pcbBinary, int cbBinary, uint dwFlags,
			StringBuilder pszString, ref int pcchString);

		[DllImport("crypt32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptStringToBinary(string pszString, int cchString, uint dwFlags, IntPtr pbBinary,
			ref int pcbBinary, ref int pdwSkip, ref int pdwFlags);

		// interfaces
		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214E6-0000-0000-C000-000000000046")]
		public interface IShellFolder
		{
			Int32 ParseDisplayName(IntPtr hwnd, IntPtr pbc, String pszDisplayName, UInt32 pchEaten, out IntPtr ppidl,
				UInt32 pdwAttributes);

			Int32 EnumObjects(IntPtr hwnd, ESHCONTF grfFlags, out IntPtr ppenumIDList);
			Int32 BindToObject(IntPtr pidl, IntPtr pbc, [In] ref Guid riid, out IntPtr ppv);
			Int32 BindToStorage(IntPtr pidl, IntPtr pbc, [In] ref Guid riid, out IntPtr ppv);
			Int32 CompareIDs(Int32 lParam, IntPtr pidl1, IntPtr pidl2);
			Int32 CreateViewObject(IntPtr hwndOwner, [In] ref Guid riid, out IntPtr ppv);

			Int32 GetAttributesOf(UInt32 cidl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IntPtr[] apidl, ref ESFGAO rgfInOut);

			Int32 GetUIObjectOf(IntPtr hwndOwner, UInt32 cidl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] IntPtr[] apidl, [In] ref Guid riid, UInt32 rgfReserved, out IntPtr ppv);

			Int32 GetDisplayNameOf(IntPtr pidl, ESHGDN uFlags, out ESTRRET pName);
			Int32 SetNameOf(IntPtr hwnd, IntPtr pidl, String pszName, ESHCONTF uFlags, out IntPtr ppidlOut);
		}

		// Enumerations
		public enum ESHCONTF
		{
			SHCONTF_FOLDERS = 0x0020,
			SHCONTF_NONFOLDERS = 0x0040,
			SHCONTF_INCLUDEHIDDEN = 0x0080,
			SHCONTF_INIT_ON_FIRST_NEXT = 0x0100,
			SHCONTF_NETPRINTERSRCH = 0x0200,
			SHCONTF_SHAREABLE = 0x0400,
			SHCONTF_STORAGE = 0x0800
		}

		public enum ESFGAO : uint
		{
			SFGAO_CANCOPY = 0x00000001,
			SFGAO_CANMOVE = 0x00000002,
			SFGAO_CANLINK = 0x00000004,
			SFGAO_LINK = 0x00010000,
			SFGAO_SHARE = 0x00020000,
			SFGAO_READONLY = 0x00040000,
			SFGAO_HIDDEN = 0x00080000,
			SFGAO_FOLDER = 0x20000000,
			SFGAO_FILESYSTEM = 0x40000000,
			SFGAO_HASSUBFOLDER = 0x80000000,
		}

		public enum ESHGDN
		{
			SHGDN_NORMAL = 0x0000,
			SHGDN_INFOLDER = 0x0001,
			SHGDN_FOREDITING = 0x1000,
			SHGDN_FORADDRESSBAR = 0x4000,
			SHGDN_FORPARSING = 0x8000,
		}

		public enum ESTRRET : int
		{
			eeRRET_WSTR = 0x0000,
			STRRET_OFFSET = 0x0001,
			STRRET_CSTR = 0x0002
		}

		// Constants
		private const uint CRYPT_STRING_BASE64 = 1;

		//*****************************************************************************************************************************
		//*****************************************************************************************************************************

		public static string Encode(string myClearPath)
		{
			var encodedPath = "ERROR";
			var plainTextPath = myClearPath;


			if (SHGetDesktopFolder(out var folder) == 0)
			{
				folder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, plainTextPath, 0, out var pidl, 0);

				var k = 0;
				short cb = 0;

				var tempIntPtr = new IntPtr(pidl.ToInt64() + cb);
				while ((k = Marshal.ReadInt16(tempIntPtr)) > 0)
				{
					cb += (short)k;
					tempIntPtr = new IntPtr(pidl.ToInt64() + cb);
				}

				cb += 2;

				var sb = new StringBuilder();
				var largo = 0;

				CryptBinaryToString(pidl, cb, CRYPT_STRING_BASE64, null, ref largo);
				sb.Capacity = largo;

				if (CryptBinaryToString(pidl, cb, CRYPT_STRING_BASE64, sb, ref largo))
				{
					encodedPath = sb.ToString();
				}

				Marshal.FreeCoTaskMem(pidl);
			}

			return encodedPath;
		}

		//************************************************************************************************************************************

		public static string Decode(string myEncryptedPath)
		{
			string pathToDecode = "ERROR";
			var encryptedPath = myEncryptedPath;

			if (SHGetDesktopFolder(out IShellFolder folder) != 0) { return pathToDecode; }

			var a = 0;
			var b = 0;
			var largo = 0;

			CryptStringToBinary(encryptedPath, encryptedPath.Length, CRYPT_STRING_BASE64, IntPtr.Zero,
				ref largo, ref a, ref b);
			var pidl2 = Marshal.AllocCoTaskMem(largo);
			var sb = new StringBuilder();
			if (CryptStringToBinary(encryptedPath, encryptedPath.Length, CRYPT_STRING_BASE64, pidl2, ref largo, ref a, ref b))
			{
				sb.Length = 0;
				sb.Capacity = 261;
				SHGetPathFromIDList(pidl2, sb);
				pathToDecode = sb.ToString();
			}

			Marshal.FreeCoTaskMem(pidl2);

			return pathToDecode;
		}
	}
}