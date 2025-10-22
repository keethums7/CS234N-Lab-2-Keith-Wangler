using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MMABooksBusinessClasses
{
    public class Product
    {
        public Product() { }

        public Product(string productCode, string description, decimal unitPrice, int onhandQuantity)
        {
            ProductCode = productCode;
            Description = description;
            UnitPrice = unitPrice;
            OnHandQuantity = onHandQuantity;
        }

        private string productCode;
        private string description;
        private decimal unitPrice;
        private int onHandQuantity;

        public string ProductCode
        {
            get 
            { 
                return productCode; 
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 10)
                {
                    productCode = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Product code must be a string between 1-10 characters in length");
                }
            }
        }

        public string Description
        {
            get 
            { 
                return description; 
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                {
                    description = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Description must be a string between 1-50 characters in length.");
                }
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                // test and confirm unit price falls under 10 characters (column size)
                // and that the unit price is 0.00 (free) or more
                if (value.ToString().Length > 0 && value.ToString().Length <= 10 && value >= 0)
                {
                    unitPrice = value;
                }
            }
        }

        public int OnHandQuantity
        {
            get
            {
                return onHandQuantity;
            }
            set
            {
                // test and confirm unit price falls under 10 characters (column size)
                // and that the unit price is 0.00 (free) or more
                if (value.ToString().Length > 0 && value.ToString().Length <= 10 && value >= 0)
                {
                    onHandQuantity = value;
                }
            }
        }
    }
}
