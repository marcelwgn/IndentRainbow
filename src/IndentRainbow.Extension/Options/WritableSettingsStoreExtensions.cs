using System;
using System.Globalization;
using Microsoft.VisualStudio.Settings;

namespace IndentRainbow.Extension.Options
{
    public static class WritableSettingsStoreExtensions
    {

        public const string CollectionName = "IndentRainbow";
        public const string IndentSizePropertyName = "IndentSize";
        public const string FileExtensionSizesPropertyName = "FileExtensionSizes";
        public const string FolorsPropertyName = "Colors";
        public const string HighlightingModePropertyName = "HighlightingMode";
        public const string ColorModePropertyName = "ColorMode";
        public const string OpacityMultiplierPropertyName = "OpacityMultiplier";
        public const string DetectErrorsPropertyName = "DetectErrors";
        public const string errorColorPropertyName = "ErrorColor";

        /// <summary>
        /// Saves the given indentsize to the settings store using a specific collection and property name
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="indentSize">The indent size to save</param>
        public static void SaveIndentSize(this WritableSettingsStore store, int indentSize)
        {
            store?.SetInt32(CollectionName, IndentSizePropertyName, indentSize);
        }

        /// <summary>
        /// Saves the file extensions string to the store
        /// </summary>
        /// <param name="store">The store to use</param>
        /// <param name="fileExtensions">The file extensions string</param>
        public static void SaveFileExtensionsIndentSizes(this WritableSettingsStore store, string fileExtensions)
        {
            store?.SetString(CollectionName, FileExtensionSizesPropertyName, fileExtensions);
        }

        /// <summary>
        /// Saves the given string (representing the colors)
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="colors">The colors to save</param>
        public static void SaveColors(this WritableSettingsStore store, string colors)
        {
            store?.SetString(CollectionName, FolorsPropertyName, colors);
        }

