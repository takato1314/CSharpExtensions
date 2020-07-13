using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Reevo.Unbroken.Extensions;

namespace Reevo.Unbroken.ExtensionsTest
{
    [TestFixture]
    public class ListExtensionsTest
    {
        [Test]
        public void RemoveAll_CalledWithMultipleTargets_RemovesThemAll()
        {
            var l = new List<int> {2, 3, 4, 5, 6};

            var amountRemoved = l.RemoveAll(x => (x%2) == 0);

            Assert.AreEqual(amountRemoved, 3);
            Assert.IsTrue(l.SequenceEqual(new List<int> {3, 5}));
        }

        [Test]
        public void RemoveFirst_CalledWithTwoTargets_RemovesOnlyFirst()
        {
            var l = new List<int> {4, 2, 3, 4, 5, 6};

            var didRemove = l.RemoveFirst(x => x == 4);

            Assert.IsTrue(didRemove);
            Assert.IsTrue(l.SequenceEqual(new List<int> {2, 3, 4, 5, 6}));
        }

        [Test]
        public void RemoveFirst_CalledWithNoTargets_RemovesNothing()
        {
            var l = new List<int> {2, 3, 5, 6};

            var didRemove = l.RemoveFirst(x => x == 4);

            Assert.IsFalse(didRemove);
            Assert.IsTrue(l.SequenceEqual(new List<int> {2, 3, 5, 6}));
        }

        [Test]
        public void AddDistinct_AddUniqueStringItem_WillAddIntoList()
        {
            // Arrange
            var item = "Deus";
            var list = new List<string>{ "Alex", "Bob", "Cynthia" };

            // Act
            list.AddDistinct(item);

            // Assert
            var count = list.Count(_ => _.Equals(item, StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(1, count);
        }

        [Test]
        public void AddDistinct_AddDuplicateStringItem_WontAddIntoList()
        {
            // Arrange
            var item = "alex";
            var list = new List<string> { "Alex", "Bob", "Cynthia" };

            // Act
            list.AddDistinct(item);

            // Assert
            var count = list.Count(_ => _.Equals(item, StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(1, count);
        }

        [Test, Ignore("Require to implement custom Comparer")]
        public void AddDistinct_AddDuplicateComplexTestObjectItem_WontAddIntoList()
        {
            // Arrange
            var item = "alex";
            var list = new List<TestObject>
            {
                new TestObject { StringProperty = "Alex" },
                new TestObject { StringProperty = "Bob" },
                new TestObject { StringProperty = "Cynthia" }
            };

            // Act
            list.AddDistinct(new TestObject {StringProperty = item});

            // Assert
            var count = list.Count(_ => _.StringProperty.Equals(item, StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(1, count);
        }
    }
}