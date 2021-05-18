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
    public partial class UserControl_NCC : UserControl
    {
        public UserControl_NCC()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);
        void reload()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select ID as 'ID', DisplayName as N'Họ tên', Phone as N'Số điện thoại', Mail as 'Email', Address_ as N'Địa chỉ' from Suplier";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                sqlc.Close();
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            if (textBox3.Text == "") return;
            if (textBox2.Text == "") return;
            if (textBox1.Text == "") return;
            if (textBox5.Text != "") return;
            sqlc.Open();
            SqlCommand cmd = new SqlCommand("insert into Suplier(DisplayName,Phone,Mail,Address_) values (N'" + textBox2.Text + "', '" + textBox1.Text + "', '" + textBox3.Text + "', N'" + textBox4.Text + "')", sqlc);
            cmd.ExecuteNonQuery();
            sqlc.Close();
            MessageBox.Show("Đã thêm nhà cung cấp.");
            reload();
        }
        
        private void UserControl_NCC_Load(object sender, EventArgs e)
        {
            reload();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "") return;
            if (textBox4.Text == "") return;
            if (textBox3.Text == "") return;
            if (textBox2.Text == "") return;
            if (textBox1.Text == "") return;
            sqlc.Open();
            SqlCommand cmd = new SqlCommand("update Suplier set DisplayName = N'" + textBox2.Text + "', Phone = '" + textBox1.Text + "', Mail = '" + textBox3.Text + "', Address_ = N'" + textBox4.Text + "' where ID= " + textBox5.Text, sqlc);
            cmd.ExecuteNonQuery();
            sqlc.Close();
            MessageBox.Show("Sửa thông tin thành công");
            reload();
        }
    }
}
