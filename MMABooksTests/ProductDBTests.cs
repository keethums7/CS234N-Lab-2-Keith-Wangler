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

            // update instance variable to match the first row item
            product.ProductCode = "A4CS";
            product.Description = "Murach's ASP.NET 4 Web Programming with C# 2010";
            product.UnitPrice = 56.5000m;
            product.OnHandQuantity = 4637;

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
        public void TestAddProduct()
        {
            // just changing the product code and price
            // to differ slightly from row 1's default
            // so we can confirm it was added
            product.ProductCode = "NEW1";
            product.UnitPrice = 13.37m;

            Assert.True(ProductDB.AddProduct(product));
        }
            
        [Test]
        public void TestUpdateProduct()
        {
            // create an empty product that doesn't match any existing rows
            Product product1 = new Product();

            // Create a test product to update an existing one to
            Product product2 = new Product();
            product2.ProductCode = "TEST";
            product2.Description = "TEST";
            product2.UnitPrice = 10.00m;
            product2.OnHandQuantity = 10;

            // UpdateProduct returns a bool, will be true
            // if the number of updated rows is 1
            Assert.True(ProductDB.UpdateProduct(product, product2));

            // We expect false here since there should be
            // no matching product, so # of updated rows is 0
            Assert.False(ProductDB.UpdateProduct(product1, product2));
        }

        [Test]
        public void TestDeleteProduct()
        {
            // create separate product to test that deletion fails
            // if there's no matching product to delete
            Product product1 = new Product();

            // DeleteProduct() returns a bool,
            // true if the # of deleted rows is 1
            Assert.True(ProductDB.DeleteProduct(product));

            // We expect false here since there should be no
            // matching rows to update, and # of updated rows will be 0
            Assert.False(ProductDB.DeleteProduct(product1));
        }
    }
}
