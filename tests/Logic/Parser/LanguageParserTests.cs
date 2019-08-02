using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndentRainbow.Logic.Parser;
using NUnit.Framework;

namespace IndentRainbow.LogicTests.Parser
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
        public void CreateDictionaryFromString_ExpectedBehaviour(String input, String queriesToCheck)
        {
            var dictionary = LanguageParser.CreateDictionaryFromString(input);
            string[] split = queriesToCheck.Split(',');
            foreach (string entry in split)
            {
                string[] entrySplit = entry.Split(':');
                if (entrySplit.Length == 1)
                {
                    return;
                }
                int indentation = Int32.Parse(entrySplit[1]);
                Assert.AreEqual(dictionary[entrySplit[0]], indentation);
            }
        }

        [Test]
        [TestCase("cs:4;js:5;jsx:6;")]
        [TestCase("cs:5;")]
        [TestCase("")]
        public void ConvertDictionaryToString_ExpectedBehaviour(String input)
        {
            var dictionary = LanguageParser.CreateDictionaryFromString(input);
            string result = LanguageParser.ConvertDictionaryToString(dictionary);
            Assert.AreEqual(input, result);
        }

    }
}
