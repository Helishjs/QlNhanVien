using Microsoft.SqlServer.Server;
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

namespace QuanLyNhanSu
{
    public partial class FormNhanvien : Form
    {
        string sqlString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        public FormNhanvien()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void getNhanVien()
        {
            
            try
            {
                using(SqlConnection sqlconnect = new SqlConnection(sqlString))
                {
                    sqlconnect.Open();
                    string query = @"SELECT NhanVien.ID_NhanVien, NhanVien.HoTen, NhanVien.NgaySinh, 
                        NhanVien.GioiTinh, NhanVien.QueQuan, NhanVien.Email, 
                        NhanVien.SDT, NhanVien.SoCCCD, NhanVien.DiaChi, 
                        ChucVu.Ten_ChucVu, PhongBan.Ten_PhongBan 
                 FROM NhanVien 
                 JOIN ChucVu ON ChucVu.ID_ChucVu = NhanVien.ID_ChucVu 
                 JOIN PhongBan ON PhongBan.ID_PhongBan = NhanVien.ID_PhongBan";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, sqlconnect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dataviewNhanvien.DataSource = dt;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void FormNhanvien_Load(object sender, EventArgs e)
        {
            getNhanVien();
            LoadComboBox("PhongBan", cbPhongban);
            LoadComboBox("ChucVu", cbChucvu);
            LoadComboBox_2("TroCap", cbTroCap);
            LoadComboBox_2("BaoHiem", cbBaoHiem);
        }
        public void LoadComboBox(string value, ComboBox cb)
        {
            string query = $"SELECT ID_{value}, Ten_{value} FROM {value}";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlString))
                {
                    sqlConnection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        cb.DataSource = dt;
                        cb.DisplayMember = $"Ten_{value}";
                        cb.ValueMember = $"ID_{value}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải {value}: " + ex.Message);
            }
        }
        public void LoadComboBox_2(string value, ComboBox cb)
        {
            string query = $"SELECT ID_{value}, Loai{value} FROM {value}";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlString))
                {
                    sqlConnection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        cb.DataSource = dt;
                        cb.DisplayMember = $"Loai{value}";
                        cb.ValueMember = $"ID_{value}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải {value}: " + ex.Message);
            }
        }




        private void btnThemnv_Click(object sender, EventArgs e)
        {
            try
            {
                string hoTen = tbHoten.Text.Trim();
                string email = tbEmail.Text.Trim();
                string queQuan = tbQuequan.Text.Trim();
                string gioiTinh = tbGioitinh.Text.Trim();
                DateTime ngaySinh = dtNgaysinh.Value;
                string Role = "user";
                int soDT;
                if (!int.TryParse(tbSDT.Text, out soDT))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string diaChi = tbDiachi.Text.Trim();
                int phongBan = cbPhongban.SelectedValue != null ? Convert.ToInt32(cbPhongban.SelectedValue) : 0;
                int chucVu = cbChucvu.SelectedValue != null ? Convert.ToInt32(cbChucvu.SelectedValue) : 0;
                int baoHiem = cbBaoHiem.SelectedValue != null ? Convert.ToInt32(cbBaoHiem.SelectedValue) : 0;
                int troCap = cbTroCap.SelectedValue != null ? Convert.ToInt32(cbTroCap.SelectedValue) : 0;
                string soCCCD = tbCCCD.Text.Trim();

                // Chuyển họ tên thành username không dấu
                string username = Chuyenkhongdau.Convert(hoTen);
                string query_nhanvien = @"INSERT INTO NhanVien (Username, HoTen, Email, QueQuan, GioiTinh, NgaySinh, SDT, DiaChi, ID_PhongBan, ID_ChucVu, SoCCCD,ID_BaoHiem,ID_TroCap) 
                                  VALUES (@Username, @HoTen, @Email, @QueQuan, @GioiTinh, @NgaySinh, @SoDT, @DiaChi, @PhongBanID, @ChucVuID, @SoCCCD,@BaoHiemID,@TroCapID)";
                string query_luong = @"INSERT INTO Luong (ID_NhanVien,ThangNam,ID_LuongChucVu,ID_TroCap) VALUES (@ID_NhanVien,@ThangNam,@ID_LuongChucVu,@ID_TroCap)";
                string query_account = @"INSERT INTO NguoiDung (Username, Password, Role) VALUES (@Username, @Password, @Role)";

                using (SqlConnection sqlConnect = new SqlConnection(sqlString))
                {
                    sqlConnect.Open();
                    using (SqlTransaction transaction = sqlConnect.BeginTransaction()) // Sửa chỗ này
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(query_account, sqlConnect, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Username", username);
                                cmd.Parameters.AddWithValue("@Password", soCCCD);
                                cmd.Parameters.AddWithValue("@Role", Role);
                                cmd.ExecuteNonQuery();
                            }

                            int idNhanVien;
                            using (SqlCommand cmd = new SqlCommand(query_nhanvien + "; SELECT SCOPE_IDENTITY();", sqlConnect, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Username", username);
                                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                cmd.Parameters.AddWithValue("@Email", email);
                                cmd.Parameters.AddWithValue("@QueQuan", queQuan);
                                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                                cmd.Parameters.AddWithValue("@SoDT", soDT);
                                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                                cmd.Parameters.AddWithValue("@PhongBanID", phongBan);
                                cmd.Parameters.AddWithValue("@ChucVuID", chucVu);
                                cmd.Parameters.AddWithValue("@SoCCCD", soCCCD);
                                cmd.Parameters.AddWithValue("@BaoHiemID", baoHiem);
                                cmd.Parameters.AddWithValue("@TroCapID", troCap);

                                object result = cmd.ExecuteScalar();
                                if (result == null)
                                {
                                    MessageBox.Show("Lỗi khi lấy ID nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                idNhanVien = Convert.ToInt32(result);
                            }

                            using (SqlCommand cmd = new SqlCommand(query_luong, sqlConnect, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ID_NhanVien", idNhanVien);
                                cmd.Parameters.AddWithValue("@ThangNam", DateTime.Now);
                                cmd.Parameters.AddWithValue("@ID_LuongChucVu", chucVu);
                                cmd.Parameters.AddWithValue("@ID_TroCap", troCap);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Thêm nhân viên & tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            getNhanVien();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi thêm nhân viên & tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
