using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class ModelBookListingDetails
    {
        public int BookId { get; set; }
   
        public string? title { get; set; }
 
        public string? categories { get; set; }

        public string? author { get; set; }
 
        public int price { get; set; }
  
        public string? availability { get; set; }
 
        public string? SellersNo { get; set; }

        public string? Location { get; set; }

        public string? BImage { get; set; }

        //seller information for chatting add it later also add it back in dbms
        public int SellerId { get; set; }  
        public string SellerUsername { get; set; }

    }
}
