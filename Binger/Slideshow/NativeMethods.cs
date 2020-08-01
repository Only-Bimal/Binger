using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Binger
{
	 internal partial class NativeMethods
	{
		#region Enumerations

		public enum ProcessDpiAwareness
		{
			ProcessDpiUnaware = 0,
			ProcessSystemDpiAware = 1,
			ProcessPerMonitorDpiAware = 2
		}

		#endregion
		#region Imports

		[DllImport("shcore.dll")]
		private static extern int SetProcessDpiAwareness(ProcessDpiAwareness value);

		#endregion

		public static bool EnableDpiAwareness()
		{
			try
			{
				if (Environment.OSVersion.Version.Major < 6) { return false; }

				_ = SetProcessDpiAwareness(ProcessDpiAwareness.ProcessPerMonitorDpiAware);
				return true;
			}
			catch (Exception e1)
			{
				Console.WriteLine(e1);
				return false;
			}
		}
	}
}
