using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho_ttn
{
    static class SQL_Connect
    {
        public static string ConnectionString = @"Data Source=.\sqlserverr;Initial Catalog=ttn_huy;Integrated Security=True";
        public static bool check_admin;
    }
    public class cbboxItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
