using AutoMoq;
using IndentRainbow.Logic.Classification;
using NUnit.Framework;

namespace IndentRainbow.LogicTests.Classification
{
    [TestFixture]
    public class IndentValidatorTests
    {

        private readonly AutoMoqer mocker = new AutoMoqer();
        private IndentValidator validator;
        private const string FSI = "    ";
        private const string TABI = "\t";

        [SetUp]
        public void Setup()
        {
            this.validator = new IndentValidator(0);
        }

        [Test]
        [TestCase("a", 1)]
        [TestCase("bb", 2)]
        [TestCase("ccc", 3)]
        [TestCase("", 0)]
        [TestCase(TABI, 1)]
        public void GetIndentBlockLengthTests_ExpectedBehaviors(string text, int length)
        {
            this.validator = new IndentValidator(text.Length);

            var result = this.validator.GetIndentBlockLength();

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
            this.validator = new IndentValidator(FSI.Length);

            var result = this.validator.IsIncompleteIndent(text);

            Assert.AreEqual(isIncompleteIndent, result);
        }

        [Test]
        [TestCase(FSI, true)]
        [TestCase(FSI + " ", false)]
        [TestCase(FSI + "d", false)]
        [TestCase("   d", false)]
        public void IsValidIndentTests_ExpectedBehaviours(string text, bool isValidIndent)
        {
            this.validator= new IndentValidator(FSI.Length);

            var result = this.validator.IsValidIndent(text);

            Assert.AreEqual(isValidIndent, result);
        }
    }
}
