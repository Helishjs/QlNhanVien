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
    public partial class Form5: Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        string ketnoi = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = null;

        private void Form5_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ketnoi);
            conn.Open();
            string tv = "select * from Loi";
            SqlDataAdapter adapter = new SqlDataAdapter(tv, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox2.SelectedItem == null)
            {
                return;
            }
            string query = @"
            SELECT *
            FROM Loi
            WHERE ID_NhanVien = @MaNV AND YEAR(NgayPhatSinh) = @Nam
            ORDER BY NgayPhatSinh";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", textBox1.Text);
            cmd.Parameters.AddWithValue("@Nam", comboBox2.SelectedItem);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || comboBox3.SelectedItem == null)
            {
                return;
            }
            string lenh = @"insert into Loi 
            (ID_NhanVien, NgayPhatSinh, MoTa, HinhThucXuLy, SoTienPhat)
            values (@idnhanvien, @ngayphatsinh, @mota, @xuly, @tienphat)";
            string xuly = "";
            decimal tienphat = 0;
            if(comboBox3.SelectedItem.ToString() == comboBox3.Items[0].ToString())
            {
                xuly = "Phạt tiền";
                tienphat = 50000;
            }
            else if(comboBox3.SelectedItem.ToString() == comboBox3.Items[1].ToString())
            {
                xuly = "Cảnh cáo";
                tienphat = 0;
            }
            SqlCommand cmd = new SqlCommand(lenh, conn);
            cmd.Parameters.AddWithValue("@idnhanvien", textBox1.Text);
            cmd.Parameters.AddWithValue("@ngayphatsinh", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@mota", comboBox3.SelectedItem);
            cmd.Parameters.AddWithValue("@xuly", xuly);
            cmd.Parameters.AddWithValue("@tienphat", tienphat);
            cmd.ExecuteNonQuery();
            string tv = "select * from Loi";
            SqlDataAdapter adapter = new SqlDataAdapter(tv, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
