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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyNhanSu
{
    public partial class FormDangKyLam: Form
    {
        public FormDangKyLam()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = null;
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            string truyvan = "SELECT NhanVien.ID_NhanVien, DangKyLam.ID_DangKy, NhanVien.HoTen, DangKyLam.NgayDangKy FROM NhanVien JOIN DangKyLam ON NhanVien.ID_NhanVien = DangKyLam.ID_NhanVien";
            SqlDataAdapter data = new SqlDataAdapter(truyvan, conn);
            DataTable dataTable = new DataTable();
            data.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                return;
            }
            string query = @"
            SELECT 
                NhanVien.ID_NhanVien AS NhanVienID, 
                DangKyLam.ID_DangKy, 
                NhanVien.HoTen, 
                DangKyLam.NgayDangKy 
            FROM NhanVien 
            JOIN DangKyLam ON NhanVien.ID_NhanVien = DangKyLam.ID_NhanVien 
            WHERE NhanVien.ID_NhanVien = @MaNV"; 
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", textBox1.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }
    }
}
