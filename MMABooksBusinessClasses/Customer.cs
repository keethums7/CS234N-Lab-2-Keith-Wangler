using System;

namespace MMABooksBusinessClasses
{
    public class Customer
    {
        // there are several warnings in this file related to nullable properties and return values.
        // you can ignore them
        public Customer() { }

        public Customer(int id, string name, string address, string city, string state, string zipcode)
        {
            CustomerID = id;
            Name = name;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipcode;
        }

        // instance variables
        private int customerID;
        private string name;
        private string address;
        private string city;
        private string state;
        private string zipcode;

        public int CustomerID 
        {
            get
            {
                return customerID;
            }

            set
            {
                if (value > 0)
                {
                    customerID = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Customer ID must be a positive integer");
                }
            }
        }

        public string Name {
            get
            {
                return name;
            }
            set 
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 100)
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Customer name must be between 1 and 100 characters");
                }
            } 
        }

        public string Address {
            get
            {
                return address;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 50)
                {
                    address = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Customer address must be between 1 and 50 characters");
                }
            }
        }

        public string City { 
            get
            {
                return city;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 20)
                {
                    city = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Customer city must be between 1-20 characters.");
                }
            }
        }

        public string State { 
            get
            {
                return state;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 2)
                {
                    state = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Customer state must be between 1-2 characters (e.g. AR, IL, IA)");
                }
            }
        }

        public string ZipCode { 
            get
            {
                return zipcode;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 15)
                {
                    zipcode = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Customer zipcode must be between 1-15 characters.");
                }
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
