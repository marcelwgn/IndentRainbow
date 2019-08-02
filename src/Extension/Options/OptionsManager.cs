using IndentRainbow.Logic.Parser;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System.Collections.Generic;
using System.Windows.Media;

namespace IndentRainbow.Extension.Options
{
    internal static partial class OptionsManager
    {

        /// <summary>
        /// Flag used for checking if data has been loaded.
        /// </summary>
        private static bool loadedFromStorage = false;

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
        /// Saved value of the indent size. The value is saved as static field for better performance
        /// </summary>
        public static OptionsField<int> indentSize = new OptionsField<int>(DefaultRainbowIndentOptions.defaultIndentSize);

        /// <summary>
        /// Saved value of the colors string. The value is saved as static field for better performance
        /// </summary>
        public static OptionsField<string> colors = new OptionsField<string>(DefaultRainbowIndentOptions.defaultColors);

        /// <summary>
        /// Saved value of the brushes array. The value is saved as static field for better performance
        /// </summary>
        public static OptionsField<Brush[]> brushes = new OptionsField<Brush[]>(
            ColorParser.ConvertStringToBrushArray(DefaultRainbowIndentOptions.defaultColors, DefaultRainbowIndentOptions.defaultOpacityMultiplier));

        /// <summary>
        /// The opacity multiplier which is applied to all colors
        /// </summary>
        public static OptionsField<double> opacityMultiplier = new OptionsField<double>(DefaultRainbowIndentOptions.defaultOpacityMultiplier);

        /// <summary>
        /// The detect error flag which determines wether errors will be highlighted
        /// </summary>
        public static OptionsField<bool> detectErrors = new OptionsField<bool>(DefaultRainbowIndentOptions.defaultDetectErrorsFlag);

        /// <summary>
        /// The error color used for highlighting of wrong colors
        /// </summary>
        public static OptionsField<string> errorColor = new OptionsField<string>(DefaultRainbowIndentOptions.defaultErrorColor);

        /// <summary>
        /// The string containing the file extensions indent sizes
        /// </summary>
        public static OptionsField<string> fileExtensionsString = new OptionsField<string>(DefaultRainbowIndentOptions.defaultFileExtensionsIndentSizes);

        /// <summary>
        /// Containing the language extensions and their indent sizes
        /// </summary>
        public static OptionsField<Dictionary<string, int>> fileExtensionsDictionary = new OptionsField<Dictionary<string, int>>(new Dictionary<string, int>());

        /// <summary>
        /// Saved value of the error brush
        /// </summary>
        public static OptionsField<Brush> errorBrush = new OptionsField<Brush>(
            ColorParser.ConvertStringToBrush(DefaultRainbowIndentOptions.defaultErrorColor, DefaultRainbowIndentOptions.defaultOpacityMultiplier));



        /// <summary>
        /// Loads the settings from the settings store
        /// </summary>
        public static void LoadSettings()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!loadedFromStorage)
            {
                var settingsStore = GetWritableSettingsStore();
                indentSize.Set(settingsStore.LoadIndentSize());
                colors.Set(settingsStore.LoadColors());
                opacityMultiplier.Set(settingsStore.LoadOpacityMultiplier());
                errorColor.Set(settingsStore.LoadErrorColor());
                detectErrors.Set(settingsStore.LoadDetectErrorsFlag());
                fileExtensionsString.Set(settingsStore.LoadFileExtensionsIndentSizes());
                //This fields have to be initialized after the other fields since they depend on them
                loadedFromStorage = true;
                brushes.Set(ColorParser.ConvertStringToBrushArray(colors.Get(), opacityMultiplier.Get()));
                errorBrush.Set(ColorParser.ConvertStringToBrush(errorColor.Get(), opacityMultiplier.Get()));
                fileExtensionsDictionary.Set(LanguageParser.CreateDictionaryFromString(fileExtensionsString.Get()));
            }
        }

        /// <summary>
        /// Saves the settings to the settings store
        /// </summary>
        /// <param name="indentSize">The indent size specifiyng the number of spaces for indentation detection</param>
        /// <param name="colors">The colors as string</param>
        public static void SaveSettings(int indentSize,string fileExtensionsString, string colors, double opacityMultiplier, string errorColor, bool detectError)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var settingsStore = GetWritableSettingsStore();
            settingsStore.SaveIndentSize(indentSize);
            settingsStore.SaveFileExtensionsIndentSizes(fileExtensionsString);
            settingsStore.SaveColors(colors);
            settingsStore.SaveOpacityMultiplier(opacityMultiplier);
            settingsStore.SaveDetectErrorsFlag(detectError);
            settingsStore.SaveErrorColor(errorColor);
            OptionsManager.indentSize.Set(indentSize);
            OptionsManager.fileExtensionsString.Set(fileExtensionsString);
            OptionsManager.fileExtensionsDictionary.Set(LanguageParser.CreateDictionaryFromString(fileExtensionsString));
            OptionsManager.colors.Set(colors);
            OptionsManager.brushes.Set(ColorParser.ConvertStringToBrushArray(colors, opacityMultiplier));
            OptionsManager.opacityMultiplier.Set(opacityMultiplier);
            OptionsManager.errorColor.Set(errorColor);
            OptionsManager.detectErrors.Set(detectError);
            OptionsManager.errorBrush.Set(ColorParser.ConvertStringToBrush(errorColor, opacityMultiplier));
        }
    }
}
