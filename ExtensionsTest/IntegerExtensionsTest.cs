using NUnit.Framework;
using Reevo.Unbroken.Extensions;

namespace Reevo.Unbroken.ExtensionsTest
{
    [TestFixture]
    public class IntegerExtensionsTest
    {
        [Test]
        public void UpTo_Test()
        {
            var data = new int[7];
            var result = new[] {6, 7, 8, 9, 10, 11, 12};

            int j = 0;
            6.UpTo(12).ForEach(i => { data[j++] = i; });

            Assert.AreEqual(result, data);   // AreEqual
        }
    }
}
