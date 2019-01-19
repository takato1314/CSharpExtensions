using NUnit.Framework;
using Reevo.Unbroken.Extensions;

namespace Reevo.Unbroken.ExtensionsTest
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void TestRegexString_MatchesRegex()
        {
            // Arrange
            var matchingString = "This is some matching string, with a bang.";

            // Act
            var result = matchingString.Matches(@"([\W]+)");

            // Assert
            Assert.IsTrue(result.Count == 8);
            Assert.AreEqual(", ", result[4].Value);
            Assert.AreEqual(".", result[7].Value);
        }
    }
}
