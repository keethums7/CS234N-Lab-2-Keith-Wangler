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
                "SELECT * from Products";
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

        public static bool AddProduct(Product product)
        {
            // get a connection to the database
            MySqlConnection connection = MMABooksDB.GetConnection();
            string insertStatement =
                "INSERT Products " +
                "(ProductCode, Description, UnitPrice, OnHandQuantity) " +
                "VALUES (@ProductCode, @Description, @UnitPrice, @OnHandQuantity)";

            // set up the command object
            MySqlCommand insertCommand =
                new MySqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@ProductCode", product.ProductCode);
            insertCommand.Parameters.AddWithValue(
                "@Description", product.Description);
            insertCommand.Parameters.AddWithValue(
                "@UnitPrice", product.UnitPrice);
            insertCommand.Parameters.AddWithValue(
                "@OnHandQuantity", product.OnHandQuantity);

            try
            {
                // open the connection
                connection.Open();
                // execute the command
                insertCommand.ExecuteNonQuery();

                Product addedProduct = GetProduct(product.ProductCode);
                if (addedProduct.ProductCode.Equals(product.ProductCode))
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

        public static bool DeleteProduct(Product product)
        {
            // get a connection to the database
            MySqlConnection connection = MMABooksDB.GetConnection();

            // we need to clear the invoicelineitems table
            // of any rows that link the ProductCode, otherwise
            // we'll encounter a foreign key constraint error
            string preDeleteStatement =
                "DELETE FROM InvoiceLineItems " +
                "WHERE ProductCode = @ProductCode";

            string deleteStatement =
                "DELETE FROM Products " +
                "WHERE ProductCode = @ProductCode " +
                "AND Description = @Description " +
                "AND UnitPrice = @UnitPrice " +
                "AND OnHandQuantity = @OnHandQuantity ";


            // prep work
            MySqlCommand preDeleteCommand = 
                new MySqlCommand(preDeleteStatement, connection);
            preDeleteCommand.Parameters.AddWithValue(
                "@ProductCode", product.ProductCode);

            // set up the command object
            MySqlCommand deleteCommand =
                new MySqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue(
                "@ProductCode", product.ProductCode);
            deleteCommand.Parameters.AddWithValue(
                "@Description", product.Description);
            deleteCommand.Parameters.AddWithValue(
                "@UnitPrice", product.UnitPrice);
            deleteCommand.Parameters.AddWithValue(
                "@OnHandQuantity", product.OnHandQuantity);

            try
            {
                // open the connection
                connection.Open();
                // execute the pre-command
                int preDeletedRows = preDeleteCommand.ExecuteNonQuery();

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
            Product newProduct)
        {
            // create a connection
            MySqlConnection connection = MMABooksDB.GetConnection();
            string updateStatement =
                "UPDATE Products SET " +
                "Description = @NewDescription, " +
                "UnitPrice = @NewUnitPrice, " +
                "OnHandQuantity = @NewOnHandQuantity " +
                "WHERE ProductCode = @OldProductCode " +
                "AND Description = @OldDescription " +
                "AND UnitPrice = @OldUnitPrice " +
                "AND OnHandQuantity = @OldOnHandQuantity";

            // setup the command object
            MySqlCommand updateCommand =
                new MySqlCommand(updateStatement, connection);


            // Add new product info to params
            updateCommand.Parameters.AddWithValue(
                "@NewProductCode", newProduct.ProductCode);
            updateCommand.Parameters.AddWithValue(
                "@NewDescription", newProduct.Description);
            updateCommand.Parameters.AddWithValue(
                "@NewUnitPrice", newProduct.UnitPrice);
            updateCommand.Parameters.AddWithValue(
                "@NewOnHandQuantity", newProduct.OnHandQuantity);

            // Add old customer info to params
            updateCommand.Parameters.AddWithValue(
                "@OldProductCode", oldProduct.ProductCode);
            updateCommand.Parameters.AddWithValue(
                "@OldDescription", oldProduct.Description);
            updateCommand.Parameters.AddWithValue(
                "@OldUnitPrice", oldProduct.UnitPrice);
            updateCommand.Parameters.AddWithValue(
                "@OldOnHandQuantity", oldProduct.OnHandQuantity);

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
