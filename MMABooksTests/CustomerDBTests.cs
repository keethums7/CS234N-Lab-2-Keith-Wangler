using System;
using System.Collections.Generic;
using System.Text;

using MMABooksBusinessClasses;
using MMABooksDBClasses;
using NUnit.Framework;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerDBTests
    {
        private Customer c;

        [SetUp]
        public void Setup()
        {
            c = new Customer();
        }

        [Test]
        public void TestGetCustomer()
        {
            c = CustomerDB.GetCustomer(1);
            Assert.AreEqual(1, c.CustomerID);
        }

        [Test]
        public void TestCreateCustomer()
        {
            c.Name = "Mickey Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10101";

            int customerID = CustomerDB.AddCustomer(c);
            c = CustomerDB.GetCustomer(customerID);
            Assert.AreEqual("Mickey Mouse", c.Name);
        }

        [Test]
        public void TestDeleteCustomer() 
        {

        }

        [Test]
        public void TestUpdateCustomer() { }
    }
}
