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
    public class ProductDBTests
    {
        private Product product;
        private List<Product> products;

        [SetUp]
        public void SetUp()
        {
            // reset testing instance variables
            product = new Product();
            products = new List<Product>();
        }

        [Test]
        public void TestGetProduct()
        {
            product = ProductDB.GetProduct("A4CS");
            Assert.AreEqual("A4CS", product.ProductCode);
        }

        [Test]
        public void TestGetProductList()
        {
            products = ProductDB.GetList();
            Assert.IsNotEmpty(products);
        }

        [Test]
        public void TestDeleteProduct()
        {
            // update instance variable to match the first row item
            product = new Product("A4CS", "Murach's ASP.NET 4 Web Programming with C# 2010", 56.5000m, 4637);

            // create separate product to test that deletion fails
            // if there's no matching product to delete
            Product product1 = new Product();

            // DeleteProduct() returns a bool,
            // true if the # of deleted rows is 1
            Assert.True(ProductDB.DeleteProduct(product));

            // We expect an error since there should be
            // no matching product
            Assert.Throws<MySqlException>(() => ProductDB.DeleteProduct(product1));
        }

        [Test]
        public void TestUpdateProduct()
        {
            // update instance variable to match the first row item
            product = new Product("A4CS", "Murach's ASP.NET 4 Web Programming with C# 2010", 56.5000m, 4637);

            // create an empty product that doesn't match any existing rows
            Product product1 = new Product();

            // Create a test product to update an existing one to
            Product product2 = new Product("TEST", "TEST", 10.00m, 10);

            // UpdateProduct returns a bool, will be true
            // if the number of updated rows is 1
            Assert.True(ProductDB.UpdateProduct(product, product2));

            // We expect an error since there should be
            // no matching product
            Assert.Throws<MySqlException>(() => ProductDB.UpdateProduct(product, product1));
        }
    }
}
