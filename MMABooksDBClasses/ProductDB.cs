using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data.MySqlClient;
using System.Data;
using MMABooksBusinessClasses;

namespace MMABooksDBClasses
{
    public static class ProductDB
    {
        public static List<Product> GetList()
        {
            // instantiate an empty list, to append our products to
            List<Product> products = new List<Product>();

            // create connection object from MMABooksDB class
            MySqlConnection connection = MMABooksDB.GetConnection();

            // build our query string and use it to build command object
            string selectListStatement =
                "SELECT * from Customers";
            MySqlCommand selectListCommand = new MySqlCommand(selectListStatement, connection);

            try
            {
                connection.Open();
                MySqlDataReader prodReader =
                    selectListCommand.ExecuteReader();

                // DataReader object .Read() returns true while another row
                // is available to iterate through, keep looping until it
                // returns false
                while (prodReader.Read())
                {
                    // create the placeholder object
                    Product product = new Product();

                    // port over the properties to it from the
                    // reader object's current row
                    product.ProductCode = prodReader["ProductCode"].ToString();
                    product.Description = prodReader["Description"].ToString();
                    product.UnitPrice = (decimal)prodReader["UnitPrice"];
                    product.OnHandQuantity = (int)prodReader["OnHandQuantity"];

                    // finally add the placeholder
                    // product object to the list
                    products.Add(product);
                }
                prodReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // safely close the connection
                connection.Close();
            }
            return products;
        }

        public static Product GetProduct(string productCode)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement =
                "SELECT ProductCode, Description, UnitPrice, OnHandQuantity " +
                "FROM Products " +
                "WHERE ProductCode = @ProductCode";
            MySqlCommand selectCommand =
                new MySqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                connection.Open();
                MySqlDataReader prodReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (prodReader.Read())
                {
                    Product product = new Product();
                    product.ProductCode = prodReader["ProductCode"].ToString();
                    product.Description = prodReader["Description"].ToString();
                    product.UnitPrice = (decimal)prodReader["UnitPrice"];
                    product.OnHandQuantity = (int)prodReader["OnHandQuantity"];

                    // safely close the reader
                    prodReader.Close();
                    return product;
                }
                else
                {
                    // .Read() returned false
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                // safely close the connection
                connection.Close();
            }
        }

        public static bool DeleteProduct(Product product)
        {
            // get a connection to the database
            MySqlConnection connection = MMABooksDB.GetConnection();

            string deleteStatement =
                "DELETE FROM Products " +
                "WHERE ProductCode = @ProductCode " +
                "AND Description = @Description " +
                "AND UnitPrice = @UnitPrice " +
                "AND OnHandQuantity = @OnHandQuantity "

            // set up the command object
            MySqlCommand deleteCommand =
                new MySqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue(
                "@ProductCode", product.ProductCode);
            deleteCommand.Parameters.AddWithValue(
                "@Name", product.Description);
            deleteCommand.Parameters.AddWithValue(
                "@UnitPrice", product.UnitPrice);
            deleteCommand.Parameters.AddWithValue(
                "@OnHandQuantity", product.OnHandQuantity);

            try
            {
                // open the connection
                connection.Open();
                // execute the command
                int deletedRows = deleteCommand.ExecuteNonQuery();
                // if the number of records returned = 1, return true otherwise return false
                if (deletedRows == 1)
                {
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // throw the exception
                throw ex;
            }
            finally
            {
                // close the connection
                connection.Close();
            }
            // only return false if the deletedRows didn't return 1 earlier
            return false;
        }

        public static bool UpdateProduct(Product oldProduct,
            Customer newProduct)
        {
            // create a connection
            MySqlConnection connection = MMABooksDB.GetConnection();
            string updateStatement =
                "UPDATE Customers SET " +
                "Name = @NewName, " +
                "Address = @NewAddress, " +
                "City = @NewCity, " +
                "State = @NewState, " +
                "ZipCode = @NewZipCode " +
                "WHERE CustomerID = @OldCustomerID " +
                "AND Name = @OldName " +
                "AND Address = @OldAddress " +
                "AND City = @OldCity " +
                "AND State = @OldState " +
                "AND ZipCode = @OldZipCode";
            // setup the command object
            MySqlCommand updateCommand =
                new MySqlCommand(updateStatement, connection);


            // Add new customer info to params
            updateCommand.Parameters.AddWithValue(
                "@NewName", newCustomer.Name);
            updateCommand.Parameters.AddWithValue(
                "@NewAddress", newCustomer.Address);
            updateCommand.Parameters.AddWithValue(
                "@NewCity", newCustomer.City);
            updateCommand.Parameters.AddWithValue(
                "@NewState", newCustomer.State);
            updateCommand.Parameters.AddWithValue(
                "@NewZipCode", newCustomer.ZipCode);

            // Add old customer info to params
            updateCommand.Parameters.AddWithValue(
                "@OldCustomerID", oldCustomer.CustomerID);
            updateCommand.Parameters.AddWithValue(
                "@OldName", oldCustomer.Name);
            updateCommand.Parameters.AddWithValue(
                "@OldAddress", oldCustomer.Address);
            updateCommand.Parameters.AddWithValue(
                "@OldCity", oldCustomer.City);
            updateCommand.Parameters.AddWithValue(
                "@OldState", oldCustomer.State);
            updateCommand.Parameters.AddWithValue(
                "@OldZipCode", oldCustomer.ZipCode);

            try
            {
                // open the connection
                connection.Open();
                // execute the command
                int updatedRows = updateCommand.ExecuteNonQuery();
                // if the number of records returned = 1, return true otherwise return false
                if (updatedRows == 1)
                {
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // throw the exception
                throw ex;
            }
            finally
            {
                // close the connection
                connection.Close();
            }

            return false;
        }
    }
}
