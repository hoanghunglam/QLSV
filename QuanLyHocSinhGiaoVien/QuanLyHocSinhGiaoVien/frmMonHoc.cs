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
    public partial class frmMonHoc : Form
    {
        string str;
        public frmMonHoc()
        {
            InitializeComponent();
        }

        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenGV FROM GIAOVIEN";
            SqlDataReader rd1;
            rd1 = cmd.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxTenGVD.Items.Add(td1.Rows[i][0]);
            }
            cmd.CommandText = "SELECT TenLop FROM LOP";
            rd1 = cmd.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd1);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbxTenLop.Items.Add(td2.Rows[i][0]);
            }
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
            this.txtTuKhoa.Text = "Ví Dụ: MH0001 / Toán";
            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmd.CommandText = "SELECT MH.MaMH,MH.TenMH,MH.ThoiGian,GV.TenGV,LOP.TenLop FROM MONHOC MH,GIAOVIEN GV,LOP WHERE MH.MaGVD=GV.MaGV AND LOP.MaLop=MH.MaLop";
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
            string sql = "Select TenMH from MONHOC";
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
            string sql = "Select MaMH from MONHOC";
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
            if (txtTuKhoa.Text != "" && txtTuKhoa.Text != "Ví Dụ: MH0001 / Toán")
            {

                if (KiemTra() == 1)
                {
                    cmd.CommandText = "SELECT MH.MaMH,MH.TenMH,MH.ThoiGian,GV.TenGV,LOP.TenLop FROM MONHOC MH,GIAOVIEN GV,LOP WHERE MH.MaGVD=GV.MaGV AND LOP.MaLop=MH.MaLop AND MH.MaMH='" + txtTuKhoa.Text + "'";
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
                            listView1.Items.Add(item);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Môn Học Có Mã " + txtTuKhoa.Text);
                        frmMonHoc_Load(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
                    this.txtTuKhoa.Text = "Ví Dụ: MH0001 / Toán";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "SELECT MH.MaMH,MH.TenMH,MH.ThoiGian,GV.TenGV,LOP.TenLop FROM MONHOC MH,GIAOVIEN GV,LOP WHERE MH.MaGVD=GV.MaGV AND LOP.MaLop=MH.MaLop AND MH.TenMH like N'%" + txtTuKhoa.Text + "%'";
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
                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Môn Học Có Tên " + txtTuKhoa.Text);
                        frmMonHoc_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    frmMonHoc_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                frmMonHoc_Load(sender, e);
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
            cmd.CommandText = "SELECT MH.MaMH,MH.TenMH,MH.ThoiGian,GV.TenGV,LOP.TenLop FROM MONHOC MH,GIAOVIEN GV,LOP WHERE MH.MaGVD=GV.MaGV AND LOP.MaLop=MH.MaLop AND MH.MaMH='" + str + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaMH.Text = td.Rows[0][0].ToString();
            this.txtTenMH.Text = td.Rows[0][1].ToString();
            this.txtThoiGian.Text = td.Rows[0][2].ToString();
            this.cbxTenGVD.Text = td.Rows[0][3].ToString();
            this.cbxTenLop.Text = td.Rows[0][4].ToString();
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
                cmd.CommandText = "DELETE FROM MONHOC WHERE MaMH='" + txtMaMH.Text + "'";
                if (str == txtMaMH.Text)
                {
                    DialogResult result;
                    result = MessageBox.Show("BẠN CÓ MUỐN XOÁ MÔN HỌC NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                        frmMonHoc frm = new frmMonHoc();
                        this.Close();
                        frm.Show();
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Mã Môn Học Không Khớp. Vui lòng thử lại !", "THÔNG BÁO");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn môn học muốn xóa", "THÔNG BÁO");
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
            cmd2.CommandText = "select * from MONHOC where MaMH='" + txtMaMH.Text + "'";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            if (txtMaMH.Text != "" && txtTenMH.Text != "" && txtThoiGian.Text != "" && cbxTenGVD.Text != "" && cbxTenLop.Text != "")
            {
                if (td2.Rows.Count == 0)
                {
                    if (txtMaMH.Text.Length == 6)
                    {
                        string maGV, maLop;
                        cmd2.CommandText = "select MaGV from GIAOVIEN where TenGV=N'" + cbxTenGVD.Text + "'";
                        rd2 = cmd2.ExecuteReader();
                        DataTable td3 = new DataTable();
                        td3.Load(rd2);
                        maGV = td3.Rows[0][0].ToString();
                        cmd2.CommandText = "select MaLop from LOP where TenLop=N'" + cbxTenLop.Text + "'";
                        rd2 = cmd2.ExecuteReader();
                        DataTable td4 = new DataTable();
                        td4.Load(rd2);
                        maLop = td4.Rows[0][0].ToString();
                        cmd.CommandText = "insert into MONHOC values ('" + txtMaMH.Text + "',N'" + txtTenMH.Text + "','" + maGV + "',N'" + txtThoiGian.Text + "','" + maLop + "')";
                        DialogResult result;
                        result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI MÔN HỌC NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                            frmMonHoc frm = new frmMonHoc();
                            this.Close();
                            frm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã Môn Học Phải Đủ 6 Kí Tự !", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Mã Môn Học Bị Trùng !", "Thông Báo");
                }
            }
            else
            {
                MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
            }
            con.Close();
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
                cmd2.CommandText = "select * from MONHOC where MaMH='" + txtMaMH.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                if (txtMaMH.Text == str)
                {
                    if (txtTenMH.Text != "" && txtThoiGian.Text != "" && cbxTenGVD.Text != "" && cbxTenLop.Text != "")
                    {
                        if (td2.Rows.Count == 1)
                        {
                            string maGV, maLop;
                            cmd2.CommandText = "select MaGV from GIAOVIEN where TenGV=N'" + cbxTenGVD.Text + "'";
                            rd2 = cmd2.ExecuteReader();
                            DataTable td3 = new DataTable();
                            td3.Load(rd2);
                            maGV = td3.Rows[0][0].ToString();
                            cmd2.CommandText = "select MaLop from LOP where TenLop=N'" + cbxTenLop.Text + "'";
                            rd2 = cmd2.ExecuteReader();
                            DataTable td4 = new DataTable();
                            td4.Load(rd2);
                            maLop = td4.Rows[0][0].ToString();
                            cmd.CommandText = "UPDATE MONHOC SET TenMH=N'" + txtTenMH.Text + "',MaGVD='" + maGV + "',ThoiGian=N'" + txtThoiGian.Text + "',MaLop='" + maLop + "' WHERE MaMH='" + str + "'";
                            DialogResult result;
                            result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                frmMonHoc frm = new frmMonHoc();
                                this.Close();
                                frm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã Môn Học Không Tồn Tại !", "Thông Báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Mã Môn Học Không Thể Sửa !", "Thông Báo");
                }
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn môn học muốn sửa", "THÔNG BÁO");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

