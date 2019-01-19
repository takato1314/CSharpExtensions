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
        public void FilterTest()
        {
            
        }
    }
}