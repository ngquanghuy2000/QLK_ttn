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
    public partial class Form_AddKHang : Form
    {
        public Form_AddKHang()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);
        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin khách hàng.");
            }
            else
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("insert into Customer(DisplayName,Phone,Mail,Address_) values (N'" + textBox2.Text + "', '" + textBox1.Text + "', '" + textBox3.Text + "', N'" + textBox4.Text + "')", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
                MessageBox.Show("Đã thêm khác hàng.");
                this.Dispose();
            }
        }
    }
}
