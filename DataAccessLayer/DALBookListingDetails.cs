using ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;


namespace DataAccessLayer
{
    public class DALBookListingDetails
    {
        public static List<ModelBookListingDetails> GetBookListingDetails()
        {
            SqlConnection con = DBhelper.GetConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM BookListingDetails", con);
            con.Open();
            SqlDataReader sqlDataReaderreader = cmd.ExecuteReader();
            List<ModelBookListingDetails> ListItems = new List<ModelBookListingDetails>();
            while (sqlDataReaderreader.Read())
            {
                ModelBookListingDetails modelBookListingDetails = new ModelBookListingDetails();
                modelBookListingDetails.BookId = Convert.ToInt32(sqlDataReaderreader["BookId"]);
                modelBookListingDetails.title = sqlDataReaderreader["title"].ToString();
                modelBookListingDetails.author = sqlDataReaderreader["author"].ToString();
                modelBookListingDetails.categories = sqlDataReaderreader["categories"].ToString();
                modelBookListingDetails.price = Convert.ToInt32(sqlDataReaderreader["price"]);
                modelBookListingDetails.availability = sqlDataReaderreader["availability"].ToString();
                modelBookListingDetails.SellersNo = sqlDataReaderreader["SellersNo"].ToString();

                // Check if "Location" field exists before accessing it
                if (sqlDataReaderreader.GetSchemaTable().Columns.Contains("Location"))
                {
                    modelBookListingDetails.Location = sqlDataReaderreader["Location"].ToString();
                }
                else
                {
                    // Handle the case where "Location" field doesn't exist in the result set
                    modelBookListingDetails.Location = string.Empty; // or null, depending on your needs
                }

                ListItems.Add(modelBookListingDetails);
            }


            con.Close();
            return ListItems;
        }


        public void DeleteBookListing(string title)
        {
            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM BookListingDetails WHERE title = @title";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static bool UpdateBook(ModelBookListingDetails updatedBook)
        {
            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "UPDATE BookListingDetails SET title = @title, author = @author, categories = @categories, " +
                "price = @price, availability = @availability, SellersNo = @SellersNo, Location = @Location " +
                "WHERE BookId = @BookId";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", updatedBook.title);
                    command.Parameters.AddWithValue("@author", updatedBook.author);
                    command.Parameters.AddWithValue("@categories", updatedBook.categories);
                    command.Parameters.AddWithValue("@price", updatedBook.price);
                    command.Parameters.AddWithValue("@availability", updatedBook.availability);
                    command.Parameters.AddWithValue("@SellersNo", updatedBook.SellersNo);
                    command.Parameters.AddWithValue("@BookId", updatedBook.BookId);
                    command.Parameters.AddWithValue("@Location", updatedBook.Location);

                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the update was successful
                    return rowsAffected > 0;
                }
            }
        }

        public static bool InsertBookListing(string title, string author, string categories, int price, string availability, string SellersNo, int SellerId, string SellerUsername, string Location)
        {
            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO BookListingDetails (title, author, categories, price, availability, SellersNo, SellerId, SellerUsername, Location) " +
                               "VALUES (@title, @author, @categories, @price, @availability, @SellersNo, @SellerId, @SellerUsername, @Location)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@categories", categories);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@availability", availability);
                    command.Parameters.AddWithValue("@SellersNo", SellersNo);
                    command.Parameters.AddWithValue("@SellerId", SellerId);
                    command.Parameters.AddWithValue("@SellerUsername", SellerUsername);
                    command.Parameters.AddWithValue("@Location", Location); 

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        public static List<ModelBookListingDetails> GetBooksByCategory(string category)
        {
            List<ModelBookListingDetails> books = new List<ModelBookListingDetails>();

            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM BookListingDetails WHERE categories = @category";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@category", category);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelBookListingDetails foundBook = new ModelBookListingDetails
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                title = reader["title"].ToString(),
                                author = reader["author"].ToString(),
                                categories = reader["categories"].ToString(),
                                price = Convert.ToInt32(reader["price"]),
                                availability = reader["availability"].ToString(),
                                SellersNo = reader["SellersNo"].ToString()
                            };

                            books.Add(foundBook);
                        }
                    }
                }
            }

            return books;
        }

        /**/public static List<ModelBookListingDetails> GetBooksByLocation(string Location)
        {
            List<ModelBookListingDetails> books = new List<ModelBookListingDetails>();

            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM BookListingDetails WHERE Location = @Location";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Location", Location);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelBookListingDetails foundBook = new ModelBookListingDetails
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                title = reader["title"].ToString(),
                                author = reader["author"].ToString(),
                                categories = reader["categories"].ToString(),
                                price = Convert.ToInt32(reader["price"]),
                                availability = reader["availability"].ToString(),
                                SellersNo = reader["SellersNo"].ToString(),
                                Location = reader["Location"].ToString()
                            };

                            books.Add(foundBook);
                        }
                    }
                }
            }

            return books;
        }

        public static List<string> GetDistinctCategories()
        {
            SqlConnection con = DBhelper.GetConnection();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT categories FROM BookListingDetails", con);
            con.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            List<string> distinctCategories = new List<string>();

            while (sqlDataReader.Read())
            {
                string category = sqlDataReader["categories"].ToString();
                if (!string.IsNullOrEmpty(category) && !distinctCategories.Contains(category))
                {
                    distinctCategories.Add(category);
                }
            }

            con.Close();
            return distinctCategories;
        }

        /*public static List<ModelBookListingDetails> SearchBooks(string searchQuery)
        {
            List<ModelBookListingDetails> books = new List<ModelBookListingDetails>();

            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM BookListingDetails " +
                               "WHERE title LIKE @searchQuery OR author LIKE @searchQuery";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelBookListingDetails foundBook = new ModelBookListingDetails
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                title = reader["title"].ToString(),
                                author = reader["author"].ToString(),
                                categories = reader["categories"].ToString(),
                                price = Convert.ToInt32(reader["price"]),
                                availability = reader["availability"].ToString(),
                                SellersNo = reader["SellersNo"].ToString()
                            };

                            books.Add(foundBook);
                        }
                    }
                }
            }

            return books;
        }*/
        public static List<ModelBookListingDetails> SearchBooks(string location)
        {
            List<ModelBookListingDetails> books = new List<ModelBookListingDetails>();

            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM BookListingDetails " +
                               "WHERE (title LIKE @searchQuery OR author LIKE @searchQuery) " +
                               "AND (Location = @location OR @location = '')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                    command.Parameters.AddWithValue("@location", location);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModelBookListingDetails foundBook = new ModelBookListingDetails
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                title = reader["title"].ToString(),
                                author = reader["author"].ToString(),
                                categories = reader["categories"].ToString(),
                                price = Convert.ToInt32(reader["price"]),
                                availability = reader["availability"].ToString(),
                                SellersNo = reader["SellersNo"].ToString(),
                                Location = reader["Location"].ToString(),
                                BImage = reader["BImage"].ToString(), // Assuming BImage is a property in your ModelBookListingDetails class
                              
                            };

                            books.Add(foundBook);
                        }
                    }
                }
            }

            return books;
        }

    }


}
    


