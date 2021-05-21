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

        void reload()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select Object_.ID as 'ID',Object_.DisplayName as N'Tên sản phẩm', ObjectType.DisplayName as N'Phân loại', Object_.Quantity as N'Số lượng tồn',Suplier.DisplayName,Object_.InputPrice,Available from dbo.Object_ join ObjectType on Object_.IDType=ObjectType.ID join Suplier on Object_.IDSuplier=Suplier.ID where Available='True'";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                sqlc.Close();
            }
            catch
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "") return;
            DateTime now = DateTime.Now;
            now.AddHours(24);
            string maDH = "DH" + now.Year + (now.Month < 10 ? "0" : "") + now.Month + (now.Day < 10 ? "0" : "") + now.Day + (now.Hour < 10 ? "0" : "") + now.Hour + (now.Minute < 10 ? "0" : "") + now.Minute + (now.Second < 10 ? "0" : "") + now.Second;
            textBox4.Text = maDH;
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("insert into Input(ID,InputDate) values('" + textBox4.Text + "','" + now.ToShortDateString() + "')", sqlc);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table temp( ID nvarchar(128)  primary key, DisplayName nvarchar(200), Count_ int, Price int )", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
                reloadHD();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            if (textBox2.Text == "") return;
            int x;
            if (!int.TryParse(textBox7.Text, out x)) return;
            if (x <= 0) return;
            bool update = false;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (Convert.ToString(dataGridView3.Rows[i].Cells[0].Value) == textBox2.Text) update = true;
            }
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("insert into temp(ID, DisplayName, Count_, Price) values('" + textBox2.Text + "', N'" + textBox1.Text + "', " + textBox7.Text + ", " + textBox6.Text + ")", sqlc);
                if (update) cmd = new SqlCommand("update temp set Count_=" + textBox7.Text + "where ID='" + textBox2.Text + "'", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
                reloadHD();
            }
            catch
            {

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox7.Text = "0";
            textBox1.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            textBox5.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
            textBox6.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
        }

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
