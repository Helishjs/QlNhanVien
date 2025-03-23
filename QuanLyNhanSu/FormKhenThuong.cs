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
    public partial class FormKhenThuong: Form
    {
        public FormKhenThuong()
        {
            InitializeComponent();
        }
        string ketnoi = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = null;

        private void Form6_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ketnoi);
            conn.Open();
            string tv = "select * from Thuong";
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
            FROM Thuong
            WHERE ID_NhanVien = @MaNV AND YEAR(NgayKhenThuong) = @Nam
            ORDER BY NgayKhenThuong";
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
            if (textBox1.Text == "" || comboBox3.SelectedItem == null)
            {
                return;
            }
            string lenh = @"insert into Thuong 
            (ID_NhanVien, NgayKhenThuong, MoTa, SoTienThuong)
            values (@idnhanvien, @ngaykhen, @mota, @tienthuong)";
            decimal tienphat = 0;
            if (comboBox3.SelectedItem.ToString() == comboBox3.Items[0].ToString())
            {
                tienphat = 50000;
            }
            else if (comboBox3.SelectedItem.ToString() == comboBox3.Items[1].ToString())
            {
                tienphat = 0;
            }
            else if (comboBox3.SelectedItem.ToString() == comboBox3.Items[2].ToString())
            {
                tienphat = 10000;
            }
            else if (comboBox3.SelectedItem.ToString() == comboBox3.Items[3].ToString())
            {
                tienphat = 20000;
            }
            SqlTransaction transaction = conn.BeginTransaction();
            int idThuong;
            string idnhavien = textBox1.Text;
            SqlCommand cmd = new SqlCommand(lenh + "; SELECT SCOPE_IDENTITY();", conn, transaction);
            cmd.Parameters.AddWithValue("@idnhanvien", textBox1.Text);
            cmd.Parameters.AddWithValue("@ngaykhen", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@mota", comboBox3.SelectedItem);
            cmd.Parameters.AddWithValue("@tienthuong", tienphat);
            object result = cmd.ExecuteScalar();
            idThuong = Convert.ToInt32(result);
            SqlCommand cmd_1 = new SqlCommand("UPDATE Luong SET ID_Thuong = @idthuong WHERE ID_NhanVien = @idnhanvien", conn, transaction);
            cmd_1.Parameters.AddWithValue("@idthuong", idThuong);
            cmd_1.Parameters.AddWithValue("@idnhanvien", idnhavien);
            cmd_1.ExecuteNonQuery();
            transaction.Commit();
            string tv = "select * from Thuong";
            SqlDataAdapter adapter = new SqlDataAdapter(tv, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
