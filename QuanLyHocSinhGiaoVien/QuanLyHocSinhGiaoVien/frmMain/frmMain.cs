using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHocSinhGiaoVien
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void hỌCSINHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChonLop frm = new frmChonLop();
            frm.Show();
        }

        private void gIÁOVIÊNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGiaoVien frm = new frmGiaoVien();
            frm.Show();
        }

        private void lỚPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLop frm = new frmLop();
            frm.Show();
        }

        private void mÔNHỌCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonHoc frm = new frmMonHoc();
            frm.Show();
        }

        private void đĂNGXUẤTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                MessageBox.Show("Đăng xuất thành công");
                this.Close();
                frmDangNhap frm = new frmDangNhap();
                frm.Show();
            }
        }
    }
}
