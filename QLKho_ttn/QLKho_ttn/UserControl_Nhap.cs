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
    public partial class UserControl_Nhap : UserControl
    {
        public UserControl_Nhap()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);


        private void UserControl_Nhap_Load(object sender, EventArgs e)
        {
            reload();
        }
        void reloadHD()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select ID as 'ID', DisplayName as N'Tên sản phẩm', Count_ as N'Số lượng', Price as N'Đơn giá' from temp";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0];
                dataGridView3.Columns[0].Visible = false;
                sqlc.Close();
            }
            catch
            {

            }
        }
        void unloadHD()
        {
            try
            {
                dataGridView3.DataSource = null;
            }
            catch
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("truncate table temp", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
                reloadHD();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("delete Input where ID='" + textBox4.Text + "'", sqlc);
                cmd.ExecuteNonQuery();
                textBox4.Text = "";
                cmd = new SqlCommand("drop table temp", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
                unloadHD();
            }
            catch
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            try
            {
                sqlc.Open();
                SqlCommand cmd;
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    cmd = new SqlCommand("insert into InputInfo(IDInput,IDObject,Count_) values('" + textBox4.Text + "','" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'," + dataGridView3.Rows[i].Cells[2].Value.ToString() + ")", sqlc);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("update Object_ set Quantity += " + dataGridView3.Rows[i].Cells[2].Value.ToString() + " where id='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'", sqlc);
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("drop table temp", sqlc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm đơn hàng " + textBox4.Text);
                textBox4.Text = "";
                sqlc.Close();
                reload();
                unloadHD();
            }
            catch
            {

            }
        }
    }
}
