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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
namespace QuanLyNhanSu
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbpassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnshowpassword_Click(object sender, EventArgs e)
        {
            tbpassword.UseSystemPasswordChar = !tbpassword.UseSystemPasswordChar;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           DialogResult result = MessageBox.Show("Bạn có muốn đặt lại mật khẩu không","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                tbpassword.Visible = false;
                lblpassword.Visible = false;
                btnressetpassword.Visible = false;
                checkBox1.Visible = false;
                tbcccd.Visible = true;
                lblcccd.Visible = true;
                btnlogin.Text = "Reset";

                btnlogin.Click -= btnlogin_Click;
                btnlogin.Click -= btnResetPassword_Click;
                btnlogin.Click += btnResetPassword_Click;

            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            bool result = nv.ResetPassword(tbusername.Text, tbcccd.Text);
            if (result)
            {
                MessageBox.Show("Bạn đã reset mật khẩu thành công");
                tbpassword.Visible = true;
                lblpassword.Visible = true;
                btnressetpassword.Visible = true;
                checkBox1.Visible = true;
                tbcccd.Visible = false;
                lblcccd.Visible = false;
                btnlogin.Text = "Đăng nhập";

                btnlogin.Click -= btnResetPassword_Click;
                btnlogin.Click -= btnlogin_Click;
                btnlogin.Click += btnlogin_Click;
            }

        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    string username = tbusername.Text.Trim();
                    string password = tbpassword.Text.Trim();
                    string checkrole = @"SELECT Role FROM NguoiDung WHERE Username = @username AND Password = @password";
                    string query = @"
            SELECT NhanVien.ID_NhanVien, NguoiDung.Role,NhanVien.SoCCCD,NhanVien.NgaySinh,NhanVien.GioiTinh,NhanVien.QueQuan,NhanVien.SDT,NhanVien.DiaChi,NhanVien.Email,ChucVu.Ten_ChucVu,PhongBan.Ten_PhongBan,NhanVien.HoTen
            FROM NguoiDung JOIN NhanVien ON NhanVien.Username = NguoiDung.Username
            JOIN PhongBan ON NhanVien.ID_PhongBan = PhongBan.ID_PhongBan
            JOIN ChucVu ON NhanVien.ID_ChucVu = ChucVu.ID_ChucVu
            WHERE NguoiDung.Username = @username AND NguoiDung.Password = @password";
                    using (SqlCommand cmd = new SqlCommand(checkrole, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        object roleResult = cmd.ExecuteScalar();

                        string role = roleResult.ToString();
                        User.Username = username;
                        User.Role = role;
                        User.Password = password;

                        if (User.Role.ToLower() == "admin")
                        {
                            MessageBox.Show("Đăng nhập thành công với quyền Admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                            {
                                cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar) { Value = username });
                                cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = password });

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        User.ID_NhanVien = reader.GetInt32(0);
                                        User.Username = username;
                                        User.Role = reader.IsDBNull(1) ? "User" : reader.GetString(1); // Role (nếu NULL thì mặc định là "User")

                                        // Kiểm tra NULL trước khi đọc dữ liệu
                                        User.CCCD = reader.IsDBNull(2) ? "Không có CCCD" : reader.GetString(2);
                                        User.NgaySinh = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3);
                                        User.GioiTinh = reader.IsDBNull(4) ? "Không xác định" : reader.GetString(4);
                                        User.QueQuan = reader.IsDBNull(5) ? "Không có dữ liệu" : reader.GetString(5);
                                        User.SDT = reader.IsDBNull(6) ? "Không có số" : reader.GetString(6);
                                        User.DiaChi = reader.IsDBNull(7) ? "Không có địa chỉ" : reader.GetString(7);
                                        User.Email = reader.IsDBNull(8) ? "Không có email" : reader.GetString(8);
                                        User.ChucVu = reader.IsDBNull(9) ? "Không có chức vụ" : reader.GetString(9);
                                        User.PhongBan = reader.IsDBNull(10) ? "Không có phòng ban" : reader.GetString(10);
                                        User.HoTen = reader.GetString(11);

                                        User.Password = password;

                                        this.DialogResult = DialogResult.OK;
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Đăng nhập thất bại! Sai tài khoản hoặc mật khẩu.");
                                    }
                                }
                            }
                        }
                    }

                  

                   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }
    }
}
