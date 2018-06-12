using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataLayer
{
     public class Globle_Connection
    {
        public static string connection {
            get  { return ConfigurationManager.ConnectionStrings["eCommerceEntities"].ConnectionString; }
        }
        //SqlConnection con = new SqlConnection(Globle_Con.conn);
    }
    public enum TrueFalse
    {
        Yes,
        No
    }

    public enum FashionFor
    {
        Men,
        Women,
        Kids
    }
}
