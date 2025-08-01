﻿using System.ComponentModel;
using static IndentRainbow.Logic.Parser.ColorParser;

namespace IndentRainbow.Extension.Options
{
    public enum HighlightingMode
    {
        [Description("Alternating colors")]
        Alternating = 0,
        [Description("One color")]
        Monocolor = 1
    }

    public enum IndentationSizeMode
    {
        [Description("Uses the indentation size as determined by visual studio")]
        Auto = 0,
        [Description("Uses the indentation size specified in these extension settings")]
        Manual = 1,
    }


    internal static class DefaultRainbowIndentOptions
    {
        public const IndentationSizeMode defaultIndentationSizeMode = IndentationSizeMode.Auto;
        public const int defaultIndentSize = 4;
        public const string defaultFileExtensionsIndentSizes = "";
        public const string defaultColors = "#30e8e800,#3054cf2b,#30009fc7,#307a2ac9,#30db00db,#30cf0000,#30db9200";
        public const double defaultOpacityMultiplier = 1.0;
        public const HighlightingMode defaultHighlightingMode = HighlightingMode.Alternating;
        public const ColorMode defaultColorMode = ColorMode.Solid;
        public const bool defaultFadeColors = false;

        // Constants for errors
        public const string defaultErrorColor = "#40a80000";
        public const bool defaultDetectErrorsFlag = true;
    }
}
