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
        private Customer customer;

        [SetUp]
        public void Setup()
        {
            c = new Customer();
            c.Name = "Mickey Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10101";

            Customer customer = new Customer();
        }

        [Test]
        public void TestGetCustomer()
        {
            // test getting the first customer
            // by first filling in a new empty
            // customer and comparing
            customer = CustomerDB.GetCustomer(1);
            Assert.AreEqual(1, customer.CustomerID);
        }

        [Test]
        public void TestCreateCustomer()
        {
            // setup gives us a default mickey mouse
            // customer to work with for testing
            int customerID = CustomerDB.AddCustomer(c);
            c = CustomerDB.GetCustomer(customerID);
            Assert.AreEqual("Mickey Mouse", c.Name);
        }

        [Test]
        public void TestDeleteCustomer() 
        {
            // Test that it won't delete
            // an invalid (empty) customer object
            Assert.Throws<NullReferenceException>(() => CustomerDB.DeleteCustomer(customer));

            // test that it must be an exact match, messing up fields
            // on an otherwise correct customer
            customer = CustomerDB.GetCustomer(1);
            customer.Name = "Testing";
            customer.Address = "123";
            Assert.False(CustomerDB.DeleteCustomer(customer));

            // Test that a known existing
            // customer can be deleted (1)
            customer = CustomerDB.GetCustomer(1);
            Assert.True(CustomerDB.DeleteCustomer(customer));
        }

        [Test]
        public void TestUpdateCustomer() {

            // Test with an empty (invalid) old customer
            // Expecting it not to update anything
            Assert.Throws<NullReferenceException>(() => CustomerDB.UpdateCustomer(customer, c));

            // We expect null here too, since this new customer
            // isn't a 100% match
            customer = CustomerDB.GetCustomer(2);
            Assert.False(CustomerDB.UpdateCustomer(c, customer));

            // use a test customer (mickey) to
            // update an existing customer (2)
            customer = CustomerDB.GetCustomer(2);
            Assert.True(CustomerDB.UpdateCustomer(customer, c));
        }
    }
}
