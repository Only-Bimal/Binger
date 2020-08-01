using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Binger.Properties;
using Microsoft.Win32;

namespace Binger
{
	public partial class BingerUI : Form
	{
		public BingerUI()
		{
			InitializeComponent();

			Settings.Default.Upgrade();
			Settings.Default.Reload();
			TrayIcon.Visible = true;
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			var result = FolderBrowser.ShowDialog();
			if (result != DialogResult.OK) { return; }

			var selectedFolder = FolderBrowser.SelectedPath;

			Settings.Default.FolderPath = selectedFolder;
			SaveToFolderTextBox.Text = selectedFolder;

			Settings.Default.Save();
		}

		private void EncodeButton_Click(object sender, EventArgs e)
		{
			// TODO: Check for the write access to the roaming profile path and ini file
			//Wallpaper.SetSlideShow(Settings.Default.FolderPath, 30);
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			Binger.Download(Settings.Default.FolderPath);
			//Binger.DownloadImage(Settings.Default.FolderPath);
		}

		private void DownloadTimer_Tick(object sender, EventArgs e)
		{
			Binger.DownloadImage(Settings.Default.FolderPath);
		}

		private void BingerUI_Resize(object sender, EventArgs e)
		{
			//if the form is minimized  
			//hide it from the task bar  
			if (WindowState != FormWindowState.Minimized) { return; }

			Hide();
		}

		private void TrayIcon_Click(object sender, EventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
			Focus();
			Activate();
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			if (Settings.Default.ExitToTray) { Hide(); }
			else { Close(); }
		}

		private void AutoStartCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			RegistryKey runKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

			Settings.Default.AutoStart = AutoStartCheckbox.Checked;
			if (Settings.Default.AutoStart)
			{

				var path = Assembly.GetExecutingAssembly().Location;

				runKey?.SetValue(@"Binger", path);
			}
			else
			{
				runKey?.DeleteValue(@"Binger", false);
			}
			Settings.Default.Save();
		}

		private void CheckPeriodicallyCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			SetDownloadSettings();
		}

		private void CloseToTrayCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ExitToTray = CloseToTrayCheckBox.Checked;
			Settings.Default.Save();
		}

		private void OpenMinimizedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.OpenHidden = OpenMinimizedCheckBox.Checked;
			Settings.Default.Save();
		}

		private void BingerUI_Shown(object sender, EventArgs e)
		{
			if (Settings.Default.OpenHidden)
			{
				Hide();
			}
		}

		private void DownloadDurationControl_ValueChanged(object sender, EventArgs e)
		{
			SetDownloadSettings();
		}

		private void SetDownloadSettings()
		{
			DownloadTimer.Enabled = CheckPeriodicallyCheckbox.Checked;
			Settings.Default.AutoStart = CheckPeriodicallyCheckbox.Checked;
			Settings.Default.AutoDownloadInterval = DownloadDurationControl.Value;

			if (CheckPeriodicallyCheckbox.Checked)
			{
				DownloadTimer.Interval = (int)Settings.Default.AutoDownloadInterval * 3600 * 1000;
			}
			Settings.Default.Save();
		}

		private void BingerUI_Load(object sender, EventArgs e)
		{
			Binger.UseHttps = Settings.Default.UseHttps;
			//Binger.Market = Settings.Default.Market;
			Binger.Country = Settings.Default.Country;
			Binger.UseCountry = Settings.Default.UseCountry;
		}

		private void OpenDestinationButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(Settings.Default.FolderPath))
			{
				MessageBox.Show("Please select a folder first", "No path to open", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Process.Start(Settings.Default.FolderPath);
		}
	}
}
