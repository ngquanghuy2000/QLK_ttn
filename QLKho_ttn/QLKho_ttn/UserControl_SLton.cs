using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLKho_ttn
{
    public partial class UserControl_SLton : UserControl
    {
        public UserControl_SLton()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);

    }
}
