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
