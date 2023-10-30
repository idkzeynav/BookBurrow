using System.Data.SqlClient;


namespace DataAccessLayer
{
    public class DBhelper
    {
        public static SqlConnection GetConnection() { 
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-5R2C9PR6\\SQLEXPRESS;Initial Catalog=zainab;Integrated Security=True");
            return con;
        }
    }
}