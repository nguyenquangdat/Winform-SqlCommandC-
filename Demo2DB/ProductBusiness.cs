using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Demo2DB
{
    class Product
    {
        public int ProductID { get; set; }
        public String ProductName { get; set; }
        public float UnitPrice { get; set; }

        public int UnitInstock { get; set; }

        public int CategoryId { get; set; }
    }
    class ProductBusiness
    {
        SqlConnection connection;
        // khai bao  doi tuong thuc thi cac truy van
        SqlCommand command;
        // doi tuong ket noi (lay doi tuong tu appsetting)
        string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config["ConnectionStrings:MyStoreDB"];
        }
        public  List<Product> GetProduct()
        {
            List<Product> product = new List<Product>();
            connection = new SqlConnection(GetConnectionString());
            // ket  Noi de lam gi
            command = new SqlCommand("Select * from Products", connection);
            // qua trinh co kha  nang loi ngoai le 
            try
            {
                connection.Open();
                // CommandBehivor loi close luon
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Product cur_product = new Product();
                        cur_product.ProductID = int.Parse(reader["ProductID"].ToString());
                        cur_product.ProductName = reader["ProductName"].ToString();
                        cur_product.UnitPrice = float.Parse(reader["UnitPrice"].ToString());
                        cur_product.UnitInstock = int.Parse(reader["UnitInStock"].ToString());
                        cur_product.CategoryId = int.Parse(reader["CategoryID"].ToString());
                        product.Add(cur_product);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return product;
        }
        public int InsertProudct(Product product)
        {
            int result = 0;
            connection = new SqlConnection(GetConnectionString());
            String sql = @"Insert into Products(ProductName,UnitPrice,UnitInStock,CategoryID)
                            Values(@ProductName,@UnitPrice,@unitInStock,@CateogryID)";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@unitInStock", product.UnitInstock);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@CateogryID", product.CategoryId);
            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public Product GetProductByID(int productID)
        {
            Product cur_product = new Product();
            connection = new SqlConnection(GetConnectionString());
            // ket  Noi de lam gi
            command = new SqlCommand("SELECT * FROM Products WHERE ProductID = @productID", connection);
            command.Parameters.AddWithValue("@productID", productID);
            // qua trinh co kha  nang loi ngoai le 
            try
            {
                connection.Open();
                // CommandBehivor loi close luon
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cur_product.ProductID = int.Parse(reader["ProductID"].ToString());
                        cur_product.ProductName = reader["ProductName"].ToString();
                        cur_product.UnitPrice = float.Parse(reader["UnitPrice"].ToString());
                        cur_product.UnitInstock = int.Parse(reader["UnitInStock"].ToString());
                        cur_product.CategoryId = int.Parse(reader["CategoryID"].ToString());
                        
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return cur_product;
        }

        public int UpdateProduct(Product product)
        {
            int result = 0;
            connection = new SqlConnection(GetConnectionString());
            String sql = @"UPDATE Products SET ProductName = @ProductName ,
                        UnitPrice = @UnitPrice , UnitInStock = @UnitInStock , 
                        CategoryID = @CategoryID  WHERE ProductID = @ProductID";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ProductID", product.ProductID);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@UnitInStock", product.UnitInstock);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@CategoryID", product.CategoryId);
            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public int DeleteProduct(int ID)
        {
            int result = 0;
            connection = new SqlConnection(GetConnectionString());
            String sql = " Delete from Products Where ProductID = @ID";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
