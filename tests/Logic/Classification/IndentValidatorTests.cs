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
        private const string FOUR_SPACE_INDENT = "    ";


        [SetUp]
        public void Setup()
        {
            this.validator = this.mocker.Create<IndentValidator>();
        }

        [Test]
        [TestCase("a", 1)]
        [TestCase("bb", 2)]
        [TestCase("ccc", 3)]
        [TestCase("", 0)]
        [TestCase("\t", 1)]
        public void GetIndentBlockLengthTests_ExpectedBehaviors(string text, int length)
        {
            this.validator.SetIndentLevel(text);

            var result = this.validator.GetIndentBlockLength();

            Assert.AreEqual(length, text.Length);
        }

        [Test]
        [TestCase(FOUR_SPACE_INDENT, false)]
        [TestCase(" d", true)]
        [TestCase("d", false)]
        [TestCase("   d", true)]
        public void IsIncompleteIndentTests_ExpectedBehaviors(string text, bool isIncompleteIndent)
        {
            this.validator.SetIndentLevel(FOUR_SPACE_INDENT);

            var result = this.validator.IsIncompleteIndent(text);

            Assert.AreEqual(isIncompleteIndent, result);
        }

        [Test]
        [TestCase(FOUR_SPACE_INDENT, true)]
        [TestCase(FOUR_SPACE_INDENT + " ", false)]
        [TestCase(FOUR_SPACE_INDENT + "d", false)]
        [TestCase("   d", false)]
        public void IsValidIndentTests_ExpectedBehaviours(string text, bool isValidIndent)
        {
            this.validator.SetIndentLevel(FOUR_SPACE_INDENT);

            var result = this.validator.IsValidIndent(text);

            Assert.AreEqual(isValidIndent, result);
        }

        [Test]
        [TestCase(FOUR_SPACE_INDENT, 4)]
        [TestCase("  ", 2)]
        [TestCase(FOUR_SPACE_INDENT + FOUR_SPACE_INDENT, 8)]
        [TestCase("\t", 1)]
        public void SetIndentLevelTests_ExpectedBehaviors(string indentation, int indentLength)
        {
            this.validator.SetIndentLevel(indentation);

            var result = this.validator.GetIndentBlockLength();
            Assert.AreEqual(indentLength, result);
        }
    }
}
