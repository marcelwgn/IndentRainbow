using IndentRainbow.Logic.Classification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IndentRainbow.Logic.Tests.Classification
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    public class IndentValidatorTests
    {

        private IndentValidator validator;
        private const string FSI = "    ";
        private const string TABI = "\t";

        [TestInitialize]
        public void Setup()
        {
            validator = new IndentValidator(0);
        }

        [DataTestMethod]
        [DataRow("a", 1)]
        [DataRow("bb", 2)]
        [DataRow("ccc", 3)]
        [DataRow("", 0)]
        [DataRow(TABI, 1)]
        public void GetIndentBlockLengthTests_ExpectedBehaviors(string text, int length)
        {
            validator = new IndentValidator(text.Length);

            var result = validator.GetIndentBlockLength();

            Assert.AreEqual(length, result);
        }

        [DataTestMethod]
        [DataRow(FSI, false)]
        [DataRow(" d", true)]
        [DataRow("d", false)]
        [DataRow("   d", true)]
        [DataRow("   ", false)]
        [DataRow(TABI + TABI + "t", false)]
        [DataRow(TABI + TABI + " t", true)]
        [DataRow("te  ", false)]
        public void IsIncompleteIndentTests_ExpectedBehaviors(string text, bool isIncompleteIndent)
        {
            validator = new IndentValidator(FSI.Length);

            var result = validator.IsIncompleteIndent(text);

            Assert.AreEqual(isIncompleteIndent, result);
        }

        [DataTestMethod]
        [DataRow(FSI, true)]
        [DataRow(FSI + " ", false)]
        [DataRow(FSI + "d", false)]
        [DataRow("   d", false)]
        public void IsValidIndentTests_ExpectedBehaviors(string text, bool isValidIndent)
        {
            validator = new IndentValidator(FSI.Length);

            var result = validator.IsValidIndent(text);

            Assert.AreEqual(isValidIndent, result);
        }
    }
}
