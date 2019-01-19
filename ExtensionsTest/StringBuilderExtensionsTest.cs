using System.Text;
using NUnit.Framework;
using Reevo.Unbroken.Extensions;

namespace Reevo.Unbroken.ExtensionsTest
{
    [TestFixture]
    public class StringBuilderExtensionsTest
    {
        private StringBuilder _sb;

        [SetUp]
        public void Initialize()
        {
            _sb = new StringBuilder();
            _sb.Clear();
        }

        [Test]
        public void TestStringBuilder_Prepend()
        {
            var endLine = "blablabla";
            var startLine = "Something";

            _sb.Append(endLine);
            _sb.Prepend(startLine);

            var sb = new StringBuilder();
            sb.Append(startLine);
            sb.Append(endLine);

            Assert.AreEqual(sb.ToString(), _sb.ToString());
        }

        [Test]
        public void TestStringBuilder_PrependLine()
        {
            var endLine = "blablabla";
            var startLine = "Something";

            _sb.Append(endLine);
            _sb.PrependLine(startLine);

            var sb = new StringBuilder();
            sb.AppendLine(startLine);
            sb.Append(endLine);

            Assert.AreEqual(sb.ToString(), _sb.ToString());
        }
    }
}
