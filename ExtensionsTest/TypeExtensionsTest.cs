using System;
using System.Windows.Forms;
using NUnit.Framework;
using Reevo.Unbroken.Extensions;

namespace Reevo.Unbroken.ExtensionsTest
{
    [TestFixture]
    public class TypeExtensionsTest
    {
        [Test]
        public void IsSimpleTypeTest()
        {
            // Test for simple primitive types
            Assert.IsTrue(typeof(string).IsSimpleType());
            Assert.IsTrue(typeof(bool).IsSimpleType());
            Assert.IsTrue(typeof(char).IsSimpleType());
            Assert.IsTrue(typeof(byte).IsSimpleType());
            Assert.IsTrue(typeof(short).IsSimpleType());
            Assert.IsTrue(typeof(int).IsSimpleType());
            Assert.IsTrue(typeof(long).IsSimpleType());
            Assert.IsTrue(typeof(float).IsSimpleType());
            Assert.IsTrue(typeof(double).IsSimpleType());
            Assert.IsTrue(typeof(decimal).IsSimpleType());
            Assert.IsTrue(typeof(sbyte).IsSimpleType());
            Assert.IsTrue(typeof(ushort).IsSimpleType());
            Assert.IsTrue(typeof(uint).IsSimpleType());
            Assert.IsTrue(typeof(ulong).IsSimpleType());

            // Test for some types that considered as simple types
            Assert.IsTrue(typeof(Guid).IsSimpleType());
            Assert.IsTrue(typeof(Enum).IsSimpleType());
            Assert.IsTrue(typeof(DateTime).IsSimpleType());
            Assert.IsTrue(typeof(DateTimeOffset).IsSimpleType());
            Assert.IsTrue(typeof(TimeSpan).IsSimpleType());

            // Test for custom types
            Assert.IsFalse(typeof(TestClass1).IsSimpleType());
            Assert.IsTrue(typeof(TestStruct1).IsSimpleType());
            Assert.IsTrue(typeof(TestStruct2).IsSimpleType());
            Assert.IsFalse(typeof(TestStruct3).IsSimpleType());

            // Test for Nullable<> for simple primitive types
            Assert.IsTrue(typeof(bool?).IsSimpleType());
            Assert.IsTrue(typeof(char?).IsSimpleType());
            Assert.IsTrue(typeof(byte?).IsSimpleType());
            Assert.IsTrue(typeof(short?).IsSimpleType());
            Assert.IsTrue(typeof(int?).IsSimpleType());
            Assert.IsTrue(typeof(long?).IsSimpleType());
            Assert.IsTrue(typeof(float?).IsSimpleType());
            Assert.IsTrue(typeof(double?).IsSimpleType());
            Assert.IsTrue(typeof(decimal?).IsSimpleType());
            Assert.IsTrue(typeof(sbyte?).IsSimpleType());
            Assert.IsTrue(typeof(ushort?).IsSimpleType());
            Assert.IsTrue(typeof(uint?).IsSimpleType());
            Assert.IsTrue(typeof(ulong?).IsSimpleType());

            // Test for Nullable<> for types that are considered as simple types
            Assert.IsTrue(typeof(Guid?).IsSimpleType());
            Assert.IsTrue(typeof(DateTime?).IsSimpleType());
            Assert.IsTrue(typeof(DateTimeOffset?).IsSimpleType());
            Assert.IsTrue(typeof(TimeSpan?).IsSimpleType());

            // Test for Nullable<> for custom struct
            Assert.IsTrue(typeof(TestStruct1?).IsSimpleType());
            Assert.IsTrue(typeof(TestStruct2?).IsSimpleType());
            Assert.IsFalse(typeof(TestStruct3?).IsSimpleType());
        }

        private class TestClass1
        {
            public string Prop1 { get; set; }
            public int Prop2 { get; set; }

            public DateTime Prop3 { get; set; }

            public Day Prop4 { get; set; }
        }

        private struct TestStruct1
        {
            public string Prop1 { get; set; }
            public int Prop2 { get; set; }
        }

        private struct TestStruct2
        {
            public string Prop1 { get; set; }
            public int Prop2 { get; set; }

            public TestStruct1 Prop3 { get; set; }
        }

        private struct TestStruct3
        {
            public string Prop1 { get; set; }
            public int Prop2 { get; set; }

            public TestClass1 Prop3 { get; set; }
        }        
    }
}