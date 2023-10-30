using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserSession
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
        public string? roles { get; set; }
    }
}
