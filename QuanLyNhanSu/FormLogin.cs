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

                    string query = @"
            SELECT NhanVien.ID_NhanVien, NguoiDung.Role,NhanVien.SoCCCD 
            FROM NguoiDung JOIN NhanVien ON NhanVien.Username = NguoiDung.Username
            WHERE NguoiDung.Username = @username AND NguoiDung.Password = @password";

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
                                User.Role = reader.GetString(1);
                                User.Password = password;
                                User.CCCD = reader.GetString(2);

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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }
    }
}
