using IndentRainbow.Logic.Colors;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System.Windows.Media;

namespace IndentRainbow.Extension.Options
{
    internal static class OptionsManager
    {
        /// <summary>
        /// Flag used for checking if data has been loaded.
        /// </summary>
        private static bool loadedFromStorage = false;

        /// <summary>
        /// Saved value of the indent size. The value is saved as static field for better performance
        /// </summary>
        private static int indentSize = DefaultRainbowIndentOptions.defaultIndentSize;

        /// <summary>
        /// Saved value of the colors string. The value is saved as static field for better performance
        /// </summary>
        private static string colors = DefaultRainbowIndentOptions.defaultColors;

        /// <summary>
        /// Saved value of the brushes array. The value is saved as static field for better performance
        /// </summary>
        private static Brush[] brushes = ColorParser.ConvertStringToBrushArray(colors,opacityMultiplier);

        /// <summary>
        /// The opacity multiplier which is applied to all colors
        /// </summary>
        private static double opacityMultiplier = DefaultRainbowIndentOptions.defaultOpacityMultiplier;

        /// <summary>
        /// Gets an instance of the WritableSettingsStore class, 
        ///  which will be setup to have the necessary collection have created.
        /// </summary>
        /// <returns>An WritableSettingsStore instance</returns>
        private static WritableSettingsStore GetWritableSettingsStore()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            WritableSettingsStore settingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            settingsStore.SetupIndentRainbowCollection();
            return settingsStore;
        }

        /// <summary>
        /// Loads the settings from the settings store
        /// </summary>
        public static void LoadSettings()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!loadedFromStorage)
            {
                var settingsStore = GetWritableSettingsStore();
                indentSize = settingsStore.LoadIndentSize();
                colors = settingsStore.LoadColors();
                opacityMultiplier = settingsStore.LoadOpacityMultiplier();
                brushes = ColorParser.ConvertStringToBrushArray(colors,opacityMultiplier);
                loadedFromStorage = true;
            }
        }

        /// <summary>
        /// Saves the settings to the settings store
        /// </summary>
        /// <param name="indentSize">The indent size specifiyng the number of spaces for indentation detection</param>
        /// <param name="colors">The colors as string</param>
        public static void SaveSettings(int indentSize, string colors, double opacityMultiplier)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var settingsStore = GetWritableSettingsStore();
            settingsStore.SaveIndentSize(indentSize);
            settingsStore.SaveColors(colors);
            settingsStore.SaveOpacityMultiplier(opacityMultiplier);
            OptionsManager.indentSize = indentSize;
            OptionsManager.colors = colors;
            brushes = ColorParser.ConvertStringToBrushArray(colors,opacityMultiplier);
            OptionsManager.opacityMultiplier = opacityMultiplier;
        }

        /// <summary>
        /// Retrieves the indent size. 
        /// If the setting has not been loaded from the 
        /// settings store / storage, the value will be loaded and the loaded value will be returned.
        /// </summary>
        /// <returns>The indent size</returns>
        public static int GetIndentSize()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!loadedFromStorage)
            {
                LoadSettings();
            }
            return indentSize;
        }

        /// <summary>
        /// Retrieves the colors string.
        /// If the setting has not been loaded from the 
        /// settings store / storage, the value will be loaded and the loaded value will be returned.
        /// </summary>
        /// <returns>The colors string</returns>
        public static string GetColorsString()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!loadedFromStorage)
            {
                LoadSettings();
            }
            return colors;
        }

        /// <summary>
        /// Retrieves the brushes used for drawing.
        /// If the colors string has not been loaded from the settings store / storage,
        /// the value will be loaded, the loaded value will be parsed 
        /// and the parsed brushes will be returned.
        /// </summary>
        /// <returns>The brushes that result from converting the colors string</returns>
        public static Brush[] GetColors()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!loadedFromStorage)
            {
                LoadSettings();
            }
            return brushes;
        }

        public static double GetOpacityMultiplier()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!loadedFromStorage)
            {
                LoadSettings();
            }
            return opacityMultiplier;
        }
    }
}
