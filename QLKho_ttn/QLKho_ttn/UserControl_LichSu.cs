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
    public partial class UserControl_LichSu : UserControl
    {
        public UserControl_LichSu()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);
        void loadInput()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select i.ID as'ID', i.InputDate as N'Ngày nhập', Sum(o.InputPrice*ii.Count_) as N'Tổng tiền' from Input i join InputInfo ii on i.ID=ii.IDInput join Object_ o on ii.IDObject=o.ID group by i.ID,i.InputDate";
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
        void loadOutput()
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select o.ID as'ID', o.OutputDate as N'Ngày xuất', Sum(obj.OutputPrice*oi.Count_) as N'Tổng tiền' from Output_ o join OutputInfo oi on o.ID=oi.IDOutput join Object_ obj on oi.IDObject=obj.ID group by o.ID,o.OutputDate";
                SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
                SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
                var ds = new DataSet();
                sqla.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0];
                sqlc.Close();
            }
            catch
            {

            }
        }
        private void UserControl_LichSu_Load(object sender, EventArgs e)
        {
            loadInput();
            loadOutput();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            sqlc.Open();
            string Myquery = "select o.DisplayName as N'Tên sản phẩm',i.Count_ as N'Số lượng',o.InputPrice as N'Đơn giá' from InputInfo i join Object_ o on i.IDObject=o.ID where i.IDInput='"+ dataGridView1.SelectedRows[0].Cells[0].Value.ToString() +"'";
            SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
            SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
            var ds = new DataSet();
            sqla.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            sqlc.Close();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            sqlc.Open();
            string Myquery = "select obj.DisplayName as N'Tên sản phẩm',o.Count_ as N'Số lượng', c.DisplayName as N'Khách hàng', obj.OutputPrice as N'Đơn giá' from OutputInfo o join Object_ obj on o.IDObject=obj.ID join Customer c on c.ID=o.IDCustomer where o.IDOutput='" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'";
            SqlDataAdapter sqla = new SqlDataAdapter(Myquery, sqlc);
            SqlCommandBuilder builder = new SqlCommandBuilder(sqla);
            var ds = new DataSet();
            sqla.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
            sqlc.Close();
        }
    }
}
