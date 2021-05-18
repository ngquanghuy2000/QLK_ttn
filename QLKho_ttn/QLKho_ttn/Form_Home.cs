using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKho_ttn
{
    public partial class Form_Home : Form
    {
        public Form_Home()
        {
            InitializeComponent();
        }
        
        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            panelControls.Controls.Clear();
            panelControls.Controls.Add(c);
        }

        private void Form_Home_Load(object sender, EventArgs e)
        {
            moveSidePanel(button7);
            UserControl_SLton slt = new UserControl_SLton();
            AddControlsToPanel(slt);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnHome);
            UserControl_Nhap nhap = new UserControl_Nhap();
            AddControlsToPanel(nhap);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            moveSidePanel(button1);
            UserControl_Xuat xuat = new UserControl_Xuat();
            AddControlsToPanel(xuat);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!SQL_Connect.check_admin) return;
            moveSidePanel(button3);
            UserControl_LichSu lsu = new UserControl_LichSu();
            AddControlsToPanel(lsu);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!SQL_Connect.check_admin) return;
            moveSidePanel(button4);
            UserControl_NCC ncc = new UserControl_NCC();
            AddControlsToPanel(ncc);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!SQL_Connect.check_admin) return;
            moveSidePanel(button5);
            UserControl_QlyHang qlh = new UserControl_QlyHang();
            AddControlsToPanel(qlh);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!SQL_Connect.check_admin) return;
            moveSidePanel(button6);
            UserControl_QLyTaiKhoan qltk = new UserControl_QLyTaiKhoan();
            AddControlsToPanel(qltk);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            moveSidePanel(button7);
            UserControl_SLton slt = new UserControl_SLton();
            AddControlsToPanel(slt);
        }

    }
}
