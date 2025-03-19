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
            string truyvan = "select * from Luong";
            SqlDataAdapter data = new SqlDataAdapter(truyvan, conn);
            DataTable dataTable = new DataTable();
            data.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
    }
}
