using System.Collections.Generic;
using System.Windows.Media;
using IndentRainbow.Logic.Parser;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using static IndentRainbow.Logic.Parser.ColorParser;

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
        /// Saved value of the indentation detection mode. The value is saved as static field for better performance
        /// </summary>
        public static OptionsField<IndentationSizeMode> indentationSizeMode = new OptionsField<IndentationSizeMode>(DefaultRainbowIndentOptions.defaultIndentationSizeMode);

        /// <summary>
        /// Saved value of the indent size. The value is saved as static field for better performance
        /// </summary>
        public static OptionsField<int> indentSize = new OptionsField<int>(DefaultRainbowIndentOptions.defaultIndentSize);

		/// <summary>
		/// Saved value of the colors string. The value is saved as static field for better performance
		/// </summary>
		public static OptionsField<string> hexCodes = new OptionsField<string>(DefaultRainbowIndentOptions.defaultColors);

		/// <summary>
		/// Saved value of the brushes array. The value is saved as static field for better performance
		/// </summary>
		public static OptionsField<Color[]> colors = new OptionsField<Color[]>(
			ColorParser.ConvertStringToColorArray(DefaultRainbowIndentOptions.defaultColors, DefaultRainbowIndentOptions.defaultOpacityMultiplier));

		/// <summary>
		/// The opacity multiplier which is applied to all colors
		/// </summary>
		public static OptionsField<double> opacityMultiplier = new OptionsField<double>(DefaultRainbowIndentOptions.defaultOpacityMultiplier);

		/// <summary>
		/// The highlighting mode that should be used for drawing
		/// </summary>
		public static OptionsField<HighlightingMode> highlightingMode = new OptionsField<HighlightingMode>(DefaultRainbowIndentOptions.defaultHighlightingMode);

		/// <summary>
		/// The color mode that should be used for drawing
		/// </summary>
		public static OptionsField<ColorMode> colorMode = new OptionsField<ColorMode>(DefaultRainbowIndentOptions.defaultColorMode);
		/// <summary>
		/// The fade colors flag determines whether to fade colors in and out
		/// </summary>
		public static OptionsField<bool> fadeColors = new OptionsField<bool>(DefaultRainbowIndentOptions.defaultFadeColors);

		/// <summary>
		/// The detect error flag which determines whether errors will be highlighted
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
				indentationSizeMode.Set(settingsStore.LoadIndentationSizeMode());
				indentSize.Set(settingsStore.LoadIndentSize());
				hexCodes.Set(settingsStore.LoadColors());
				opacityMultiplier.Set(settingsStore.LoadOpacityMultiplier());
				highlightingMode.Set(settingsStore.LoadHighlightingMode());
				colorMode.Set(settingsStore.LoadColorMode());
				fadeColors.Set(settingsStore.LoadFadeColors());
				errorColor.Set(settingsStore.LoadErrorColor());
				detectErrors.Set(settingsStore.LoadDetectErrorsFlag());
				fileExtensionsString.Set(settingsStore.LoadFileExtensionsIndentSizes());
				//This fields have to be initialized after the other fields since they depend on them
				loadedFromStorage = true;
				colors.Set(ConvertStringToColorArray(hexCodes.Get(), opacityMultiplier.Get()));
				errorBrush.Set(ConvertStringToBrush(errorColor.Get(), opacityMultiplier.Get()));
				fileExtensionsDictionary.Set(LanguageParser.CreateDictionaryFromString(fileExtensionsString.Get()));
			}
		}

		/// <summary>
		/// Saves the settings to the settings store
		/// </summary>
		/// <param name="newIndentSize">The indent size specifying the number of spaces for indentation detection</param>
		/// <param name="colors">The colors as string</param>
		public static void SaveSettings(IndentationSizeMode newIndentSizeMode, int newIndentSize, string newFileExtensionsString, string newHexCodes, double newOpacityMultiplier, HighlightingMode newHighlightingMode, ColorMode newColorMode, bool newFadeColors, string newErrorColor, bool newDetectError)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			var settingsStore = GetWritableSettingsStore();
			settingsStore.SaveIndentationSizeMode(newIndentSizeMode);
            settingsStore.SaveIndentSize(newIndentSize);
			settingsStore.SaveFileExtensionsIndentSizes(newFileExtensionsString);
			settingsStore.SaveColors(newHexCodes);
			settingsStore.SaveOpacityMultiplier(newOpacityMultiplier);
			settingsStore.SaveHighlightingMode(newHighlightingMode);
			settingsStore.SaveColorMode(newColorMode);
			settingsStore.SaveDetectErrorsFlag(newDetectError);
			settingsStore.SaveErrorColor(newErrorColor);
			settingsStore.SaveFadeColors(newFadeColors);

			indentationSizeMode.Set(newIndentSizeMode);
			indentSize.Set(newIndentSize);
			fileExtensionsString.Set(newFileExtensionsString);
			fileExtensionsDictionary.Set(LanguageParser.CreateDictionaryFromString(newFileExtensionsString));
			hexCodes.Set(newHexCodes);
			colors.Set(ConvertStringToColorArray(newHexCodes, newOpacityMultiplier));
			opacityMultiplier.Set(newOpacityMultiplier);
			fadeColors.Set(newFadeColors);
			highlightingMode.Set(newHighlightingMode);
			colorMode.Set(newColorMode);
			errorColor.Set(newErrorColor);
			detectErrors.Set(newDetectError);
			errorBrush.Set(ConvertStringToBrush(newErrorColor, newOpacityMultiplier));
		}
	}
}
