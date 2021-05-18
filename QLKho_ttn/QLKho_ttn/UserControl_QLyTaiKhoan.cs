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
    public partial class UserControl_QLyTaiKhoan : UserControl
    {
        public UserControl_QLyTaiKhoan()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);
        void reload()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select u.ID as 'ID', u.DisplayName as N'Họ tên',u.UserName as N'Tên người dùng', u.Password_ as N'Mật khẩu', u.AdminRole as N'Quản trị viên' from User_ u";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[3].Visible = false;
                sqlc.Close();
            }
            catch
            {

            }
        }
        void reload1()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select u.ID as 'ID', u.DisplayName as N'Họ tên',u.UserName as N'Tên người dùng', u.Password_ as N'Mật khẩu', u.AdminRole as N'Quản trị viên' from User_ u where UserName ='" + textBox2.Text + "' and Password_= '" + textBox6.Text + "'";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[3].Visible = false;
                sqlc.Close();
            }
            catch
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != textBox6.Text) return;
            if (textBox2.Text == "") return;
            if (textBox3.Text == "") return;
            if (textBox5.Text == "") return;
            if (textBox6.Text == "") return;
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("update User_ set DisplayName = N'" + textBox3.Text + "',Password_ = '" + textBox6.Text + "' where UserName ='" + textBox2.Text + "' and Password_= '" + textBox5.Text + "'", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
                reload1();
                if (dataGridView1.Rows.Count == 1)
                {
                    MessageBox.Show("Đã sửa tài khoản "+ dataGridView1.Rows[0].Cells[2].Value.ToString());
                }
                else
                {
                    MessageBox.Show("Nhập Sai!");
                }
            }
            catch
            {

            }
            reload();
            
        }

        private void UserControl_QLyTaiKhoan_Load(object sender, EventArgs e)
        {
            reload();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != textBox6.Text) return;
            if (textBox2.Text == "") return;
            if (textBox3.Text == "") return;
            if (textBox6.Text == "") return;
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("insert into User_(DisplayName, UserName, Password_, AdminRole) values(N'" + textBox3.Text + "', '" + textBox2.Text + "', '" + textBox6.Text + "', " + Convert.ToInt32(checkBox1.Checked) + ")", sqlc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm tài khoản");
                sqlc.Close();
            }
            catch
            {

            }
            reload();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            checkBox1.Checked = dataGridView1.SelectedRows[0].Cells[4].Value.ToString() == "True" ? true : false;
        }
    }
}
