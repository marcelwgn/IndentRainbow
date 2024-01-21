using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndentRainbow.Extension.Options.View
{
	public partial class OptionsForm : UserControl
	{
		public OptionsForm(OptionsPage optionsPage)
		{
			InitializeComponent();
			optionsPropertyGrid.SelectedObject = optionsPage;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
#pragma warning disable CA1031 // Do not catch general exception types
			try
			{
				System.Diagnostics.Process.Start("https://github.com/marcelwgn/IndentRainbow");
			}
			catch (Exception) { }
#pragma warning restore CA1031 // Do not catch general exception types
		}
	}
}