using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLKho_ttn
{
    public partial class UserControl_Xuat : UserControl
    {
        public UserControl_Xuat()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);

        private void button2_Click(object sender, EventArgs e)
        {
            using (Form_AddKHang addKH = new Form_AddKHang())
            {
                addKH.ShowDialog();
            }
            loadKH();
        }
        void loadKH()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select ID as 'ID', DisplayName as N'Họ tên', Phone as N'Số điện thoại', Mail as 'Email', Address_ as N'Địa chỉ' from Customer";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                sqlc.Close();
            }
            catch
            {

            }
        }
        void reload()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select Object_.ID as 'ID',Object_.DisplayName as N'Tên sản phẩm', ObjectType.DisplayName as N'Phân loại', Object_.Quantity as N'Số lượng tồn', Object_.OutputPrice,Object_.Available from dbo.Object_ join ObjectType on Object_.IDType=ObjectType.ID where Object_.Available='True'";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                sqlc.Close();
            }
            catch
            {

            }
        }
        private void UserControl_Xuat_Load(object sender, EventArgs e)
        {
            loadKH();
            reload();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox7.Text = "0";
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox5.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
        }
        void reloadHD()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select ID as 'ID', DisplayName as N'Tên sản phẩm', IDCustomer as N'Mã KH',Count_ as N'Số lượng',Price as N'Đơn giá' from temp";
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "") return;
            DateTime now = DateTime.Now;
            now.AddHours(24);
            string maDH = "DH" + now.Year + (now.Month < 10 ? "0" : "") + now.Month + (now.Day < 10 ? "0" : "") + now.Day + (now.Hour < 10 ? "0" : "") + now.Hour + (now.Minute < 10 ? "0" : "") + now.Minute + (now.Second < 10 ? "0" : "") + now.Second;
            textBox4.Text = maDH;
            //sqlc.Close();
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("insert into Output_(ID,OutputDate) values('" + textBox4.Text + "','" + now.ToShortDateString() + "')", sqlc);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table temp( ID nvarchar(128), DisplayName nvarchar(200),IDCustomer int, Count_ int, Price int,primary key(ID,IDCustomer))", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
            }
            catch
            {

            }
            reloadHD();

        }

            private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            if (textBox2.Text == "") return;
            if (textBox1.Text == "") return;
            int x;
            if (!int.TryParse(textBox7.Text, out x)) return;
            if (x <= 0) return;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox2.Text)
                {
                    if (x > Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString())) return;
                    break;
                }
            }
            bool update = false;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if ((Convert.ToString(dataGridView3.Rows[i].Cells[0].Value) == textBox2.Text) && (Convert.ToString(dataGridView3.Rows[i].Cells[2].Value) == textBox1.Text)) update = true;
            }
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("insert into temp(ID, DisplayName, IDCustomer, Count_, Price) values('" + textBox2.Text + "', N'" + textBox3.Text + "', " + textBox1.Text + ", " + textBox7.Text + ", " + textBox6.Text + ")", sqlc);
                if (update) cmd = new SqlCommand("update temp set Count_=" + textBox7.Text + "where ID='" + textBox2.Text + "' and IDCustomer='" + textBox1.Text + "'", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
            }
            catch
            {

            }
            reloadHD();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("truncate table temp", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
            }
            catch
            {

            }
            reloadHD();
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
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            try
            {
                sqlc.Open();
                SqlCommand cmd = new SqlCommand("delete Output_ where ID='" + textBox4.Text + "'", sqlc);
                cmd.ExecuteNonQuery();
                textBox4.Text = "";
                cmd = new SqlCommand("drop table temp", sqlc);
                cmd.ExecuteNonQuery();
                sqlc.Close();
            }
            catch
            {

            }
            unloadHD();
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
                    cmd = new SqlCommand("insert into OutputInfo(IDOutput,IDObject,IDCustomer,Count_) values('" + textBox4.Text + "','" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'," + dataGridView3.Rows[i].Cells[2].Value.ToString() + "," + dataGridView3.Rows[i].Cells[3].Value.ToString() + ")", sqlc);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("update Object_ set Quantity -= " + dataGridView3.Rows[i].Cells[3].Value.ToString() + " where id='" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'", sqlc);
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("drop table temp", sqlc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm đơn hàng " + textBox4.Text);
                textBox4.Text = "";
                sqlc.Close();
            }
            catch
            {

            }
            reload();
            unloadHD();
        }

    }
}
