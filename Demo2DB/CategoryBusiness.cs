using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
namespace Demo2DB
{
    class  Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    class CategoryBusiness
    {
        //khai bao doi tuong ket not
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
        public List<Category> GetCategory()
        {
            List<Category> categories = new List<Category>();
            connection = new SqlConnection(GetConnectionString());
            // ket  Noi de lam gi
            command = new SqlCommand("Select Category,CategoryName from Categories", connection);
            // qua trinh co kha  nang loi ngoai le 
            try
            {
                connection.Open();
                // CommandBehivor loi close luon
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if(reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category {CategoryID = reader.GetInt32("Category"),
                            CategoryName = reader.GetString("CategoryName") });
                        
                    }
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
                
            return categories;
        } 
        public int InsertCategory(Category category)
        {
            int result = 0;
            connection = new SqlConnection(GetConnectionString());
            string sql = "insert into Categories(CategoryName) values(@cateName)";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@cateName", category.CategoryName);
            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public int UpdateCategory(int ID, String CatName) {
            int result = 0;
            connection = new SqlConnection(GetConnectionString());
            string sql = " update Categories SET CategoryName = @CatName Where Category = @ID";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CatName", CatName);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;


        }
        public int DeleteCategory(int ID)
        {
            int result = 0;
            connection = new SqlConnection(GetConnectionString());
            String sql = " Delete from Categories Where Category = @ID";
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
