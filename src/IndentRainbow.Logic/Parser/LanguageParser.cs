using System;
using System.Collections.Generic;

namespace IndentRainbow.Logic.Parser
{
    public static class LanguageParser
    {
        /// <summary>
        /// Returns a dictionary which is created by parsing the string
        /// </summary>
        /// <param name="input">The input string containing the fileextensions and the indentsizes</param>
        /// <returns>The filled dictionary</returns>
        public static Dictionary<string, int> CreateDictionaryFromString(string input)
        {
            var dictionary = new Dictionary<string, int>();
            if (input is null)
            {
                return dictionary;
            }
            var entries = input.Split(';');
            foreach (var s in entries)
            {
                try
                {
                    var splittedData = s.Split(':');
                    if (splittedData.Length < 2)
                    {
                        continue;
                    }
                    var fileExtensions = splittedData[0].Split(',');
                    var indentationSize = int.Parse(splittedData[1], System.Globalization.CultureInfo.InvariantCulture);
                    foreach (var fileExtension in fileExtensions)
                    {
                        try
                        {
                            dictionary.Add(fileExtension, indentationSize);
                        }
                        catch (ArgumentException) { }
                    }

                }
                catch (FormatException) { }
                catch (OverflowException) { }
            }
            return dictionary;
        }

        /// <summary>
        /// Converts the given the dictionary into a string
        /// </summary>
        /// <param name="dictionary">The dictionary to convert</param>
        /// <returns>The string "representation" of the dictionary</returns>
        public static string ConvertDictionaryToString(Dictionary<string, int> dictionary)
        {
            var result = "";
            foreach (var key in dictionary?.Keys)
            {
                result += key + ":" + dictionary[key] + ";";
            }
            return result;
        }
    }
}
