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
    public partial class Form15: Form
    {
        string sqlString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        public Form15()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
        private void ShowPanel(Panel panel)
        {
            foreach (Control ctrl in panelcontrol.Controls)
            {
                if (ctrl is Panel)
                    ctrl.Visible = false;
            }

            panel.Visible = true;
            panel.BringToFront();
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            LoadThongTin();
            BangDangKi();
            lblMNV.Text = "Mã nhân viên: " + User.ID_NhanVien;
            lblNV.Text = "Nhân viên: " + User.HoTen;
            LoadDangKyCaLam();

            //Load cho bảng đăng kí ở mục thông tin
            try
            {
                using (SqlConnection sqlconnect = new SqlConnection(sqlString))
                {
                    sqlconnect.Open();
                    string query = @"SELECT NgayDangKy,CaLam,GhiChu FROM DangKylam";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, sqlconnect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvbangdk.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex);
            }

        }
        private void LoadThongTin()
        {
            lblHoTen.Text = "Họ và tên: " + User.HoTen;
            lblDiachi.Text = "Địa chỉ: " + User.DiaChi;
            lblChucVu.Text = "Chức vụ: " + User.ChucVu;
            lblCCCD.Text = "Email: " + User.Email;
            lblGioiTinh.Text = "Giới Tính: " + User.GioiTinh;
            lblNgaySinh.Text = "Ngày Sinh:" + User.NgaySinh.ToString("dd/MM/yyyy");
            lblSDT.Text = "Số điện thoại: " + User.SDT;
            lblPhongBan.Text = "Ban quản lý: " + User.PhongBan;
            lblQueQuan.Text = "Quê quán: " + User.QueQuan;
            lblID.Text = "ID Nhân viên: " + User.ID_NhanVien;
        }

        private void BangDangKi()
        {
            dgvDangKy.Columns.Clear();
            dgvDangKy.Rows.Clear();

            dgvDangKy.Columns.Add("Loai", "Loại");
            dgvDangKy.Rows.Add("Thứ 7");
            dgvDangKy.Rows.Add("Chủ Nhật");

            int nam = DateTime.Now.Year;
            int thang = (cbThang.SelectedItem != null) ? int.Parse(cbThang.SelectedItem.ToString()) : DateTime.Now.Month;

            for (int i = 1; i <= DateTime.DaysInMonth(nam, thang); i++)
            {
                DateTime ngay = new DateTime(nam, thang, i);
                if (ngay.DayOfWeek == DayOfWeek.Saturday || ngay.DayOfWeek == DayOfWeek.Sunday)
                {
                    var col = new DataGridViewCheckBoxColumn() { HeaderText = $"{i}/{thang}", Name = $"Ngay_{i}" };
                    dgvDangKy.Columns.Add(col);
                }
            }

            // Hiển thị tất cả checkbox lên
            for (int i = 1; i < dgvDangKy.ColumnCount; i++)
            {
                dgvDangKy.Rows[0].Cells[i].Value = false; // Thứ 7
                dgvDangKy.Rows[1].Cells[i].Value = false; // Chủ Nhật
            }
        }



        private void LoadDangKyCaLam()
        {
            using (SqlConnection conn = new SqlConnection(sqlString))
            {
                conn.Open();
                string query = "SELECT NgayDangKy, CaLam FROM DangKyLam WHERE ID_NhanVien = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", User.ID_NhanVien);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ngay = ((DateTime)reader["NgayDangKy"]).Day;
                            string caLam = reader["CaLam"].ToString().Trim();

                            // Tìm đúng cột ngày tương ứng trong DataGridView
                            foreach (DataGridViewColumn col in dgvDangKy.Columns)
                            {
                                if (col is DataGridViewCheckBoxColumn && col.HeaderText.StartsWith($"{ngay}/"))
                                {
                                    int columnIndex = col.Index;

                                    // Tích vào đúng ô checkbox của Thứ 7 hoặc Chủ Nhật
                                    if (dgvDangKy.Rows[0].Cells[columnIndex].OwningRow.Cells[0].Value.ToString() == "Thứ 7")
                                    {
                                        dgvDangKy.Rows[0].Cells[columnIndex].Value = true; // Thứ 7
                                    }
                                    else
                                    {
                                        dgvDangKy.Rows[1].Cells[columnIndex].Value = true; // Chủ Nhật
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



        private void cbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            BangDangKi();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(sqlString))
            {
                conn.Open();
//xóa trc khi lưu
                string deleteQuery = "DELETE FROM DangKyLam WHERE ID_NhanVien = @id";
                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@id", User.ID_NhanVien);
                    deleteCmd.ExecuteNonQuery();
                }
                foreach (DataGridViewRow row in dgvDangKy.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int ngay = Convert.ToInt32(row.Cells[0].Value);

                        for (int i = 1; i <= 4; i++)
                        {
                            bool isChecked = Convert.ToBoolean(row.Cells[i].Value);

                            if (isChecked)
                            {
                                result += 1;
                                string query = "INSERT INTO DangKyLam (ID_NhanVien, NgayDangKy, CaLam) VALUES (@id, @ngay, @ca)";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", User.ID_NhanVien);
                                    cmd.Parameters.AddWithValue("@ngay", new DateTime(DateTime.Now.Year, DateTime.Now.Month, ngay));
                                    cmd.Parameters.AddWithValue("@ca", "Ca " + i);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            lblTongCaLam.Text = "Tổng số ngày đã đăng kí: " + result.ToString();

            MessageBox.Show("Đăng kí ca làm thành công!");
        }

        private void btnkqdk_Click(object sender, EventArgs e)
        {
            ShowPanel(panelkqdk);
            lblHoTen2.Text = "Họ và tên: " + User.HoTen;
            lblDiachi2.Text = "Địa chỉ: " + User.DiaChi;
            lblChucVu2.Text = "Chức vụ: " + User.ChucVu;
            lblCCCD2.Text = "Email: " + User.Email;
            lblGioiTinh2.Text = "Giới Tính: " + User.GioiTinh;
            lblNgaySinh2.Text = "Ngày Sinh:" + User.NgaySinh.ToString("dd/MM/yyyy");
            lblSDT2.Text = "Số điện thoại: " + User.SDT;
            lblPhongBan2.Text = "Ban quản lý: " + User.PhongBan;
            lblQueQuan2.Text = "Quê quán: " + User.QueQuan;
            try
            {
                using(SqlConnection sqlconnect = new SqlConnection(sqlString))
                {
                    sqlconnect.Open();
                    string query = @"SELECT ID_DangKy,CaLam,NgayDangKy,GhiChu FROM DangKylam";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, sqlconnect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvcalam.DataSource = dt;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowPanel(panelthongtin);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowPanel(paneldk);
        }
    }

}
