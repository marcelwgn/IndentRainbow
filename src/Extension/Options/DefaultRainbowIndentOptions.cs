using System.ComponentModel;

namespace IndentRainbow.Extension.Options
{

    public enum HighlightingMode
    {
        [Description("Alternating colors")]
        Alternating = 0,
        [Description("One color")]
        Monocolor = 1
    }

    internal static class DefaultRainbowIndentOptions
    {
        public const int defaultIndentSize = 4;
        public const string defaultFileExtensionsIndentSizes = "";
        public const string defaultColors = "#40FFFF00,#4066FF33,#4000CCFF,#409933FF,#40FF00FF,#40FF0000,#40FFAA00";
        public const double defaultOpacityMultiplier = 1.0;
        public const HighlightingMode defaultHighlightingMode = HighlightingMode.Alternating;

        // Constants for errors
        public const string defaultErrorColor = "#40a80000";
        public const bool defaultDetectErrorsFlag = true;
    }
}
