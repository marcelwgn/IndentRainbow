using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel;

namespace IndentRainbow.Extension.Options
{

    /// <summary>
    /// This is the options page for the IndentRainbow extension.
    /// The loading and saving to storage has been overwritten since 
    /// there is no easy way to load settings saved with the 
    /// default option pages without a package to load the settingspackage (or atleast I did not find a way).
    /// If there is a better way, PRs are appreciated :)
    /// </summary>
    internal class OptionsPage : DialogPage
    {
        [Category("Indentation")]
        [DisplayName("Indent size")]
        [Description("The amount of spaces used for indentation")]
        public int IndentSize { get; set; }


        [Category("Colors")]
        [DisplayName("Colors list")]
        [Description("The colors used for indentation levels. " +
            "Colors must be provided in ARGB hexadecimal and must be separated by a comma.")]
        public string Colors { get; set; }

        [Category("Colors")]
        [DisplayName("General opacity")]
        [Description("The opacity that will be applied to all colors. If the color has an opacity of 0.5 and this value is 0.5, the color will be drawn with an opacity of 0.25")]
        public double OpacityMultiplier { get; set; }

        [Category("Error highlighting")]
        [DisplayName("Highlight wrong indentation")]
        [Description("Determines wether wrong/faulty indentation will be detected and highlighted or wether it should be treated as correct indentation")]
        public bool HighglightErrors { get; set; }

        [Category("Error highlighting")]
        [DisplayName("Wrong indententation color")]
        [Description("The color that will be used to draw, when the indentation is faulty/incomplete")]
        public string ErrorColor { get; set; }


        /// <summary>
        /// Loads the settings for this form from storage and sets the values.
        /// </summary>
        public override void LoadSettingsFromStorage()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            OptionsManager.LoadSettings();
            this.IndentSize = OptionsManager.indentSize.Get();
            this.Colors = OptionsManager.colors.Get();
            this.OpacityMultiplier = OptionsManager.opacityMultiplier.Get();
            this.HighglightErrors = OptionsManager.detectErrors.Get();
            this.ErrorColor = OptionsManager.errorColor.Get();
        }

        /// <summary>
        /// Saves the settings to storage
        /// </summary>
        public override void SaveSettingsToStorage()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            OptionsManager.SaveSettings(this.IndentSize, this.Colors, this.OpacityMultiplier, this.ErrorColor, this.HighglightErrors);
        }
    }
}
