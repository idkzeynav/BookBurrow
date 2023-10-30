using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Data;
using ModelClass;
using DataAccessLayer;

namespace DataAccessLayer
{
    public class DALUserAuth
    {

        public static ModelUserProfile Authenticate(string username, string password)
        {
            try
            {
                SqlConnection con = DBhelper.GetConnection();
                SqlCommand cmd = new SqlCommand("LoginKaro", con);
                cmd.Parameters.AddWithValue("@email", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                ModelUserProfile modelUserProfile = new ModelUserProfile();
                while (sdr.Read())
                {
                   // modelUserProfile.id = sdr["id"].ToString();
                    modelUserProfile.name = sdr["name"].ToString();
                    modelUserProfile.roles = sdr["roles"].ToString();

                }
                sdr.Close();
                con.Close();
                return modelUserProfile;
            }
            catch
            {
                return new ModelUserProfile();
            }
           
        }
    }
}
