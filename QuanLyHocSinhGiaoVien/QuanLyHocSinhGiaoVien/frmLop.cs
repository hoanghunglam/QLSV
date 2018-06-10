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
    public partial class frmLop : Form
    {
        string str;
        public frmLop()
        {
            InitializeComponent();
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenHS FROM HOCSINH WHERE MaHS NOT IN ( SELECT MaLopTruong FROM dbo.LOP )";
            SqlDataReader rd1;
            rd1 = cmd.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxTenLT.Items.Add(td1.Rows[i][0]);
            }
            cmd.CommandText = "SELECT TenGV FROM GIAOVIEN WHERE MaGV NOT IN ( SELECT MaGVCN FROM dbo.LOP )";
            rd1 = cmd.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd1);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbxTenGVCN.Items.Add(td2.Rows[i][0]);
            }
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
            this.txtTuKhoa.Text = "Ví Dụ: LOP10A / Lớp 10A";
            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmd.CommandText = "SELECT LOP.MaLop,TenLop,DiaDiem,SoLuongHS,TenHS,TenGV FROM LOP,GIAOVIEN GV,HOCSINH HS WHERE LOP.MaLopTruong=HS.MaHS AND LOP.MaGVCN=GV.MaGV";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                item.SubItems.Add(td.Rows[i][1].ToString());
                item.SubItems.Add(td.Rows[i][2].ToString());
                item.SubItems.Add(td.Rows[i][3].ToString());
                item.SubItems.Add(td.Rows[i][4].ToString());
                item.SubItems.Add(td.Rows[i][5].ToString());
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
            string sql = "Select TenLop from LOP";

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
            string sql = "Select MaLop from LOP";

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
            if (txtTuKhoa.Text != "" && txtTuKhoa.Text != "Ví Dụ: LOP10A / Lớp 10A")
            {

                if (KiemTra() == 1)
                {
                    cmd.CommandText = "SELECT LOP.MaLop,TenLop,DiaDiem,SoLuongHS,TenHS,TenGV FROM LOP,GIAOVIEN GV,HOCSINH HS WHERE LOP.MaLopTruong=HS.MaHS AND LOP.MaGVCN=GV.MaGV AND LOP.MaLop='" + txtTuKhoa.Text + "'";
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();
                    DataTable td = new DataTable();
                    td.Load(rd);

                    if (td.Rows.Count != 0)
                    {
                        for (int i = 0; i < td.Rows.Count; i++)
                        {
                            ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                            item.SubItems.Add(td.Rows[i][1].ToString());
                            item.SubItems.Add(td.Rows[i][2].ToString());
                            item.SubItems.Add(td.Rows[i][3].ToString());
                            item.SubItems.Add(td.Rows[i][4].ToString());
                            item.SubItems.Add(td.Rows[i][5].ToString());
                            listView1.Items.Add(item);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Lớp Có Mã " + txtTuKhoa.Text);
                        frmLop_Load(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
                    this.txtTuKhoa.Text = "Ví Dụ: LOP10A / Lớp 10A";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "SELECT LOP.MaLop,TenLop,DiaDiem,SoLuongHS,TenHS,TenGV FROM LOP,GIAOVIEN GV,HOCSINH HS WHERE LOP.MaLopTruong=HS.MaHS AND LOP.MaGVCN=GV.MaGV AND LOP.TenLop like N'%" + txtTuKhoa.Text + "%'";
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();

                    DataTable td = new DataTable();
                    td.Load(rd);
                    if (td.Rows.Count != 0)
                    {
                        for (int i = 0; i < td.Rows.Count; i++)
                        {
                            ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                            item.SubItems.Add(td.Rows[i][1].ToString());
                            item.SubItems.Add(td.Rows[i][2].ToString());
                            item.SubItems.Add(td.Rows[i][3].ToString());
                            item.SubItems.Add(td.Rows[i][4].ToString());
                            item.SubItems.Add(td.Rows[i][5].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Lớp Có Tên " + txtTuKhoa.Text);
                        frmLop_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    frmLop_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                frmLop_Load(sender, e);
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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
            cmd.CommandText = "SELECT LOP.MaLop,TenLop,DiaDiem,SoLuongHS,TenHS,TenGV FROM LOP,GIAOVIEN GV,HOCSINH HS WHERE LOP.MaLopTruong=HS.MaHS AND LOP.MaGVCN=GV.MaGV AND LOP.MaLop='" + str + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaLop.Text = td.Rows[0][0].ToString();
            this.txtTenLop.Text = td.Rows[0][1].ToString();
            this.txtDiaDiem.Text = td.Rows[0][2].ToString();
            this.txtSLHS.Text = td.Rows[0][3].ToString();
            this.textBox1.Text = td.Rows[0][4].ToString();
            this.textBox2.Text = td.Rows[0][5].ToString();
            con.Close();
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
                cmd.CommandText = "DELETE FROM LOP WHERE MaLop='" + txtMaLop.Text + "'";
                if (str == txtMaLop.Text)
                {
                    DialogResult result;
                    result = MessageBox.Show("BẠN CÓ MUỐN XOÁ LỚP NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                        frmLop frm = new frmLop();
                        this.Close();
                        frm.Show();
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Mã Lớp Không Khớp. Vui lòng thử lại !", "THÔNG BÁO");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn lớp muốn xóa", "THÔNG BÁO");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "select * from LOP where MaLop='" + txtMaLop.Text + "'";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            if (txtMaLop.Text != "" && txtTenLop.Text != "" && txtDiaDiem.Text != "" && txtSLHS.Text != "" && cbxTenLT.Text != "" && cbxTenGVCN.Text != "")
            {
                if (td2.Rows.Count == 0)
                {
                    if (txtMaLop.Text.Length == 6)
                    {
                        if (IsNumber(txtSLHS.Text))
                        {
                            string maLT, maGVCN;
                            cmd2.CommandText = "select MaHS from HOCSINH where TenHS=N'" + cbxTenLT.Text + "'";
                            rd2 = cmd2.ExecuteReader();
                            DataTable td3 = new DataTable();
                            td3.Load(rd2);
                            maLT = td3.Rows[0][0].ToString();
                            cmd2.CommandText = "select MaGV from GIAOVIEN where TenGV=N'" + cbxTenGVCN.Text + "'";
                            rd2 = cmd2.ExecuteReader();
                            DataTable td4 = new DataTable();
                            td4.Load(rd2);
                            maGVCN = td4.Rows[0][0].ToString();
                            cmd.CommandText = "insert into LOP values ('" + txtMaLop.Text + "',N'" + txtTenLop.Text + "',N'" + txtDiaDiem.Text + "','" + txtSLHS.Text + "','" + maLT + "','" + maGVCN + "')";
                            DialogResult result;
                            result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI LỚP NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                frmLop frm = new frmLop();
                                this.Close();
                                frm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng học sinh nhập không đúng!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Mã Lớp Phải Đủ 6 Kí Tự !", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Mã Lớp Bị Trùng !", "Thông Báo");
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

        private void button2_Click(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "select * from LOP where MaLop='" + txtMaLop.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                if (txtMaLop.Text == str)
                {
                    if (txtTenLop.Text != "" && txtDiaDiem.Text != "" && txtSLHS.Text != "")
                    {
                        if (cbxTenLT.Text != "" && cbxTenGVCN.Text != "")
                        {
                            if (td2.Rows.Count == 1)
                            {
                                if (IsNumber(txtSLHS.Text))
                                {
                                    string maLT, maGVCN;
                                    cmd2.CommandText = "select MaHS from HOCSINH where TenHS=N'" + cbxTenLT.Text + "'";
                                    rd2 = cmd2.ExecuteReader();
                                    DataTable td3 = new DataTable();
                                    td3.Load(rd2);
                                    maLT = td3.Rows[0][0].ToString();
                                    cmd2.CommandText = "select MaGV from GIAOVIEN where TenGV=N'" + cbxTenGVCN.Text + "'";
                                    rd2 = cmd2.ExecuteReader();
                                    DataTable td4 = new DataTable();
                                    td4.Load(rd2);
                                    maGVCN = td4.Rows[0][0].ToString();
                                    cmd.CommandText = "UPDATE LOP SET TenLop=N'" + txtTenLop.Text + "',DiaDiem=N'" + txtDiaDiem.Text + "',SoLuongHS='" + txtSLHS.Text + "',MaLopTruong='" + maLT + "',MaGVCN='" + maGVCN + "' WHERE LOP.MaLop='" + str + "'";
                                    DialogResult result;
                                    result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                        frmLop frm = new frmLop();
                                        this.Close();
                                        frm.Show();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("SĐT nhập không đúng!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã Lớp Không Tồn Tại !", "Thông Báo");
                            }
                        }
                        else
                        {
                            if (td2.Rows.Count == 1)
                            {
                                if (IsNumber(txtSLHS.Text))
                                {
                                    cmd.CommandText = "UPDATE LOP SET TenLop=N'" + txtTenLop.Text + "',DiaDiem=N'" + txtDiaDiem.Text + "',SoLuongHS='" + txtSLHS.Text + "' WHERE LOP.MaLop='" + str + "'";
                                    DialogResult result;
                                    result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                        frmLop frm = new frmLop();
                                        this.Close();
                                        frm.Show();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("SĐT nhập không đúng!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã Lớp Không Tồn Tại !", "Thông Báo");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Mã lớp Không Thể Sửa !", "Thông Báo");
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn lớp muốn sửa", "THÔNG BÁO");
            }
        }
    }
}
