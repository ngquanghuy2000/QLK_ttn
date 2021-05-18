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
    public partial class Form_AddType : Form
    {
        public Form_AddType()
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
                MessageBox.Show("Vui lòng nhập tên danh mục.");
            }
            else
            {
                try
                {
                    sqlc.Open();
                    SqlCommand cmd = new SqlCommand("insert into ObjectType(DisplayName) values(N'" + textBox2.Text + "')", sqlc);
                    cmd.ExecuteNonQuery();
                    sqlc.Close();
                    MessageBox.Show("Đã thêm danh mục.");
                    this.Dispose();
                }
                catch
                {

                }
            }
        }
    }
}
