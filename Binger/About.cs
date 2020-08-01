﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binger
{
	partial class About : Form
	{
		public About()
		{
			InitializeComponent();
			Text = $"About {AssemblyTitle}";
			labelProductName.Text = AssemblyProduct;
			labelVersion.Text = $"Version {AssemblyVersion}";
			labelCopyright.Text = AssemblyCopyright;
			labelCompanyName.Text = AssemblyCompany;
			textBoxDescription.Text = AssemblyDescription;
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

				if (attributes.Length <= 0) { return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase); }

				var titleAttribute = (AssemblyTitleAttribute)attributes[0];

				return string.IsNullOrWhiteSpace(titleAttribute.Title) ? System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase) : titleAttribute.Title;
			}
		}

		public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public string AssemblyDescription
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

				return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);

				return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

				return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

				return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion
	}
}
