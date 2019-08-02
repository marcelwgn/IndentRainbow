using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            string[] entries = input.Split(';');
            foreach (string s in entries)
            {
                try
                {
                    string[] splittedData = s.Split(':');
                    string[] fileExtensions = splittedData[0].Split(',');
                    int indentationSize = Int32.Parse(splittedData[1]);
                    foreach (string fileExtension in fileExtensions)
                    {
                        try
                        {
                            dictionary.Add(fileExtension, indentationSize);
                        } catch (Exception) { }
                    }

                } catch (Exception) { }
            }
            return dictionary;
        }

        /// <summary>
        /// Converts the given the dictionary into a string
        /// </summary>
        /// <param name="dictionary">The dictionary to convert</param>
        /// <returns>The string "representation" of the dictionary</returns>
        public static String ConvertDictionaryToString(Dictionary<string,int> dictionary)
        {
            string result = "";
            foreach(string key in dictionary.Keys)
            {
                result += key + ":" + dictionary[key] + ";";
            }
            return result;
        }
    }
}
