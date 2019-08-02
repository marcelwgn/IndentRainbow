using System;
using Microsoft.VisualStudio.Settings;

namespace IndentRainbow.Extension.Options
{
    public static class WritableSettingsStoreExtensions
    {

        public const string collectionName = "IndentRainbow";
        public const string indentSizePropertyName = "IndentSize";
        public const string fileExtensionSizesPropertyName = "FileExtensionSizes";
        public const string colorsPropertyName = "Colors";
        public const string opacityMultiplierPropertyName = "OpacityMultiplier";
        public const string detectErrorsPropertyName = "DetectErrors";
        public const string errorColorPropertyName = "ErrorColor";

        /// <summary>
        /// Saves the given indentsize to the settings store using a specific collection and property name
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="indentSize">The indent size to save</param>
        public static void SaveIndentSize(this WritableSettingsStore store, int indentSize)
        {
            store.SetInt32(collectionName, indentSizePropertyName, indentSize);
        }

        /// <summary>
        /// Saves the file extensions string to the store
        /// </summary>
        /// <param name="store">The store to use</param>
        /// <param name="fileExtensions">The file extensions string</param>
        public static void SaveFileExtensionsIndentSizes(this WritableSettingsStore store, string fileExtensions)
        {
            store.SetString(collectionName, fileExtensionSizesPropertyName, fileExtensions);
        }

        /// <summary>
        /// Saves the given string (representing the colors)
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="colors">The colors to save</param>
        public static void SaveColors(this WritableSettingsStore store, string colors)
        {
            store.SetString(collectionName, colorsPropertyName, colors);
        }

        /// <summary>
        /// Saves the opacity multiplier
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="opacityMultiplier">The opacity multiplier to save</param>
        public static void SaveOpacityMultiplier(this WritableSettingsStore store, double opacityMultiplier)
        {
            store.SetString(collectionName, opacityMultiplierPropertyName, opacityMultiplier.ToString());
        }

        /// <summary>
        /// Saves the detect errors flag
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="detectErrors">The detect error flag to save</param>
        public static void SaveDetectErrorsFlag(this WritableSettingsStore store, bool detectErrors)
        {
            store.SetBoolean(collectionName, detectErrorsPropertyName, detectErrors);
        }

        /// <summary>
        /// Saves the error color
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="errorColor">The erro color to save</param>
        public static void SaveErrorColor(this WritableSettingsStore store, string errorColor)
        {
            store.SetString(collectionName, errorColorPropertyName, errorColor);
        }

        /// <summary>
        /// Loads the indent size from the settings store.
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The indent size saved or if not found, the default indent size</returns>
        public static int LoadIndentSize(this WritableSettingsStore store)
        {
            if (!store.PropertyExists(collectionName, indentSizePropertyName))
            {
                store.SaveIndentSize(DefaultRainbowIndentOptions.defaultIndentSize);
                return DefaultRainbowIndentOptions.defaultIndentSize;
            } else
            {
                return store.GetInt32(collectionName, indentSizePropertyName);
            }
        }

        /// <summary>
        /// Loads the file extensions string from store
        /// </summary>
        /// <param name="store">The store to use</param>
        /// <returns>The file extensions string stored or if not found, the default indent size</returns>
        public static string LoadFileExtensionsIndentSizes(this WritableSettingsStore store)
        {
            if (!store.PropertyExists(collectionName, fileExtensionSizesPropertyName))
            {
                store.SaveFileExtensionsIndentSizes(DefaultRainbowIndentOptions.defaultFileExtensionsIndentSizes);
                return DefaultRainbowIndentOptions.defaultFileExtensionsIndentSizes;
            } else
            {
                return store.GetString(collectionName, fileExtensionSizesPropertyName);
            }
        }

        /// <summary>
        /// Loads the color string from the settings store
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>´The colors string or if not found, the default colors</returns>
        public static string LoadColors(this WritableSettingsStore store)
        {
            if (!store.PropertyExists(collectionName, colorsPropertyName))
            {
                store.SaveColors(DefaultRainbowIndentOptions.defaultColors);
                return DefaultRainbowIndentOptions.defaultColors;
            } else
            {
                return store.GetString(collectionName, colorsPropertyName);
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
            if (!store.PropertyExists(collectionName, opacityMultiplierPropertyName))
            {
                store.SaveOpacityMultiplier(opacMultiplier);
            } else
            {
                Double.TryParse(store.GetString(collectionName, opacityMultiplierPropertyName), out opacMultiplier);
            }
            return opacMultiplier;

        }

        /// <summary>
        /// Loads the detect errors flag
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <returns>The detect error flag or if not found, the default detect error flag</returns>
        public static bool LoadDetectErrorsFlag(this WritableSettingsStore store)
        {
            var detectErrorFlag = DefaultRainbowIndentOptions.defaultDetectErrorsFlag;
            if (!store.PropertyExists(collectionName, detectErrorsPropertyName))
            {
                store.SaveDetectErrorsFlag(detectErrorFlag);
            } else
            {
                detectErrorFlag = store.GetBoolean(collectionName, detectErrorsPropertyName);
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
            if (!store.PropertyExists(collectionName, errorColor))
            {
                store.SaveErrorColor(errorColor);
            } else
            {
                errorColor = store.GetString(collectionName, errorColorPropertyName);
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
            if (!store.CollectionExists(collectionName))
            {
                store.CreateCollection(collectionName);
            }
        }
    }
}
