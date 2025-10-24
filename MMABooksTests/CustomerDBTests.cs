using System;
using System.Collections.Generic;
using System.Text;

using MMABooksBusinessClasses;
using MMABooksDBClasses;
using MySql.Data.MySqlClient;
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
            //c = new Customer();
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
            Customer customer = new Customer();
            customer = CustomerDB.GetCustomer(1);
            Console.WriteLine(customer);

            // Test that it won't delete
            // an invalid (empty) customer object
            Assert.False(CustomerDB.DeleteCustomer(c));

            // Test that a known existing
            // customer can be deleted
            Assert.True(CustomerDB.DeleteCustomer(customer));

            // Readd and update the customer to reset the change
            CustomerDB.AddCustomer(customer);
        }

        [Test]
        public void TestUpdateCustomer() { }
    }
}
