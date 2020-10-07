using IndentRainbow.Logic.Parser;
using NUnit.Framework;

namespace IndentRainbow.Logic.Tests.Parser
{
    [TestFixture]
    public class LanguageParserTests
    {
        [Test]
        [TestCase("cs:4;js,jsx:5;", "cs:4,js:5,jsx:5")]
        [TestCase("cs,js,jsx:5;css:4;html:2", "cs:5,js:5,jsx:5,css:4,html:2")]
        [TestCase("cs:a;32:32", "32:32")]
        [TestCase(",,,cs:3", "cs:3")]
        //Checking that null does not kill the method
        [TestCase(null, "")]
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

        [Test]
        [TestCase("cs:4;js:5;jsx:6;")]
        [TestCase("cs:5;")]
        [TestCase("")]
        public void ConvertDictionaryToString_ExpectedBehavior(string input)
        {
            var dictionary = LanguageParser.CreateDictionaryFromString(input);
            var result = LanguageParser.ConvertDictionaryToString(dictionary);
            Assert.AreEqual(input, result);
        }

    }
}
