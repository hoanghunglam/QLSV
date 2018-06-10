using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHocSinhGiaoVien
{
    class chkDangNhap
    {
        public bool LoginHeThong(string TenDangNhap, string MatKhau)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenDN,MatKhau FROM QUANLYNGUOIDUNG WHERE TenDN=@TenDN AND MatKhau=@MK";
            cmd.Parameters.AddWithValue("@TenDN", TenDangNhap);
            cmd.Parameters.AddWithValue("@MK", MatKhau);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
    }
}
