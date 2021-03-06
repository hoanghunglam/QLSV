﻿using System;
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
    public partial class frmGiaoVien : Form
    {
        string str;
        public frmGiaoVien()
        {
            InitializeComponent();
        }

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
            this.txtTuKhoa.Text = "Ví Dụ: GV0001 / Nguyễn Văn A";
            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmd.CommandText = "SELECT * FROM GIAOVIEN";
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
            string sql = "Select TenGV from GIAOVIEN";

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
            string sql = "Select MaGV from GIAOVIEN";

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
            if (txtTuKhoa.Text != "" && txtTuKhoa.Text != "Ví Dụ: GV0001 / Nguyễn Văn A")
            {

                if (KiemTra() == 1)
                {
                    cmd.CommandText = "SELECT * FROM GIAOVIEN WHERE MaGV='" + txtTuKhoa.Text + "'";
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
                            listView1.Items.Add(item);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Giáo Viên Có Mã " + txtTuKhoa.Text);
                        frmGiaoVien_Load(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus);
                    this.txtTuKhoa.Text = "Ví Dụ: GV0001 / Nguyễn Văn A";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "SELECT * FROM GIAOVIEN WHERE TenGV like N'%" + txtTuKhoa.Text + "%'";
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
                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Giáo Viên Có Tên " + txtTuKhoa.Text);
                        frmGiaoVien_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    frmGiaoVien_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                frmGiaoVien_Load(sender, e);
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
            cmd.CommandText = "SELECT * FROM GIAOVIEN WHERE MaGV='" + str + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaGV.Text = td.Rows[0][0].ToString();
            this.txtTenGV.Text = td.Rows[0][1].ToString();
            this.txtSDT.Text = td.Rows[0][2].ToString();
            this.txtDiaChi.Text = td.Rows[0][3].ToString();
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
                cmd.CommandText = "DELETE FROM GIAOVIEN WHERE MaGV='" + txtMaGV.Text + "'";
                if (str == txtMaGV.Text)
                {
                    DialogResult result;
                    result = MessageBox.Show("BẠN CÓ MUỐN XOÁ GIÁO VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                        frmGiaoVien frm = new frmGiaoVien();
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
                MessageBox.Show("Hãy chọn giáo viên muốn xóa", "THÔNG BÁO");
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
            cmd2.CommandText = "select * from GIAOVIEN where MaGV='" + txtMaGV.Text + "'";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            if (txtMaGV.Text != "" && txtTenGV.Text != "")
            {
                if (td2.Rows.Count == 0)
                {
                    if (txtMaGV.Text.Length == 6)
                    {
                        if (txtSDT.Text == "")
                        {
                            cmd.CommandText = "insert into GIAOVIEN values ('" + txtMaGV.Text + "',N'" + txtTenGV.Text + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "')";
                            DialogResult result;
                            result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI GIÁO VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                frmGiaoVien frm = new frmGiaoVien();
                                this.Close();
                                frm.Show();
                            }
                        }
                        else
                        {
                            if (IsNumber(txtSDT.Text))
                            {
                                cmd.CommandText = "insert into GIAOVIEN values ('" + txtMaGV.Text + "',N'" + txtTenGV.Text + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "')";
                                DialogResult result;
                                result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI GIÁO VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                    frmGiaoVien frm = new frmGiaoVien();
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
                        MessageBox.Show("Mã Giáo Viên Phải Đủ 6 Kí Tự !", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Mã Giáo Viên Bị Trùng !", "Thông Báo");
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
                cmd2.CommandText = "select * from GIAOVIEN where MaGV='" + txtMaGV.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                if (txtMaGV.Text != "" && txtTenGV.Text != "")
                {
                    if (td2.Rows.Count == 1)
                    {
                        if (txtMaGV.Text == str)
                        {
                            if (txtSDT.Text == "")
                            {
                                cmd.CommandText = "UPDATE GIAOVIEN SET TenGV=N'" + txtTenGV.Text + "',SDT='" + txtSDT.Text + "',DiaChi=N'" + txtDiaChi.Text + "' WHERE MaGV='" + str + "'";
                                DialogResult result;
                                result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                    frmGiaoVien frm = new frmGiaoVien();
                                    this.Close();
                                    frm.Show();
                                }
                            }
                            else
                            {
                                if (IsNumber(txtSDT.Text))
                                {
                                    cmd.CommandText = "UPDATE GIAOVIEN SET TenGV=N'" + txtTenGV.Text + "',SDT='" + txtSDT.Text + "',DiaChi=N'" + txtDiaChi.Text + "' WHERE MaGV='" + str + "'";
                                    DialogResult result;
                                    result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                        frmGiaoVien frm = new frmGiaoVien();
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
                            MessageBox.Show("Mã Giáo Viên Không Thể Sửa !", "Thông Báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã Giáo Viên Không Tồn Tại !", "Thông Báo");
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
                MessageBox.Show("Hãy chọn giáo viên muốn sửa", "THÔNG BÁO");
            }
        }
    }
}
