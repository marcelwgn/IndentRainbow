using IndentRainbow.Logic.Classification;
using NUnit.Framework;

namespace IndentRainbow.Logic.Tests.Classification
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    public class IndentValidatorTests
    {

        private IndentValidator validator;
        private const string FSI = "    ";
        private const string TABI = "\t";

        [SetUp]
        public void Setup()
        {
            validator = new IndentValidator(0);
        }

        [Test]
        [TestCase("a", 1)]
        [TestCase("bb", 2)]
        [TestCase("ccc", 3)]
        [TestCase("", 0)]
        [TestCase(TABI, 1)]
        public void GetIndentBlockLengthTests_ExpectedBehaviors(string text, int length)
        {
            validator = new IndentValidator(text.Length);

            var result = validator.GetIndentBlockLength();

            Assert.AreEqual(length, result);
        }

        [Test]
        [TestCase(FSI, false)]
        [TestCase(" d", true)]
        [TestCase("d", false)]
        [TestCase("   d", true)]
        [TestCase("   ", false)]
        [TestCase(TABI + TABI + "t", false)]
        [TestCase(TABI + TABI + " t", true)]
        [TestCase("te  ", false)]
        public void IsIncompleteIndentTests_ExpectedBehaviors(string text, bool isIncompleteIndent)
        {
            validator = new IndentValidator(FSI.Length);

            var result = validator.IsIncompleteIndent(text);

            Assert.AreEqual(isIncompleteIndent, result);
        }

        [Test]
        [TestCase(FSI, true)]
        [TestCase(FSI + " ", false)]
        [TestCase(FSI + "d", false)]
        [TestCase("   d", false)]
        public void IsValidIndentTests_ExpectedBehaviors(string text, bool isValidIndent)
        {
            validator = new IndentValidator(FSI.Length);

            var result = validator.IsValidIndent(text);

            Assert.AreEqual(isValidIndent, result);
        }
    }
}
