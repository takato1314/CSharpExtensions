using System;
using System.IO;
using NUnit.Framework;
using Reevo.Unbroken.Extensions;
using Assert = NUnit.Framework.Assert;

namespace Reevo.Unbroken.ExtensionsTest
{
    [TestFixture]
    public class DirectoryExtensionTest
    {
        private static DirectoryInfo _dirNotExisted = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InvalidDir"));
        private static DirectoryInfo _dirExisted = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SomeDir"));
        private static readonly string FileExisted = Path.Combine(_dirExisted.FullName, "SomeText.txt");

        [SetUp]
        public void SetUp()
        {
            if (!Directory.Exists(_dirExisted.FullName))
            {
                _dirExisted = Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SomeDir"));
                var fs = File.Create(FileExisted);
                fs.Close();
            }         
        }

        [Test]
        public void TestStaticDeleteReadOnlyDirectory_ValidDir_ShouldDelete()
        {
            DirectoryExtensions.DeleteReadOnlyDirectory(_dirExisted.FullName);
            Assert.IsFalse(Directory.Exists(_dirExisted.FullName));
        }
        
        [Test]
        public void TestDeleteReadOnlyDirectory_ValidDir_ShouldDelete()
        {
            _dirExisted.DeleteReadOnlyDirectory();
            Assert.IsFalse(Directory.Exists(_dirExisted.FullName));
        }

        [Test]
        public void TestStaticDeleteReadOnlyDirectory_InvalidDir_ShouldThrowException()
        {
            Assert.Throws<DirectoryNotFoundException>(() => DirectoryExtensions.DeleteReadOnlyDirectory(_dirNotExisted.FullName));
        }

        [Test]
        public void TestDeleteReadOnlyDirectory_InvalidDir_ShouldThrowException()
        {
            Assert.Throws<DirectoryNotFoundException>(() => _dirNotExisted.DeleteReadOnlyDirectory());
        }
    }
}
