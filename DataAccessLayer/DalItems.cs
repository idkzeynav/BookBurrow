using ModelClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DalItems
    {
        public static List<ModelItems> GetModelItems()
        {
            SqlConnection con = DBhelper.GetConnection();
            SqlCommand cmd = new SqlCommand("select * from items ", con);
            con.Open();
            SqlDataReader sqlDataReaderreader = cmd.ExecuteReader(); 
            List<ModelItems> ListItems = new List<ModelItems>();
            while (sqlDataReaderreader.Read()) 
            {
                ModelItems modelItem = new ModelItems();
                modelItem.ItemId = Convert.ToInt32(sqlDataReaderreader["ItemId"]);
                modelItem.ItemName= sqlDataReaderreader["ItemName"].ToString();
                ListItems.Add(modelItem);
            }
            con.Close();
            return ListItems;
        }
    }
}
