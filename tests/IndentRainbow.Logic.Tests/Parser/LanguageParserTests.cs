using IndentRainbow.Logic.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IndentRainbow.Logic.Tests.Parser
{
    [TestClass]
    public class LanguageParserTests
    {
        [DataTestMethod]
        [DataRow("cs:4;js,jsx:5;", "cs:4,js:5,jsx:5")]
        [DataRow("cs,js,jsx:5;css:4;html:2", "cs:5,js:5,jsx:5,css:4,html:2")]
        [DataRow("cs:a;32:32", "32:32")]
        [DataRow(",,,cs:3", "cs:3")]
        //Checking that null does not kill the method
        [DataRow(null, "")]
        public void CreateDictionaryFromString_ExpectedBehavior(string input, string queriesToCheck)
        {
            var dictionary = LanguageParser.CreateDictionaryFromString(input);
            var split = queriesToCheck.Split(',');
            foreach (var entry in split)
            {
                var entrySplit = entry.Split(':');
                if (entrySplit.Length == 1)
                {
                    return;
                }
                var indentation = int.Parse(entrySplit[1]);
                Assert.AreEqual(dictionary[entrySplit[0]], indentation);
            }
        }

        [DataTestMethod]
        [DataRow("cs:4;js:5;jsx:6;")]
        [DataRow("cs:5;")]
        [DataRow("")]
        public void ConvertDictionaryToString_ExpectedBehavior(string input)
        {
            var dictionary = LanguageParser.CreateDictionaryFromString(input);
            var result = LanguageParser.ConvertDictionaryToString(dictionary);
            Assert.AreEqual(input, result);
        }

    }
}
