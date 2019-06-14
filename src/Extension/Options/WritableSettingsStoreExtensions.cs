using System;
using Microsoft.VisualStudio.Settings;

namespace IndentRainbow.Extension.Options
{
    public static class WritableSettingsStoreExtensions
    {

        public const string collectionName = "IndentRainbow";
        public const string indentSizePropertyName = "IndentSize";
        public const string colorsPropertyName = "Colors";
        public const string opacityMultiplierPropertyName = "OpacityMultiplier";

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
        /// Saves the given string (representing the colors)
        /// </summary>
        /// <param name="store">The writable settings store</param>
        /// <param name="colors">The colors to save</param>
        public static void SaveColors(this WritableSettingsStore store, string colors)
        {
            store.SetString(collectionName, colorsPropertyName, colors);
        }

        public static void SaveOpacityMultiplier(this WritableSettingsStore store, double opacityMultiplier)
        {
            store.SetString(collectionName, opacityMultiplierPropertyName, opacityMultiplier.ToString());
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
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
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
