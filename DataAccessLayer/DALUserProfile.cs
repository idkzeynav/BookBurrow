using ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DataAccessLayer
{
    public class DALUserProfile
    {
        public static List<ModelUserProfile> GetUserProfile()
        {
            SqlConnection con = DBhelper.GetConnection();
            SqlCommand cmd = new SqlCommand("select * from UserProfile ", con);
            con.Open();
            SqlDataReader sqlDataReaderreader = cmd.ExecuteReader();
            List<ModelUserProfile> ListItems = new List<ModelUserProfile>();
            while (sqlDataReaderreader.Read())
            {
                ModelUserProfile modelUserProfile = new ModelUserProfile();
                modelUserProfile.id = Convert.ToInt32(sqlDataReaderreader["id"]);
                modelUserProfile.name = sqlDataReaderreader["name"].ToString();
                modelUserProfile.roles = sqlDataReaderreader["roles"].ToString();
                modelUserProfile.email = sqlDataReaderreader["email"].ToString();
                ListItems.Add(modelUserProfile);
            }
            con.Close();
            return ListItems;
        }
    }
}
