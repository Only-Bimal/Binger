using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//#That string is encoded in Base64, without headers. There is a Windows API that encrypt binary arrays (the PIDL) to Base64, CryptBinaryToString. The dwFlags parameter should be set to CRYPT_STRING_BASE64.
//#Our problem is the inverse, given a Base64 string (the encoded PIDL), get the decoded PIDL. There's an API for that, too!, CryptStringToBinary. The dwFlags parameter should also be set to CRYPT_STRING_BASE64.
//#I think it is not hard to write a small program that uses those API's (and some more to get a "readable" path from the PIDL); ask in the MSDN forums to get directions if you get stuck.

//#http://www.ms-windows.info/Help/windows-photo-screensaver-settings-19334.aspx

//#http://msdn.microsoft.com/en-us/library/aa379887(v=vs.85).aspx
//#BOOL WINAPI CryptBinaryToString(
//#  _In_       const BYTE *pbBinary,
//#  _In_       DWORD cbBinary,
//#  _In_       DWORD dwFlags,
//#  _Out_opt_  LPTSTR pszString,
//#  _Inout_    DWORD *pcchString
//#);
//#http://msdn.microsoft.com/en-us/library/aa380285(v=vs.85).aspx
//#BOOL WINAPI CryptStringToBinary(
//#  _In_     LPCTSTR pszString,
//#  _In_     DWORD cchString,
//#  _In_     DWORD dwFlags,
//#  _In_     BYTE *pbBinary,
//#  _Inout_  DWORD *pcbBinary,
//#  _Out_    DWORD *pdwSkip,
//#  _Out_    DWORD *pdwFlags
//#);

namespace Binger
{
	internal class Pidl
	{
		#region Enumerations

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

		// constantes
		private const uint CRYPT_STRING_BASE64 = 1;

		#endregion

		#region Function Impports

		[DllImport("shell32.dll")]
		public static extern Int32 SHGetDesktopFolder(out IShellFolder ppshf);

		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		public static extern bool SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);

		[DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptBinaryToString(IntPtr pcbBinary, int cbBinary, uint dwFlags, StringBuilder pszString, ref int pcchString);

		[DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptStringToBinary(string pszString, int cchString, uint dwFlags, IntPtr pbBinary, ref int pcbBinary, ref int pdwSkip, ref int pdwFlags);

		#endregion

		#region Interfaces

		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214E6-0000-0000-C000-000000000046")]
		public interface IShellFolder
		{
			Int32 ParseDisplayName(IntPtr hwnd, IntPtr pbc, String pszDisplayName, UInt32 pchEaten, out IntPtr ppidl, UInt32 pdwAttributes);
			Int32 EnumObjects(IntPtr hwnd, ESHCONTF grfFlags, out IntPtr ppenumIDList);
			Int32 BindToObject(IntPtr pidl, IntPtr pbc, [In]ref Guid riid, out IntPtr ppv);
			Int32 BindToStorage(IntPtr pidl, IntPtr pbc, [In]ref Guid riid, out IntPtr ppv);
			Int32 CompareIDs(Int32 lParam, IntPtr pidl1, IntPtr pidl2);
			Int32 CreateViewObject(IntPtr hwndOwner, [In] ref Guid riid, out IntPtr ppv);
			Int32 GetAttributesOf(UInt32 cidl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]IntPtr[] apidl, ref ESFGAO rgfInOut);
			Int32 GetUIObjectOf(IntPtr hwndOwner, UInt32 cidl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]IntPtr[] apidl, [In] ref Guid riid, UInt32 rgfReserved, out IntPtr ppv);
			Int32 GetDisplayNameOf(IntPtr pidl, ESHGDN uFlags, out ESTRRET pName);
			Int32 SetNameOf(IntPtr hwnd, IntPtr pidl, String pszName, ESHCONTF uFlags, out IntPtr ppidlOut);
		}

		#endregion

		#region Public Functions

		public static string Encode(string myClearPath)
		{
			IShellFolder folder;
			IntPtr pidl;
			string Pathcodificado;
			Pathcodificado = "ERROR";
			string myPathTextoPlano = myClearPath;
			//string myPathTextoPlano = @"c:\flashtool"; // Regex.Escape(myClearPath);


			// este es el folder del escritorio (raíz del espacio de nombres del shell)
			if (SHGetDesktopFolder(out folder) == 0)
			{
				// pidl del archivo
				//folder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, @"C:\walter\yo.png", 0, out pidl, 0);
				folder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, myPathTextoPlano, 0, out pidl, 0);

				// parseo el pidl para obtener sus tamaño
				int k = 0;
				short cb = 0;

				// ONLY WORKS WITH .NET FRAMEWORK 4	
				//  while ((k = Marshal.ReadInt16(pidl + cb)) > 0)
				//		
				//  {
				//      cb += (short)k;
				//  }

				IntPtr tempIntPtr = new IntPtr(pidl.ToInt64() + cb);
				while ((k = Marshal.ReadInt16(tempIntPtr)) > 0)
				{
					cb += (short)k;
					tempIntPtr = new IntPtr(pidl.ToInt64() + cb);
				}

				cb += 2;

				// encripto el pidl
				StringBuilder sb = new StringBuilder();
				int largo = 0;

				CryptBinaryToString(pidl, cb, CRYPT_STRING_BASE64, null, ref largo);
				sb.Capacity = largo;

				if (CryptBinaryToString(pidl, cb, CRYPT_STRING_BASE64, sb, ref largo))
				{
					//Console.WriteLine(sb.ToString());
					Pathcodificado = sb.ToString();
				}
				Marshal.FreeCoTaskMem(pidl);
			}

			return Pathcodificado;
		}

		public static string Decode(string myEncryptedPath)
		{
			IShellFolder folder;
			// IntPtr pidl;
			string PathDescodificado;
			PathDescodificado = "ERROR";
			string mypathEncriptado = myEncryptedPath; // Regex.Escape(myEncryptedPath);

			// este es el folder del escritorio (raíz del espacio de nombres del shell)
			if (SHGetDesktopFolder(out folder) == 0)
			{

				// Desencripto 
				int a = 0;
				int b = 0;
				int largo = 0;


				CryptStringToBinary(mypathEncriptado, mypathEncriptado.Length, CRYPT_STRING_BASE64, IntPtr.Zero, ref largo, ref a, ref b);
				// recreo el objeto
				IntPtr pidl2 = Marshal.AllocCoTaskMem(largo);
				//if (CryptStringToBinary(sb.ToString(), sb.Length, CRYPT_STRING_BASE64, pidl2, ref largo, ref a, ref b))
				StringBuilder sb = new StringBuilder();
				if (CryptStringToBinary(mypathEncriptado, mypathEncriptado.Length, CRYPT_STRING_BASE64, pidl2, ref largo, ref a, ref b))
				{
					// muestro el path proveyendo el pidl reconstruído
					//sb.Clear(); Only works with .NET 4
					sb.Length = 0;
					sb.Capacity = 261;
					SHGetPathFromIDList(pidl2, sb);
					//Console.WriteLine(sb.ToString());
					PathDescodificado = sb.ToString();
				}

				//
				//Marshal.FreeCoTaskMem(pidl);
				Marshal.FreeCoTaskMem(pidl2);
			}
			return PathDescodificado;

		}
		#endregion
	}
}