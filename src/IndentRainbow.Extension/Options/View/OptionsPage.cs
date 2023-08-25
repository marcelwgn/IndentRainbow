using System;
using System.ComponentModel;
using System.Windows.Forms;
using IndentRainbow.Extension.Options.View;
using Microsoft.VisualStudio.Shell;
using static IndentRainbow.Logic.Parser.ColorParser;

namespace IndentRainbow.Extension.Options
{


	/// <summary>
	/// This is the options page for the IndentRainbow extension.
	/// The loading and saving to storage has been overwritten since 
	/// there is no easy way to load settings saved with the 
	/// default option pages without a package to load the settingspackage.
	/// </summary>
	// This page gets instantiated by Visual Studio so it is used!
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
	public class OptionsPage : DialogPage
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
	{
		protected override IWin32Window Window
		{
			get
			{
				var page = new OptionsForm(this);
				return page;
			}
		}

		[Category("Indentation")]
		[DisplayName("Indent size")]
		[Description("The amount of spaces used for indentation")]
		public int IndentSize { get; set; }

		[Category("Indentation")]
		[DisplayName("File specific indent sizes")]
		[Description("The amount of spaces used based on the file extensions. " +
			"File extensions should be specified in the format " +
			"'file-extensions':'indent-size';'next-file-extension':'next-indent-size';" +
			"For example: 'cs:4;js:2'. Changing requires document reload or VS restart.")]
		public string FileSpecificIndentSizes { get; set; }


		[Category("Colors")]
		[DisplayName("Colors list")]
		[Description("The colors used for indentation levels. " +
			"Colors must be provided in ARGB hexadecimal and must be separated by a comma.")]
		public string Colors { get; set; }

		[Category("Colors")]
		[DisplayName("General opacity")]
		[Description("The opacity that will be applied to all colors. If the color has an opacity of 0.5 and this value is 0.5, the color will be drawn with an opacity of 0.25")]
		public double OpacityMultiplier { get; set; }

		[Category("Colors")]
		[DisplayName("Highlighting mode")]
		[Description("Determines whether to alternate between the colors in a single with every indent or use the color of the last indent level for the whole indentation block.")]
		public HighlightingMode HighlightingMode { get; set; }

		[Category("Colors")]
		[DisplayName("Color mode")]
		[Description("Determines whether the colors change in a soft gradient or with a hard cut after every indent step.")]
		public ColorMode ColorMode { get; set; }

		[Category("Colors")]
		[DisplayName("Fade colors")]
		[Description("Determines whether colors should fade in and out of the code or whether there should be a hard cut at the start and end")]
		public bool FadeColors { get; set; }

		[Category("Error highlighting")]
		[DisplayName("Highlight wrong indentation")]
		[Description("Determines whether wrong/faulty indentation will be detected and highlighted or whether it should be treated as correct indentation")]
		public bool HighglightErrors { get; set; }

		[Category("Error highlighting")]
		[DisplayName("Wrong indentation color")]
		[Description("The color that will be used to draw, when the indentation is faulty/incomplete")]
		public string ErrorColor { get; set; }

		/// <summary>
		/// Loads the settings for this form from storage and sets the values.
		/// </summary>
		public override void LoadSettingsFromStorage()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			OptionsManager.LoadSettings();
			IndentSize = OptionsManager.indentSize.Get();
			Colors = OptionsManager.hexCodes.Get();
			FileSpecificIndentSizes = OptionsManager.fileExtensionsString.Get();
			OpacityMultiplier = OptionsManager.opacityMultiplier.Get();
			HighglightErrors = OptionsManager.detectErrors.Get();
			ErrorColor = OptionsManager.errorColor.Get();
			HighlightingMode = OptionsManager.highlightingMode.Get();
			ColorMode = OptionsManager.colorMode.Get();
			FadeColors = OptionsManager.fadeColors.Get();
		}

		/// <summary>
		/// Saves the settings to storage
		/// </summary>
		public override void SaveSettingsToStorage()
		{
			if (FileSpecificIndentSizes is null)
			{
				FileSpecificIndentSizes = "";
			}
			if (Colors is null)
			{
				Colors = "";
			}
			if (ErrorColor is null)
			{
				ErrorColor = "";
			}
			ThreadHelper.ThrowIfNotOnUIThread();
			OptionsManager.SaveSettings(IndentSize,
				FileSpecificIndentSizes,
				Colors,
				OpacityMultiplier,
				HighlightingMode,
				ColorMode,
				FadeColors,
				ErrorColor,
				HighglightErrors);
		}
	}
}
