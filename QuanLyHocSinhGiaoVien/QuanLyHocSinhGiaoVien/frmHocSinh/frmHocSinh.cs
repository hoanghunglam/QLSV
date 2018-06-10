using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHocSinhGiaoVien
{
    public partial class frmHocSinh : Form
    {
        string MaLop;
        string hinhanh;
        string str;
        public frmHocSinh(string ma)
        {
            MaLop = ma;
            InitializeComponent();
        }

        private void frmHocSinh_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
            this.txtTuKhoa.Text = "Ví Dụ: HS0001 / Nguyễn Văn A";
            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmd.CommandText = "SELECT MaHS,TenHS,GioiTinh,NgaySinh,DiaChi,SDT,TenLop FROM HOCSINH HS,LOP WHERE HS.MaLop = LOP.MaLop AND HS.MaLop = '" + MaLop + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            DateTime dt;
            for (int i = 0; i < td.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                item.SubItems.Add(td.Rows[i][1].ToString());
                if (td.Rows[i][2].ToString() == "True")
                    item.SubItems.Add("Nam");
                else
                    item.SubItems.Add("Nữ");
                dt = DateTime.Parse(td.Rows[i][3].ToString());
                item.SubItems.Add(dt.ToString("dd/MM/yyyy"));
                item.SubItems.Add(td.Rows[i][4].ToString());
                item.SubItems.Add(td.Rows[i][5].ToString());
                item.SubItems.Add(td.Rows[i][6].ToString());
                listView1.Items.Add(item);
            }
            con.Close();
        }
        protected void textBox1_Focus(Object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";
        }
        public int KiemTra()
        {
            if (radioButton1.Checked == true)
                return 1;
            else if (radioButton2.Checked == true)
                return 2;
            else
                return 0;
        }
        private void getData(AutoCompleteStringCollection dataCollection)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            connetionString = KetNoi.str;
            string sql = "Select TenHS from HOCSINH WHERE MaLop = '" + MaLop + "'";

            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void getData2(AutoCompleteStringCollection dataCollection)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            connetionString = KetNoi.str;
            string sql = "Select distinct MaHS from HOCSINH WHERE MaLop = '" + MaLop + "'";

            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int row = this.listView1.SelectedItems[0].Index;
                str = this.listView1.Items[row].SubItems[0].Text;
                int sex;
                if (rdNam.Checked == true)
                    sex = 1;
                else
                    sex = 0;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "select * from HOCSINH where MaHS='" + txtMaHS.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                if (txtMaHS.Text != "" && txtTenHS.Text != "" && dateTimePicker1.Text != "" && (rdNam.Checked == true || rdNu.Checked == true))
                {
                    if (td2.Rows.Count == 1)
                    {
                        if(txtMaHS.Text == str)
                        {
                            if (txtSDT.Text == "")
                            {
                                cmd.CommandText = "UPDATE HOCSINH SET TenHS=N'" + txtTenHS.Text + "',NgaySinh='" + DateTime.Parse(dateTimePicker1.Text) + "',GioiTinh='" + sex + "',SDT='" + txtSDT.Text + "',DiaChi=N'" + txtDiaChi.Text + "',MaLop='" + MaLop + "',HinhAnh='" + hinhanh + "' WHERE MaHS='" + str + "'";
                                DialogResult result;
                                result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                    frmHocSinh frm = new frmHocSinh(MaLop);
                                    this.Close();
                                    frm.Show();
                                }
                            }
                            else
                            {
                                if (IsNumber(txtSDT.Text))
                                {
                                    cmd.CommandText = "UPDATE HOCSINH SET TenHS=N'" + txtTenHS.Text + "',NgaySinh='" + DateTime.Parse(dateTimePicker1.Text) + "',GioiTinh='" + sex + "',SDT='" + txtSDT.Text + "',DiaChi=N'" + txtDiaChi.Text + "',MaLop='" + MaLop + "',HinhAnh='" + hinhanh + "' WHERE MaHS='" + str + "'";
                                    DialogResult result;
                                    result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                        frmHocSinh frm = new frmHocSinh(MaLop);
                                        this.Close();
                                        frm.Show();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("SĐT nhập không đúng!");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã Học Sinh Không Thể Sửa !", "Thông Báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã Học Sinh Không Tồn Tại !", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn học sinh muốn sửa", "THÔNG BÁO");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sex;
            if (rdNam.Checked == true)
                sex = 1;
            else
                sex = 0;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "select * from HOCSINH where MaHS='" + txtMaHS.Text + "'";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            if (txtMaHS.Text != "" && txtTenHS.Text != "" && dateTimePicker1.Text != "" && (rdNam.Checked == true || rdNu.Checked == true))
            {
                if (td2.Rows.Count == 0)
                {
                    if (txtMaHS.Text.Length == 6)
                    {
                        if (txtSDT.Text == "")
                        {
                            cmd.CommandText = "insert into HOCSINH values ('" + txtMaHS.Text + "',N'" + txtTenHS.Text + "','" + DateTime.Parse(dateTimePicker1.Text) + "','" + sex + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "','" + MaLop + "','" + hinhanh + "')";
                            DialogResult result;
                            result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI HOC SINH NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                frmHocSinh frm = new frmHocSinh(MaLop);
                                this.Close();
                                frm.Show();
                            }
                        }
                        else
                        {
                            if (IsNumber(txtSDT.Text))
                            {
                                cmd.CommandText = "insert into HOCSINH values ('" + txtMaHS.Text + "',N'" + txtTenHS.Text + "','" + DateTime.Parse(dateTimePicker1.ToString()) + "','" + sex + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "','" + MaLop + "','" + hinhanh + "')";
                                DialogResult result;
                                result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI HOC SINH NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                    frmHocSinh frm = new frmHocSinh(MaLop);
                                    this.Close();
                                    frm.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("SĐT nhập không đúng!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã Học Sinh Phải Đủ 6 Kí Tự !", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Mã Học Sinh Bị Trùng !", "Thông Báo");
                }
            }
            else
            {
                MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
            }
            con.Close();
        }
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Remove();
                i--;
            }
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (txtTuKhoa.Text != "" && txtTuKhoa.Text != "Ví Dụ: HS0001 / Nguyễn Văn A")
            {

                if (KiemTra() == 1)
                {
                    cmd.CommandText = "SELECT MaHS,TenHS,GioiTinh,NgaySinh,DiaChi,SDT,TenLop FROM HOCSINH HS,LOP WHERE HS.MaLop = LOP.MaLop AND HS.MaLop = '" + MaLop + "' AND HS.MaHS='" + txtTuKhoa.Text + "'";
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();

                    DataTable td = new DataTable();
                    td.Load(rd);

                    if (td.Rows.Count != 0)
                    {
                        DateTime dt;
                        for (int i = 0; i < td.Rows.Count; i++)
                        {
                            ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                            item.SubItems.Add(td.Rows[i][1].ToString());
                            if (td.Rows[i][2].ToString() == "True")
                                item.SubItems.Add("Nam");
                            else
                                item.SubItems.Add("Nữ");
                            dt = DateTime.Parse(td.Rows[i][3].ToString());
                            item.SubItems.Add(dt.ToString("dd/MM/yyyy"));
                            item.SubItems.Add(td.Rows[i][4].ToString());
                            item.SubItems.Add(td.Rows[i][5].ToString());
                            item.SubItems.Add(td.Rows[i][6].ToString());
                            listView1.Items.Add(item);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Mã " + txtTuKhoa.Text);
                        frmHocSinh_Load(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
                    this.txtTuKhoa.Text = "Ví Dụ: HS0001 / Nguyễn Văn A";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "SELECT MaHS,TenHS,GioiTinh,NgaySinh,DiaChi,SDT,TenLop FROM HOCSINH HS,LOP WHERE HS.MaLop = LOP.MaLop AND HS.MaLop = '" + MaLop + "' AND HS.TenHS like N'%" + txtTuKhoa.Text + "%'";
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();

                    DataTable td = new DataTable();
                    td.Load(rd);

                    if (td.Rows.Count != 0)
                    {
                        DateTime dt;
                        for (int i = 0; i < td.Rows.Count; i++)
                        {
                            ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                            item.SubItems.Add(td.Rows[i][1].ToString());
                            if (td.Rows[i][2].ToString() == "True")
                                item.SubItems.Add("Nam");
                            else
                                item.SubItems.Add("Nữ");
                            dt = DateTime.Parse(td.Rows[i][3].ToString());
                            item.SubItems.Add(dt.ToString("dd/MM/yyyy"));
                            item.SubItems.Add(td.Rows[i][4].ToString());
                            item.SubItems.Add(td.Rows[i][5].ToString());
                            item.SubItems.Add(td.Rows[i][6].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Tên " + txtTuKhoa.Text);
                        frmHocSinh_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    frmHocSinh_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                frmHocSinh_Load(sender, e);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData2(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dl = new OpenFileDialog();
            dl.InitialDirectory = Application.StartupPath + @"hinhanh/";
            if (dl.ShowDialog() == DialogResult.OK)
            {
                hinhanh = dl.FileName.Substring(dl.FileName.LastIndexOf("\\") + 1, dl.FileName.Length - dl.FileName.LastIndexOf("\\") - 1);
                pictureBox1.Image = new Bitmap(Application.StartupPath + @"\hinhanh\" + hinhanh);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int row = this.listView1.SelectedItems[0].Index;
                str = this.listView1.Items[row].SubItems[0].Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM HOCSINH WHERE MaHS='" + txtMaHS.Text + "'";
                if (str == txtMaHS.Text)
                {
                    DialogResult result;
                    result = MessageBox.Show("BẠN CÓ MUỐN XOÁ HỌC SINH NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                        frmHocSinh frm = new frmHocSinh(MaLop);
                        this.Close();
                        frm.Show();
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Mã Học Sinh Không Khớp. Vui lòng thử lại !", "THÔNG BÁO");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn học sinh muốn xóa", "THÔNG BÁO");
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem items in listView1.SelectedItems)
            {
                str = items.SubItems[0].Text;
            }
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM HOCSINH WHERE MaHS='" + str + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaHS.Text = td.Rows[0][0].ToString();
            this.txtTenHS.Text = td.Rows[0][1].ToString();
            DateTime dt = DateTime.Parse(td.Rows[0][2].ToString());
            this.dateTimePicker1.Text = dt.ToString();
            this.txtSDT.Text = td.Rows[0][4].ToString();
            this.txtDiaChi.Text = td.Rows[0][5].ToString();
            hinhanh = td.Rows[0][7].ToString();
            if (hinhanh.Length <= 0)
            {
                this.pictureBox1.Image = new Bitmap(Application.StartupPath + @"\hinhanh\vodien.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                this.pictureBox1.Image = new Bitmap(Application.StartupPath + @"\hinhanh\" + hinhanh);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            if (td.Rows[0][3].ToString() == "True")
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
            con.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
