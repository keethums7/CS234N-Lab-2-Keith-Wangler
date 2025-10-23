using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer def;
        private Customer c;
        
        [SetUp]

        // runs before every test, resets customer objects
        public void Setup()
        {
            def = new Customer();
            c = new Customer(1, "Donald, Duck", "101 Main Street", "Orlando", "FL", "10001");
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(def);
            Assert.AreEqual(null, def.Name); // confirm name isn't null
            Assert.AreEqual(null, def.Address);
            Assert.AreEqual(null, def.City);
            Assert.AreEqual(null, def.State);
            Assert.AreEqual(null, def.ZipCode);

            Assert.IsNotNull(c);
            Assert.AreNotEqual(null, c.Name);
            Assert.AreNotEqual(null, c.Address);
            Assert.AreNotEqual(null, c.City);
            Assert.AreNotEqual(null, c.State);
            Assert.AreNotEqual(null, c.ZipCode);
        }

        [Test]

        public void TestNameSetter()
        {
            c.Name = "Dasie, Duck";
            Assert.AreNotEqual("Donald, Duck", c.Name);
            Assert.AreEqual("Dasie, Duck", c.Name);
        }

        [Test]

        public void TestSettersNameTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Name = "23456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789");
        }
    }
}
