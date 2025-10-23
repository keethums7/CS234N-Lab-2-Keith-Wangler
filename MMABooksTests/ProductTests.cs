using NUnit.Framework;
using MMABooksBusinessClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {
        private Product product;
        private Product product1;
        private Product product2;

        [SetUp]

        public void Setup()
        {
            product = new Product();
            product1 = new Product("testGizmo1", "Test Gizmo 1", 25.00m, 25);
        }

        [Test]
        public void TestProductEmptyConstructor()
        {
            // check that the product object was created during setup
            Assert.IsNotNull(product);
            // check that it didn't instantiate anything property-wise
            Assert.IsNull(product.ProductCode);
            Assert.IsNull(product.Description);
            
            // Had to confirm what the expected value would be before devising unit tests.
            // Console.WriteLine($"product.UnitPrice evaluates to: {product.UnitPrice}");
            // Console.WriteLine($"product.OnHandQuantity evaluates to: {product.OnHandQuantity}");

            Assert.Zero(product.UnitPrice);
            Assert.Zero(product.OnHandQuantity);
        }

        [Test]
        public void TestProductDefaultConstructor()
        {
            Assert.IsNotNull(product1);
            Assert.AreEqual("testGizmo1", product1.ProductCode);
            Assert.AreEqual("Test Gizmo 1", product1.Description);
            Assert.AreEqual(25.00m, product1.UnitPrice);
            Assert.AreEqual(25, product1.OnHandQuantity);

            string newProductCode = "testGizmo1";
            string newDescription = "Test Gizmo 1";
            decimal newUnitPrice = 25.00m;
            int newOnHandQuantity = 25;

            product2 = new Product(newProductCode, newDescription, newUnitPrice, newOnHandQuantity);
            Assert.IsNotNull(product2);
            Assert.AreEqual(newProductCode, product2.ProductCode);
            Assert.AreEqual(newDescription, product2.Description);
            Assert.AreEqual(newUnitPrice, product2.UnitPrice);
            Assert.AreEqual(newOnHandQuantity, product2.OnHandQuantity);
        }

        [Test]
        public void TestProductToString()
        {
            // needed to see what these even evaluate to before devising tests
            Console.WriteLine(product1);

            Assert.IsTrue(product1.ToString().Contains("testGizmo1"));
            Assert.IsTrue(product1.ToString().Contains("Test Gizmo 1"));
            Assert.IsTrue(product1.ToString().Contains("UnitPrice: 25.00"));
            Assert.IsTrue(product1.ToString().Contains("OnHandQuantity: 25"));
        }
    }
}
