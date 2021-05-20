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
    public partial class UserControl_SLton : UserControl
    {
        public UserControl_SLton()
        {
            InitializeComponent();
        }
        SqlConnection sqlc = new SqlConnection(SQL_Connect.ConnectionString);


        // bind data to gridview  Duy
        private void UserControl_SLton_Load(object sender, EventArgs e)
        {
            try
            {
                sqlc.Open();
                string Myquery = @"select dbo.Object_.ID as ID,dbo.Object_.DisplayName as N'Tên sản phẩm',dbo.ObjectType.DisplayName as N'Phân loại',dbo.Suplier.DisplayName as N'Nhà cung cấp',dbo.Object_.Quantity as N'Số lượng tồn',dbo.Object_.InputPrice as N'Giá nhập',dbo.Object_.OutputPrice as N'Giá xuất' from dbo.Suplier join dbo.Object_ on Suplier.ID = Object_.IDSuplier join dbo.ObjectType on Object_.IDType = ObjectType.ID";
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


    }
}
