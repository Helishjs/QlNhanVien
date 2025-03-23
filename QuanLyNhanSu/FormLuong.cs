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

namespace QuanLyNhanSu
{
    public partial class FormXemBangLuong: Form
    {
        public FormXemBangLuong()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = null;
        private void button1_Click_1(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || comboBox1.SelectedItem == null)
            {
                return;
            }
            string query = @"
            SELECT *
            FROM Luong
            WHERE ID_NhanVien = @MaNV AND YEAR(ThangNam) = @Nam
            ORDER BY ThangNam";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", textBox1.Text);
            cmd.Parameters.AddWithValue("@Nam", comboBox1.SelectedItem);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private void FormXemBangLuong_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            string truyvan = "SELECT Luong.ID_Luong, NhanVien.ID_NhanVien, NhanVien.HoTen, Luong.ThangNam, Luong.TongSoNgayLamThem, Luong_ChucVu.LuongCoBan, TroCap.SoTien AS SoTienTroCap, COALESCE(SUM(Thuong.SoTienThuong), 0) AS TongSoTienThuong, Luong.TongLuong FROM Luong JOIN NhanVien ON Luong.ID_NhanVien = NhanVien.ID_NhanVien JOIN Luong_ChucVu ON Luong.ID_LuongChucVu = Luong_ChucVu.ID_LuongChucVu LEFT JOIN TroCap ON Luong.ID_TroCap = TroCap.ID_TroCap LEFT JOIN Thuong ON Luong.ID_NhanVien = Thuong.ID_NhanVien LEFT JOIN PhamLoi ON NhanVien.ID_NhanVien = PhamLoi.ID_NhanVien GROUP BY Luong.ID_Luong, NhanVien.ID_NhanVien, NhanVien.HoTen, Luong.ThangNam, Luong.TongSoNgayLamThem, Luong_ChucVu.LuongCoBan, TroCap.SoTien, Luong.TongLuong ORDER BY Luong.ThangNam DESC;";
            SqlDataAdapter data = new SqlDataAdapter(truyvan, conn);
            DataTable dataTable = new DataTable();
            data.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string truyvan = @"
        UPDATE Luong
        SET TongLuong = Luong_ChucVu.LuongCoBan * DATEDIFF(DAY, Luong.ThangNam, GETDATE()) 
                     + Luong.TongSoNgayLamThem * 600000
                     + COALESCE((SELECT SUM(Thuong.SoTienThuong) 
                                 FROM Thuong 
                                 WHERE Thuong.ID_NhanVien = Luong.ID_NhanVien), 0)
                     + COALESCE(TroCap.SoTien, 0)
        FROM Luong
        JOIN Luong_ChucVu ON Luong.ID_LuongChucVu = Luong_ChucVu.ID_LuongChucVu
        LEFT JOIN TroCap ON Luong.ID_TroCap = TroCap.ID_TroCap;
    ";

                using (SqlCommand cmd = new SqlCommand(truyvan, conn))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Cập nhật thành công {rowsAffected} dòng.");
                }
            }

        }
    }
}
