using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ModelClass; // Add this using directive
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DALMessage
    {
        public static bool InsertMessage(int senderId, int receiverId, string content)
        {
            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO Messages (SenderId, ReceiverId, Content, Timestamp) VALUES (@SenderId, @ReceiverId, @Content, @Timestamp)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderId", senderId);
                    command.Parameters.AddWithValue("@ReceiverId", receiverId);
                    command.Parameters.AddWithValue("@Content", content);
                    command.Parameters.AddWithValue("@Timestamp", DateTime.UtcNow);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static List<Message> GetIncomingMessages(int recipientId)
        {
            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Messages WHERE ReceiverId = @RecipientId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecipientId", recipientId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Message> incomingMessages = new List<Message>();

                        while (reader.Read())
                        {
                            var message = new Message
                            {
                                MessageId = Convert.ToInt32(reader["MessageId"]),
                                SenderId = Convert.ToInt32(reader["SenderId"]),
                                ReceiverId = Convert.ToInt32(reader["ReceiverId"]),
                                Content = reader["Content"].ToString(),
                                Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                            };

                            incomingMessages.Add(message);
                        }

                        return incomingMessages;
                    }
                }
            }
        }

        public static Message GetMessageDetails(int messageId)
        {
            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Messages WHERE MessageId = @MessageId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MessageId", messageId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Message
                            {
                                MessageId = Convert.ToInt32(reader["MessageId"]),
                                SenderId = Convert.ToInt32(reader["SenderId"]),
                                ReceiverId = Convert.ToInt32(reader["ReceiverId"]),
                                Content = reader["Content"].ToString(),
                                Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                                // Add other properties as needed
                            };
                        }

                        // Handle the case where the message with the given MessageId is not found
                        // You might want to throw an exception or return a default value
                        return null;
                    }
                }
            }
        }
    }

}
