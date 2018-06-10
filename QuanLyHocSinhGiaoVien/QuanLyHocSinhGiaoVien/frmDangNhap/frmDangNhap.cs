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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            chkDangNhap DangNhap = new chkDangNhap();
            if (DangNhap.LoginHeThong(txtTenDN.Text, txtMK.Text) == true)
            {
                MessageBox.Show("Đăng Nhập Thành Công", "Thông Báo");
                //frmMain frm = new frmMain();
                this.Hide();
                //frm.Show();
            }
            else
            {
                MessageBox.Show("Thông Tin Đăng Nhập Không Đúng.Vui Lòng Kiểm Tra Lại", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
