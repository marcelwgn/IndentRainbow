namespace IndentRainbow.Extension.Options.View
{
	partial class OptionsForm
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.optionsPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.restartDisclaimer = new System.Windows.Forms.Label();
			this.mainLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainLayout
			// 
			this.mainLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainLayout.AutoSize = true;
			this.mainLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.mainLayout.ColumnCount = 1;
			this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainLayout.Controls.Add(this.linkLabel1, 0, 2);
			this.mainLayout.Controls.Add(this.optionsPropertyGrid, 0, 0);
			this.mainLayout.Controls.Add(this.restartDisclaimer, 0, 1);
			this.mainLayout.Location = new System.Drawing.Point(0, 0);
			this.mainLayout.Name = "mainLayout";
			this.mainLayout.RowCount = 3;
			this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainLayout.Size = new System.Drawing.Size(1082, 579);
			this.mainLayout.TabIndex = 0;
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(63, 108);
			this.linkLabel1.Location = new System.Drawing.Point(0, 562);
			this.linkLabel1.Margin = new System.Windows.Forms.Padding(0);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(1082, 17);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "For bugs and feature requests, please open an issue on GitHub: https://github.com" +
    "/chingucoding/IndentRainbow";
			this.linkLabel1.UseCompatibleTextRendering = true;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// optionsPropertyGrid
			// 
			this.optionsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optionsPropertyGrid.Location = new System.Drawing.Point(3, 3);
			this.optionsPropertyGrid.Name = "optionsPropertyGrid";
			this.optionsPropertyGrid.Size = new System.Drawing.Size(1076, 529);
			this.optionsPropertyGrid.TabIndex = 1;
			// 
			// restartDisclaimer
			// 
			this.restartDisclaimer.AutoSize = true;
			this.restartDisclaimer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.restartDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.restartDisclaimer.Location = new System.Drawing.Point(0, 541);
			this.restartDisclaimer.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
			this.restartDisclaimer.Name = "restartDisclaimer";
			this.restartDisclaimer.Size = new System.Drawing.Size(1082, 15);
			this.restartDisclaimer.TabIndex = 2;
			this.restartDisclaimer.Text = "Note that changes only take effect after reopening documents or restarting Visual" +
    " Studio.";
			this.restartDisclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainLayout);
			this.Name = "OptionsForm";
			this.Size = new System.Drawing.Size(1082, 579);
			this.mainLayout.ResumeLayout(false);
			this.mainLayout.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel mainLayout;
		private System.Windows.Forms.PropertyGrid optionsPropertyGrid;
		private System.Windows.Forms.Label restartDisclaimer;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}
