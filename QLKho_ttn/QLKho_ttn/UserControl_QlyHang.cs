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
    public partial class UserControl_QlyHang : UserControl
    {
        public UserControl_QlyHang()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);
        void reload()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select dbo.Object_.ID as ID,dbo.Object_.DisplayName as N'Tên sản phẩm',dbo.ObjectType.DisplayName as N'Phân loại',dbo.Suplier.DisplayName as N'Nhà cung cấp',dbo.Object_.InputPrice as N'Giá nhập',dbo.Object_.OutputPrice as N'Giá xuất',dbo.Object_.Available as N'Nhập/xuất' from dbo.Suplier join dbo.Object_ on Suplier.ID = Object_.IDSuplier join dbo.ObjectType on Object_.IDType = ObjectType.ID";
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
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") return;
            int x;
            if (!int.TryParse(textBox4.Text, out x)) return;
            if (x <= 0) return;
            if (!int.TryParse(textBox1.Text, out x)) return;
            if (x <= 0) return;
            DateTime now = DateTime.Now;
            now.AddHours(24);
            string ID = "OBJ" + now.Year + (now.Month < 10 ? "0" : "") + now.Month + (now.Day < 10 ? "0" : "") + now.Day + (now.Hour < 10 ? "0" : "") + now.Hour + (now.Minute < 10 ? "0" : "") + now.Minute + (now.Second < 10 ? "0" : "") + now.Second;
            sqlc.Open();
            SqlCommand cmd = new SqlCommand("insert into Object_(ID,DisplayName,IDType,IDSuplier,InputPrice,OutputPrice,Available) values('" + ID + "',N'" + textBox2.Text + "'," + comboBox3.SelectedValue.ToString() + "," + comboBox1.SelectedValue.ToString() + "," + textBox4.Text + "," + textBox1.Text + ",'True')", sqlc);
            cmd.ExecuteNonQuery();
            sqlc.Close();
            MessageBox.Show("Thêm sản phẩm thành công");
            reload();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "") return;
            int x;
            if (!int.TryParse(textBox8.Text, out x)) return;
            if (x <= 0) return;
            if (!int.TryParse(textBox5.Text, out x)) return;
            if (x <= 0) return;
            sqlc.Open();
            SqlCommand cmd = new SqlCommand("update Object_ set InputPrice=" + textBox8.Text + ", OutputPrice=" + textBox5.Text + ", Available='" + Convert.ToString(checkBox1.Checked) + "' where ID='" + textBox7.Text + "'", sqlc);
            cmd.ExecuteNonQuery();
            sqlc.Close();
            MessageBox.Show("Sửa thông tin thành công");
            reload();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Form_AddType addT = new Form_AddType())
            {
                addT.ShowDialog();
            }
            comboboxType();
        }
        void comboboxType()
        {
            string query = "select ID,DisplayName from ObjectType";
            SqlCommand cmd = new SqlCommand(query, sqlc);
            SqlDataReader reader;
            try
            {
                sqlc.Open();
                reader = cmd.ExecuteReader();
                List<cbboxItem> items=new List<cbboxItem>();
                while (reader.Read())
                {
                    cbboxItem item = new cbboxItem();
                    item.ID = Convert.ToInt32(reader[0].ToString());
                    item.Text = reader[1].ToString();
                    items.Add(item);
                }
                sqlc.Close();
                if (items.Count() == 0) return;
                comboBox3.DataSource = items;
                comboBox3.SelectedIndex=0;
                comboBox3.DisplayMember = "Text";
                comboBox3.ValueMember = "ID";
            }
            catch
            {

            }
        }
        void comboboxSuplier()
        {
            string query = "select ID,DisplayName from Suplier";
            SqlCommand cmd = new SqlCommand(query, sqlc);
            SqlDataReader reader;
            try
            {
                sqlc.Open();
                reader = cmd.ExecuteReader();
                List<cbboxItem> items = new List<cbboxItem>();
                while (reader.Read())
                {
                    cbboxItem item = new cbboxItem();
                    item.ID = Convert.ToInt32(reader[0].ToString());
                    item.Text = reader[1].ToString();
                    items.Add(item);
                }
                sqlc.Close();
                if (items.Count() == 0) return;
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "ID";
            }
            catch
            {

            }
        }

        private void UserControl_QlyHang_Load(object sender, EventArgs e)
        {
            reload();
            comboboxType();
            comboboxSuplier();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            checkBox1.Checked = dataGridView1.SelectedRows[0].Cells[6].Value.ToString() == "True" ? true : false;
        }
    }
}
