namespace Binger
{
	partial class BingerUI
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BingerUI));
			this.SaveToLabel = new System.Windows.Forms.Label();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.FolderGroupBox = new System.Windows.Forms.GroupBox();
			this.OpenDestinationButton = new System.Windows.Forms.Button();
			this.SaveToFolderTextBox = new System.Windows.Forms.TextBox();
			this.EncodeButton = new System.Windows.Forms.Button();
			this.DownloadButton = new System.Windows.Forms.Button();
			this.SetAsSlideshowButton = new System.Windows.Forms.Button();
			this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.DownloadTimer = new System.Windows.Forms.Timer(this.components);
			this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.ContainerMain = new System.Windows.Forms.SplitContainer();
			this.CloseOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.OpenMinimizedCheckBox = new System.Windows.Forms.CheckBox();
			this.CloseToTrayCheckBox = new System.Windows.Forms.CheckBox();
			this.StartupGroupbox = new System.Windows.Forms.GroupBox();
			this.HoursLabel = new System.Windows.Forms.Label();
			this.DownloadDurationControl = new System.Windows.Forms.NumericUpDown();
			this.CheckPeriodicallyCheckbox = new System.Windows.Forms.CheckBox();
			this.AutoStartCheckbox = new System.Windows.Forms.CheckBox();
			this.CloseButton = new System.Windows.Forms.Button();
			this.FolderGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ContainerMain)).BeginInit();
			this.ContainerMain.Panel1.SuspendLayout();
			this.ContainerMain.Panel2.SuspendLayout();
			this.ContainerMain.SuspendLayout();
			this.CloseOptionsGroupBox.SuspendLayout();
			this.StartupGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DownloadDurationControl)).BeginInit();
			this.SuspendLayout();
			// 
			// SaveToLabel
			// 
			this.SaveToLabel.AutoSize = true;
			this.SaveToLabel.Location = new System.Drawing.Point(8, 28);
			this.SaveToLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.SaveToLabel.Name = "SaveToLabel";
			this.SaveToLabel.Size = new System.Drawing.Size(82, 15);
			this.SaveToLabel.TabIndex = 0;
			this.SaveToLabel.Text = "Save To &Folder";
			// 
			// BrowseButton
			// 
			this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BrowseButton.Location = new System.Drawing.Point(493, 22);
			this.BrowseButton.Margin = new System.Windows.Forms.Padding(2);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(72, 27);
			this.BrowseButton.TabIndex = 2;
			this.BrowseButton.Text = "&Browse...";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// FolderGroupBox
			// 
			this.FolderGroupBox.Controls.Add(this.OpenDestinationButton);
			this.FolderGroupBox.Controls.Add(this.SaveToLabel);
			this.FolderGroupBox.Controls.Add(this.BrowseButton);
			this.FolderGroupBox.Controls.Add(this.SaveToFolderTextBox);
			this.FolderGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.FolderGroupBox.Location = new System.Drawing.Point(0, 0);
			this.FolderGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.FolderGroupBox.Name = "FolderGroupBox";
			this.FolderGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.FolderGroupBox.Size = new System.Drawing.Size(654, 63);
			this.FolderGroupBox.TabIndex = 3;
			this.FolderGroupBox.TabStop = false;
			this.FolderGroupBox.Text = "Location to save Images";
			// 
			// OpenDestinationButton
			// 
			this.OpenDestinationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.OpenDestinationButton.Location = new System.Drawing.Point(571, 22);
			this.OpenDestinationButton.Margin = new System.Windows.Forms.Padding(2);
			this.OpenDestinationButton.Name = "OpenDestinationButton";
			this.OpenDestinationButton.Size = new System.Drawing.Size(72, 27);
			this.OpenDestinationButton.TabIndex = 3;
			this.OpenDestinationButton.Text = "&Open...";
			this.OpenDestinationButton.UseVisualStyleBackColor = true;
			this.OpenDestinationButton.Click += new System.EventHandler(this.OpenDestinationButton_Click);
			// 
			// SaveToFolderTextBox
			// 
			this.SaveToFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SaveToFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.SaveToFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
			this.SaveToFolderTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Binger.Properties.Settings.Default, "FolderPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.SaveToFolderTextBox.Location = new System.Drawing.Point(96, 24);
			this.SaveToFolderTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.SaveToFolderTextBox.Name = "SaveToFolderTextBox";
			this.SaveToFolderTextBox.Size = new System.Drawing.Size(391, 23);
			this.SaveToFolderTextBox.TabIndex = 1;
			this.SaveToFolderTextBox.Text = global::Binger.Properties.Settings.Default.FolderPath;
			// 
			// EncodeButton
			// 
			this.EncodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.EncodeButton.Location = new System.Drawing.Point(493, 17);
			this.EncodeButton.Margin = new System.Windows.Forms.Padding(2);
			this.EncodeButton.Name = "EncodeButton";
			this.EncodeButton.Size = new System.Drawing.Size(72, 27);
			this.EncodeButton.TabIndex = 6;
			this.EncodeButton.Text = "&Save";
			this.EncodeButton.UseVisualStyleBackColor = true;
			this.EncodeButton.Visible = false;
			this.EncodeButton.Click += new System.EventHandler(this.EncodeButton_Click);
			// 
			// DownloadButton
			// 
			this.DownloadButton.Location = new System.Drawing.Point(369, 43);
			this.DownloadButton.Margin = new System.Windows.Forms.Padding(2);
			this.DownloadButton.Name = "DownloadButton";
			this.DownloadButton.Size = new System.Drawing.Size(104, 27);
			this.DownloadButton.TabIndex = 4;
			this.DownloadButton.Text = "&Download Now";
			this.DownloadButton.UseVisualStyleBackColor = true;
			this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
			// 
			// SetAsSlideshowButton
			// 
			this.SetAsSlideshowButton.Enabled = false;
			this.SetAsSlideshowButton.Location = new System.Drawing.Point(357, 138);
			this.SetAsSlideshowButton.Margin = new System.Windows.Forms.Padding(2);
			this.SetAsSlideshowButton.Name = "SetAsSlideshowButton";
			this.SetAsSlideshowButton.Size = new System.Drawing.Size(70, 22);
			this.SetAsSlideshowButton.TabIndex = 5;
			this.SetAsSlideshowButton.Text = "Set Slidesho&w";
			this.SetAsSlideshowButton.UseVisualStyleBackColor = true;
			// 
			// DownloadTimer
			// 
			this.DownloadTimer.Interval = 10800;
			this.DownloadTimer.Tick += new System.EventHandler(this.DownloadTimer_Tick);
			// 
			// TrayIcon
			// 
			this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
			this.TrayIcon.Text = "Binger";
			this.TrayIcon.Visible = true;
			this.TrayIcon.Click += new System.EventHandler(this.TrayIcon_Click);
			// 
			// ContainerMain
			// 
			this.ContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ContainerMain.IsSplitterFixed = true;
			this.ContainerMain.Location = new System.Drawing.Point(0, 0);
			this.ContainerMain.Margin = new System.Windows.Forms.Padding(2);
			this.ContainerMain.Name = "ContainerMain";
			this.ContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// ContainerMain.Panel1
			// 
			this.ContainerMain.Panel1.Controls.Add(this.CloseOptionsGroupBox);
			this.ContainerMain.Panel1.Controls.Add(this.StartupGroupbox);
			this.ContainerMain.Panel1.Controls.Add(this.FolderGroupBox);
			// 
			// ContainerMain.Panel2
			// 
			this.ContainerMain.Panel2.Controls.Add(this.CloseButton);
			this.ContainerMain.Panel2.Controls.Add(this.EncodeButton);
			this.ContainerMain.Panel2.Controls.Add(this.SetAsSlideshowButton);
			this.ContainerMain.Size = new System.Drawing.Size(654, 322);
			this.ContainerMain.SplitterDistance = 265;
			this.ContainerMain.SplitterWidth = 3;
			this.ContainerMain.TabIndex = 7;
			// 
			// CloseOptionsGroupBox
			// 
			this.CloseOptionsGroupBox.Controls.Add(this.OpenMinimizedCheckBox);
			this.CloseOptionsGroupBox.Controls.Add(this.CloseToTrayCheckBox);
			this.CloseOptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.CloseOptionsGroupBox.Location = new System.Drawing.Point(0, 153);
			this.CloseOptionsGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.CloseOptionsGroupBox.Name = "CloseOptionsGroupBox";
			this.CloseOptionsGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.CloseOptionsGroupBox.Size = new System.Drawing.Size(654, 54);
			this.CloseOptionsGroupBox.TabIndex = 6;
			this.CloseOptionsGroupBox.TabStop = false;
			// 
			// OpenMinimizedCheckBox
			// 
			this.OpenMinimizedCheckBox.AutoSize = true;
			this.OpenMinimizedCheckBox.Checked = global::Binger.Properties.Settings.Default.OpenHidden;
			this.OpenMinimizedCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Binger.Properties.Settings.Default, "OpenHidden", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.OpenMinimizedCheckBox.Location = new System.Drawing.Point(295, 21);
			this.OpenMinimizedCheckBox.Margin = new System.Windows.Forms.Padding(2);
			this.OpenMinimizedCheckBox.Name = "OpenMinimizedCheckBox";
			this.OpenMinimizedCheckBox.Size = new System.Drawing.Size(114, 19);
			this.OpenMinimizedCheckBox.TabIndex = 1;
			this.OpenMinimizedCheckBox.Text = "Open Minimi&zed";
			this.OpenMinimizedCheckBox.UseVisualStyleBackColor = true;
			this.OpenMinimizedCheckBox.CheckedChanged += new System.EventHandler(this.OpenMinimizedCheckBox_CheckedChanged);
			// 
			// CloseToTrayCheckBox
			// 
			this.CloseToTrayCheckBox.AutoSize = true;
			this.CloseToTrayCheckBox.Checked = global::Binger.Properties.Settings.Default.ExitToTray;
			this.CloseToTrayCheckBox.Location = new System.Drawing.Point(12, 21);
			this.CloseToTrayCheckBox.Margin = new System.Windows.Forms.Padding(2);
			this.CloseToTrayCheckBox.Name = "CloseToTrayCheckBox";
			this.CloseToTrayCheckBox.Size = new System.Drawing.Size(223, 19);
			this.CloseToTrayCheckBox.TabIndex = 0;
			this.CloseToTrayCheckBox.Text = "Closing application minimizes to &Tray";
			this.CloseToTrayCheckBox.UseVisualStyleBackColor = true;
			this.CloseToTrayCheckBox.CheckedChanged += new System.EventHandler(this.CloseToTrayCheckBox_CheckedChanged);
			// 
			// StartupGroupbox
			// 
			this.StartupGroupbox.Controls.Add(this.HoursLabel);
			this.StartupGroupbox.Controls.Add(this.DownloadDurationControl);
			this.StartupGroupbox.Controls.Add(this.DownloadButton);
			this.StartupGroupbox.Controls.Add(this.CheckPeriodicallyCheckbox);
			this.StartupGroupbox.Controls.Add(this.AutoStartCheckbox);
			this.StartupGroupbox.Dock = System.Windows.Forms.DockStyle.Top;
			this.StartupGroupbox.Location = new System.Drawing.Point(0, 63);
			this.StartupGroupbox.Margin = new System.Windows.Forms.Padding(2);
			this.StartupGroupbox.Name = "StartupGroupbox";
			this.StartupGroupbox.Padding = new System.Windows.Forms.Padding(2);
			this.StartupGroupbox.Size = new System.Drawing.Size(654, 90);
			this.StartupGroupbox.TabIndex = 4;
			this.StartupGroupbox.TabStop = false;
			// 
			// HoursLabel
			// 
			this.HoursLabel.AutoSize = true;
			this.HoursLabel.Location = new System.Drawing.Point(306, 49);
			this.HoursLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.HoursLabel.Name = "HoursLabel";
			this.HoursLabel.Size = new System.Drawing.Size(39, 15);
			this.HoursLabel.TabIndex = 3;
			this.HoursLabel.Text = "Hours";
			// 
			// DownloadDurationControl
			// 
			this.DownloadDurationControl.Location = new System.Drawing.Point(266, 47);
			this.DownloadDurationControl.Margin = new System.Windows.Forms.Padding(2);
			this.DownloadDurationControl.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
			this.DownloadDurationControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DownloadDurationControl.Name = "DownloadDurationControl";
			this.DownloadDurationControl.Size = new System.Drawing.Size(36, 23);
			this.DownloadDurationControl.TabIndex = 2;
			this.DownloadDurationControl.Value = global::Binger.Properties.Settings.Default.AutoDownloadInterval;
			this.DownloadDurationControl.ValueChanged += new System.EventHandler(this.DownloadDurationControl_ValueChanged);
			// 
			// CheckPeriodicallyCheckbox
			// 
			this.CheckPeriodicallyCheckbox.AutoSize = true;
			this.CheckPeriodicallyCheckbox.Checked = global::Binger.Properties.Settings.Default.AutoDownload;
			this.CheckPeriodicallyCheckbox.Location = new System.Drawing.Point(12, 48);
			this.CheckPeriodicallyCheckbox.Margin = new System.Windows.Forms.Padding(2);
			this.CheckPeriodicallyCheckbox.Name = "CheckPeriodicallyCheckbox";
			this.CheckPeriodicallyCheckbox.Size = new System.Drawing.Size(240, 19);
			this.CheckPeriodicallyCheckbox.TabIndex = 1;
			this.CheckPeriodicallyCheckbox.Text = "Check for New Images &Periodically every";
			this.CheckPeriodicallyCheckbox.UseVisualStyleBackColor = true;
			this.CheckPeriodicallyCheckbox.CheckedChanged += new System.EventHandler(this.CheckPeriodicallyCheckbox_CheckedChanged);
			// 
			// AutoStartCheckbox
			// 
			this.AutoStartCheckbox.AutoSize = true;
			this.AutoStartCheckbox.Checked = global::Binger.Properties.Settings.Default.AutoStart;
			this.AutoStartCheckbox.Location = new System.Drawing.Point(12, 24);
			this.AutoStartCheckbox.Margin = new System.Windows.Forms.Padding(2);
			this.AutoStartCheckbox.Name = "AutoStartCheckbox";
			this.AutoStartCheckbox.Size = new System.Drawing.Size(310, 19);
			this.AutoStartCheckbox.TabIndex = 0;
			this.AutoStartCheckbox.Text = "&Automatically start the app when I log on to Windows";
			this.AutoStartCheckbox.UseVisualStyleBackColor = true;
			this.AutoStartCheckbox.CheckedChanged += new System.EventHandler(this.AutoStartCheckbox_CheckedChanged);
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseButton.Location = new System.Drawing.Point(573, 17);
			this.CloseButton.Margin = new System.Windows.Forms.Padding(2);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(72, 27);
			this.CloseButton.TabIndex = 7;
			this.CloseButton.Text = "E&xit";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// BingerUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(654, 322);
			this.Controls.Add(this.ContainerMain);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.Name = "BingerUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Binger Settings";
			this.Load += new System.EventHandler(this.BingerUI_Load);
			this.Shown += new System.EventHandler(this.BingerUI_Shown);
			this.Resize += new System.EventHandler(this.BingerUI_Resize);
			this.FolderGroupBox.ResumeLayout(false);
			this.FolderGroupBox.PerformLayout();
			this.ContainerMain.Panel1.ResumeLayout(false);
			this.ContainerMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ContainerMain)).EndInit();
			this.ContainerMain.ResumeLayout(false);
			this.CloseOptionsGroupBox.ResumeLayout(false);
			this.CloseOptionsGroupBox.PerformLayout();
			this.StartupGroupbox.ResumeLayout(false);
			this.StartupGroupbox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DownloadDurationControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label SaveToLabel;
		private System.Windows.Forms.TextBox SaveToFolderTextBox;
		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.GroupBox FolderGroupBox;
		private System.Windows.Forms.Button DownloadButton;
		private System.Windows.Forms.Button SetAsSlideshowButton;
		private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
		private System.Windows.Forms.Button EncodeButton;
		private System.Windows.Forms.Timer DownloadTimer;
		private System.Windows.Forms.NotifyIcon TrayIcon;
		private System.Windows.Forms.SplitContainer ContainerMain;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.GroupBox StartupGroupbox;
		private System.Windows.Forms.CheckBox AutoStartCheckbox;
		private System.Windows.Forms.Label HoursLabel;
		private System.Windows.Forms.NumericUpDown DownloadDurationControl;
		private System.Windows.Forms.CheckBox CheckPeriodicallyCheckbox;
		private System.Windows.Forms.GroupBox CloseOptionsGroupBox;
		private System.Windows.Forms.CheckBox CloseToTrayCheckBox;
		private System.Windows.Forms.CheckBox OpenMinimizedCheckBox;
		private System.Windows.Forms.Button OpenDestinationButton;
	}
}

