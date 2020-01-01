using System;
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
			Hide();
		}

		private void BingerUI_Load(object sender, EventArgs e)
		{
			//SaveToFolderTextBox.Text = Settings.Default.FolderPath;
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

		private void SaveButton_Click(object sender, EventArgs e)
		{
			Settings.Default.Save();
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			Binger.DownloadImage(Settings.Default.FolderPath);
		}

		private void DownloadTimer_Tick(object sender, EventArgs e)
		{
			Binger.DownloadImage(Settings.Default.FolderPath);
		}

		private void BingerUI_Resize(object sender, EventArgs e)
		{
			//if the form is minimized  
			//hide it from the task bar  
			//and show the system tray icon (represented by the NotifyIcon control)  
			if (WindowState == FormWindowState.Minimized)
			{
				Hide();
				TrayIcon.Visible = true;
			}
		}

		private void TrayIcon_Click(object sender, EventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			if (Settings.Default.ExitToTray)
			{
				Hide();
			}
			else
			{
				Close();
			}
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
			DownloadTimer.Enabled = CheckPeriodicallyCheckbox.Checked;
			Settings.Default.AutoStart = CheckPeriodicallyCheckbox.Checked;

			if (CheckPeriodicallyCheckbox.Checked)
			{
				DownloadTimer.Interval = (int)DownloadDurationControl.Value * 3600 * 1000;
			}
			Settings.Default.Save();
			}

		private void CloseToTrayCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ExitToTray = CloseToTrayCheckBox.Checked;
			Settings.Default.Save();
		}
	}
}
