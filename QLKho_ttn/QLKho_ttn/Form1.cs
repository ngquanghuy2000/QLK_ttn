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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select AdminRole from User_ where UserName='" + textBox1.Text + "' and Password_='" + textBox2.Text + "'";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                sqlc.Close();

                if (ds.Tables[0].Rows.Count >= 1)
                {
                    SQL_Connect.check_admin = ds.Tables[0].Rows[0].ItemArray[0].ToString() == "True" ? true : false;
                    using (Form_Home fd = new Form_Home())
                    {
                        fd.ShowDialog();
                        //Application.Exit();
                    }
                }
            }
            catch
            {

            }
            
        }
    }
}
