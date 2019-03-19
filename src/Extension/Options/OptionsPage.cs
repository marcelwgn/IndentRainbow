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
    /// If there is a better way PR are appreciated :)
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
            "Colors must be provided in ARGB hexadecimal and must be seperated by a comma.")]
        public string Colors { get; set; }

        /// <summary>
        /// Loads the settings for this form from storage and sets the values.
        /// </summary>
        public override void LoadSettingsFromStorage()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            OptionsManager.LoadSettings();
            this.IndentSize = OptionsManager.GetIndentSize();
            this.Colors = OptionsManager.GetColorsString();
        }

        /// <summary>
        /// Saves the settings to storage
        /// </summary>
        public override void SaveSettingsToStorage()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            OptionsManager.SaveSettings(this.IndentSize, this.Colors);
        }
    }
}