        /// <summary>
        /// Saves the opacity multiplier
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="opacityMultiplier">The opacity multiplier to save</param>
        public static void SaveOpacityMultiplier(this WritableSettingsStore store, double opacityMultiplier)
        {
            store?.SetString(CollectionName, OpacityMultiplierPropertyName, opacityMultiplier.ToString(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Saves the highlightingMode
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="highlightingMode">The highlightingmode to  save</param>
        public static void SaveHighlightingMode(this WritableSettingsStore store, HighlightingMode highlightingMode)
        {
            store?.SetString(CollectionName, HighlightingModePropertyName, highlightingMode.ToString());
        }

        /// <summary>
        /// Saves the colormode
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="colorMode">The highlightingmode to  save</param>
        public static void SaveColorMode(this WritableSettingsStore store, ColorMode colorMode)
        {
            store?.SetString(CollectionName, ColorModePropertyName, colorMode.ToString());
        }

        /// <summary>
        /// Saves the detect errors flag
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="detectErrors">The detect error flag to save</param>
        public static void SaveDetectErrorsFlag(this WritableSettingsStore store, bool detectErrors)
        {
            store?.SetBoolean(CollectionName, DetectErrorsPropertyName, detectErrors);
        }

        /// <summary>
        /// Saves the error color
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="errorColor">The error color to save</param>
        public static void SaveErrorColor(this WritableSettingsStore store, string errorColor)
        {
            store?.SetString(CollectionName, errorColorPropertyName, errorColor);
        }

        /// <summary>
        /// Loads the indent size from the settings store.
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The indent size saved or if not found, the default indent size</returns>
        public static int LoadIndentSize(this WritableSettingsStore store)
        {
            if (store == null)
            {
                return DefaultRainbowIndentOptions.defaultIndentSize;
            }
            if (!store.PropertyExists(CollectionName, IndentSizePropertyName))
            {
                store.SaveIndentSize(DefaultRainbowIndentOptions.defaultIndentSize);
                return DefaultRainbowIndentOptions.defaultIndentSize;
            }
            else
            {
                return store.GetInt32(CollectionName, IndentSizePropertyName);
            }
        }

        /// <summary>
        /// Loads the file extensions string from store
        /// </summary>
        /// <param name="store">The store to use</param>
        /// <returns>The file extensions string stored or if not found, the default indent size</returns>
        public static string LoadFileExtensionsIndentSizes(this WritableSettingsStore store)
        {
            if (store == null)
            {
                return "";
            }
            if (!store.PropertyExists(CollectionName, FileExtensionSizesPropertyName))
            {
                store.SaveFileExtensionsIndentSizes(DefaultRainbowIndentOptions.defaultFileExtensionsIndentSizes);
                return DefaultRainbowIndentOptions.defaultFileExtensionsIndentSizes;
            }
            else
            {
                return store.GetString(CollectionName, FileExtensionSizesPropertyName);
            }
        }

        /// <summary>
        /// Loads the color string from the settings store
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>´The colors string or if not found, the default colors</returns>
        public static string LoadColors(this WritableSettingsStore store)
        {
            if (store == null)
            {
                return "";
            }
            if (!store.PropertyExists(CollectionName, FolorsPropertyName))
            {
                store.SaveColors(DefaultRainbowIndentOptions.defaultColors);
                return DefaultRainbowIndentOptions.defaultColors;
            }
            else
            {
                return store.GetString(CollectionName, FolorsPropertyName);
            }
        }

        /// <summary>
        /// Loads the opacity multiplier
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The opacity multiplier or if not found, the default opacity multiplier</returns>
        public static double LoadOpacityMultiplier(this WritableSettingsStore store)
        {
            var opacMultiplier = DefaultRainbowIndentOptions.defaultOpacityMultiplier;
            if (store == null)
            {
                return opacMultiplier;
            }
            if (!store.PropertyExists(CollectionName, OpacityMultiplierPropertyName))
            {
                store.SaveOpacityMultiplier(opacMultiplier);
            }
            else
            {
                // No matter what happens, opacMultiplier will have a valid value as this is ensured by the input form
#pragma warning disable CA1806 // Do not ignore method results
                double.TryParse(store.GetString(CollectionName, OpacityMultiplierPropertyName), out opacMultiplier);
#pragma warning restore CA1806 // Do not ignore method results
            }
            return opacMultiplier;

        }

        /// <summary>
        /// Loads the highlighting mode
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The highlighting mode or if not found, the default highlighting mode</returns>
        public static HighlightingMode LoadHighlightingMode(this WritableSettingsStore store)
        {
            var highlightingMode = DefaultRainbowIndentOptions.defaultHighlightingMode;
            if (store == null)
            {
                return highlightingMode;
            }
            if (!store.PropertyExists(CollectionName, HighlightingModePropertyName))
            {
                store.SaveHighlightingMode(highlightingMode);
            }
            else
            {
                highlightingMode = (HighlightingMode)Enum.Parse(typeof(HighlightingMode),
                    store.GetString(CollectionName, HighlightingModePropertyName));
            }
            return highlightingMode;
        }

        /// <summary>
        /// Loads the color mode
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The color mode or if not found, the default color mode</returns>
        public static ColorMode LoadColorMode(this WritableSettingsStore store)
        {
            var colorMode = DefaultRainbowIndentOptions.defaultColorMode;
            if (store == null)
            {
                return colorMode;
            }

            if (!store.PropertyExists(CollectionName, ColorModePropertyName))
            {
                store.SaveColorMode(colorMode);
            }
            else
            {
                colorMode = (ColorMode)Enum.Parse(typeof(ColorMode),
                    store.GetString(CollectionName, ColorModePropertyName));
            }
            return colorMode;
        }

        /// <summary>
        /// Loads the detect errors flag
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The detect error flag or if not found, the default detect error flag</returns>
        public static bool LoadDetectErrorsFlag(this WritableSettingsStore store)
        {
            var detectErrorFlag = DefaultRainbowIndentOptions.defaultDetectErrorsFlag;
            if (store == null)
            {
                return detectErrorFlag;
            }
            if (!store.PropertyExists(CollectionName, DetectErrorsPropertyName))
            {
                store.SaveDetectErrorsFlag(detectErrorFlag);
            }
            else
            {
                detectErrorFlag = store.GetBoolean(CollectionName, DetectErrorsPropertyName);
            }
            return detectErrorFlag;
        }

        /// <summary>
        /// Loads the error color
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The error color or if not found, the default error color</returns>
        public static string LoadErrorColor(this WritableSettingsStore store)
        {
            var errorColor = DefaultRainbowIndentOptions.defaultErrorColor;
            if (store == null)
            {
                return errorColor;
            }
            if (!store.PropertyExists(CollectionName, errorColorPropertyName))
            {
                store.SaveErrorColor(errorColor);
            }
            else
            {
                errorColor = store.GetString(CollectionName, errorColorPropertyName);
            }
            return errorColor;
        }

        /// <summary>
        /// Sets the settings store up.
        /// Creates the default collection for this extensions settings if it does not exist
        /// </summary>
        /// <param name="store"></param>
        public static void SetupIndentRainbowCollection(this WritableSettingsStore store)
        {
            if (store == null)
            {
                return;
            }
            if (!store.CollectionExists(CollectionName))
            {
                store.CreateCollection(CollectionName);
            }
        }
    }
}
