using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Threading;
using System.Data.SqlTypes;
namespace QuanLyNhanSu
{
    class GetNhanVien
    {
        string sqlstring = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        public void LoadNhanVien(DataGridView dataGridViewNhanVien)
        {
            try
            {
                using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
                {
                    sqlconnect.Open();
                    string query = @"
                                SELECT 
                                    NhanVien.ID_NhanVien, 
                                    NhanVien.HoTen, 
                                    NhanVien.NgaySinh, 
                                    NhanVien.GioiTinh, 
                                    NhanVien.QueQuan, 
                                    NhanVien.Email, 
                                    NhanVien.SDT, 
                                    NhanVien.SoCCCD, 
                                    NhanVien.DiaChi, 
                                    ChucVu.Ten_ChucVu, 
                                    PhongBan.Ten_PhongBan, 
                                    ISNULL(BaoHiem.LoaiBaoHiem, 'Không có') AS LoaiBaoHiem, 
                                    ISNULL(TroCap.LoaiTroCap, 'Không có') AS LoaiTroCap
                                FROM NhanVien 
                                JOIN ChucVu ON ChucVu.ID_ChucVu = NhanVien.ID_ChucVu 
                                JOIN PhongBan ON PhongBan.ID_PhongBan = NhanVien.ID_PhongBan
                                LEFT JOIN TroCap ON TroCap.ID_TroCap = NhanVien.ID_TroCap
                                LEFT JOIN BaoHiem ON BaoHiem.ID_BaoHiem = NhanVien.ID_BaoHiem;
                            ";


                    SqlDataAdapter adapter = new SqlDataAdapter(query, sqlconnect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dataGridViewNhanVien.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        public void XoaNhanVien(int id,string username)
        {
            //if (dataGridView.SelectedRows.Count > 0)
            //{
            //    DataGridViewRow row = dataGridView.SelectedRows[0];
                try
                {
                    using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
                    {
                        sqlconnect.Open();

                        string query1 = "DELETE FROM Luong WHERE ID_Thuong IN (SELECT ID_Thuong FROM Thuong WHERE ID_NhanVien = @id)";
                        string query2 = "DELETE FROM Thuong WHERE ID_NhanVien = @id";
                        string query3 = "DELETE FROM Luong WHERE ID_NhanVien = @id";
                        string query4 = "DELETE FROM NhanVien WHERE ID_NhanVien = @id";
                        //string query5 = "DELETE FROM NguoiDung WHERE Username = @username";
                        //using (SqlCommand cmd5 = new SqlCommand(query5, sqlconnect))
                        using (SqlCommand cmd1 = new SqlCommand(query1, sqlconnect))
                        using (SqlCommand cmd2 = new SqlCommand(query2, sqlconnect))
                        using (SqlCommand cmd3 = new SqlCommand(query3, sqlconnect))
                        using (SqlCommand cmd4 = new SqlCommand(query4, sqlconnect))
                        {
                            cmd1.Parameters.AddWithValue("@id", id);
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd3.Parameters.AddWithValue("@id", id);
                            cmd4.Parameters.AddWithValue("@id", id);
                            //cmd5.Parameters.AddWithValue("@username", username);

                            //cmd5.ExecuteNonQuery();
                            cmd1.ExecuteNonQuery();
                            cmd2.ExecuteNonQuery();
                            cmd3.ExecuteNonQuery();
                            int check = cmd4.ExecuteNonQuery();

                            if (check > 0)
                            {
                                MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi: " + ex);
                }
            //}
        }
        public void SuaNhaVien(DataGridView dataGridView)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];

                string id = row.Cells["ID_NhanVien"].Value.ToString();
                string hoTen = row.Cells["HoTen"].Value.ToString();
                DateTime ngaySinh = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                string queQuan = row.Cells["QueQuan"].Value.ToString();
                string email = row.Cells["Email"].Value.ToString();
                string sdt = row.Cells["SDT"].Value.ToString();
                string soCCCD = row.Cells["SoCCCD"].Value.ToString();
                string diaChi = row.Cells["DiaChi"].Value.ToString();
                string phongBan = row.Cells["Ten_PhongBan"].Value.ToString();
                string chucVu = row.Cells["Ten_ChucVu"].Value.ToString();

                //using (FormSuaNhanVien suaNhanVien = new FormSuaNhanVien(id, hoTen, ngaySinh, gioiTinh,
                //                                                         queQuan, email, sdt, soCCCD, diaChi, phongBan, chucVu))
                //{
                //    suaNhanVien.ShowDialog();
                //}
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SearchNhanVien(string search,DataGridView dataGridView)
        {
            try
            {
                using(SqlConnection sqlconnection = new SqlConnection(sqlstring))
                {
                    sqlconnection.Open();
                    string result =  search;
                    string query = "SELECT NhanVien.ID_NhanVien, NhanVien.HoTen, NhanVien.NgaySinh,NhanVien.GioiTinh, NhanVien.QueQuan, NhanVien.Email,NhanVien.SDT, NhanVien.SoCCCD, NhanVien.DiaChi,ChucVu.Ten_ChucVu, PhongBan.Ten_PhongBan FROM NhanVien JOIN ChucVu ON ChucVu.ID_ChucVu = NhanVien.ID_ChucVu JOIN PhongBan ON PhongBan.ID_PhongBan = NhanVien.ID_PhongBan WHERE ID_NhanVien = @Search";
                    using(SqlCommand cmd = new SqlCommand(query, sqlconnection))
                    {
                        cmd.Parameters.AddWithValue("@Search", result);
                        using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView.DataSource = dt;
                        }
                    }
                    
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex);
            }
        }
        public bool ChangePassword(string Password, string Username, string newPassword)
        {
            try
            {
                using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
                {
                    sqlconnect.Open();
                    string query_1 = @"SELECT COUNT(*) FROM NguoiDung WHERE Password = @password AND Username = @username";
                    using (SqlCommand cmd = new SqlCommand(query_1, sqlconnect))
                    {
                        cmd.Parameters.AddWithValue("@password", Password);
                        cmd.Parameters.AddWithValue("@username", Username);
                        int count = (int)(cmd.ExecuteScalar() ?? 0);
                        if (count == 0)
                        {
                            MessageBox.Show("Mật khẩu cũ không đúng!");
                            return false;
                        }
                    }

                    string query_2 = @"UPDATE NguoiDung SET Password = @newPassword WHERE Username = @username";
                    using (SqlCommand cmd = new SqlCommand(query_2, sqlconnect))
                    {
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@username", Username);
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công!");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật mật khẩu thất bại!");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                return false;
            }
        }
        public bool ResetPassword(string Username,string CCCD)
        {
            try
            {
                using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
                {
                    sqlconnect.Open();
                    string query_1 = @"SELECT COUNT(*) FROM NhanVien WHERE SoCCCD = @cccd AND Username = @username";
                    using (SqlCommand cmd = new SqlCommand(query_1, sqlconnect))
                    {
                        cmd.Parameters.AddWithValue("@cccd", CCCD);
                        cmd.Parameters.AddWithValue("@username", Username);
                        int count = (int)(cmd.ExecuteScalar() ?? 0);
                        if (count == 0)
                        {
                            MessageBox.Show("Tên đăng nhập hoặc cccd không chính xác");
                            return false;
                        }
                    }

                    string query_2 = @"UPDATE NguoiDung SET Password = @newPassword WHERE Username = @username";
                    using (SqlCommand cmd = new SqlCommand(query_2, sqlconnect))
                    {
                        cmd.Parameters.AddWithValue("@newPassword",CCCD);
                        cmd.Parameters.AddWithValue("@username", Username);
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công!");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật mật khẩu thất bại!");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                return false;
            }
        }

        public void LogOut(Form currentForm)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                User.Username = null;
                User.ID_NhanVien = 0;
                User.Password = null;
                User.Role = null;

                currentForm.Close();
                Application.Restart();

            }
        }


        public void UpdateNhanVien(string id, string hoTen, DateTime ngaySinh, string gioiTinh,
                           string queQuan, string email, string sdt, string soCCCD,
                           string diaChi, string phongBan, string chucVu)
        {
            try
            {
                using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
                {
                    sqlconnect.Open();
                    string query = @"UPDATE NhanVien 
                 SET HoTen = @HoTen, 
                     NgaySinh = @NgaySinh, 
                     GioiTinh = @GioiTinh, 
                     QueQuan = @QueQuan, 
                     Email = @Email, 
                     SDT = @SDT, 
                     SoCCCD = @SoCCCD, 
                     DiaChi = @DiaChi,
                     ID_PhongBan = @PhongBan, 
                     ID_ChucVu = @ChucVu 
                 WHERE ID_NhanVien = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, sqlconnect))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        cmd.Parameters.AddWithValue("@QueQuan", queQuan);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@SoCCCD", soCCCD);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@PhongBan", Convert.ToInt32(phongBan));
                        cmd.Parameters.AddWithValue("@ChucVu", Convert.ToInt32(chucVu));
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex);
            }
        }
    }
}


