using System.IO;
using NUnit.Framework;
using Reevo.Unbroken.Extensions;

namespace Reevo.Unbroken.ExtensionsTest
{
    [TestFixture]
    public class FileInfoExtensionsTest
    {
        private FileInfo _fileNotExisted;
        private FileInfo _fileExisted;

        [SetUp]
        public void Initialize()
        {
            _fileNotExisted = new FileInfo("gitex.cmd");
            _fileExisted = new FileInfo(@"C:\Program Files (x86)\GitExtensions\gitex.cmd");
        }

        [Test]
        public void AssertFileNotExisted_ShouldNotExist()
        {
            var result = _fileNotExisted.Exists;
            Assert.IsFalse(result);
        }

        [Test]
        public void AssertFileExisted_ShouldExist()
        {
            var result = _fileExisted.Exists;
            Assert.IsTrue(result);
        }

        [Test]
        public void AssertExisted_ShouldExistsFileInEnvironmentVariable()
        {
            var result = _fileExisted.FileExistsOnEnvironmentPaths();
            Assert.IsTrue(result);
        }

        [Test]
        public void AssertNotExistedFile_ShouldExistsInEnvironmentVariable()
        {
            var result = _fileNotExisted.FileExistsOnEnvironmentPaths();
            Assert.IsTrue(result);
        }
    }
}
