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
using static System.Collections.Specialized.BitVector32;

namespace QuanLyNhanSu
{
    public partial class MainForm: Form
    {
        string sqlString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";

        public MainForm()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(User.Username))
            {
                MessageBox.Show("Bạn cần đăng nhập để sử dụng ứng dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FormLogin loginForm = new FormLogin();
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void btnNhanvienthaisan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuanlinhanvien_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {   
            //tao loi dan ham dang nhap
            this.Hide();
            using(FormLogin login = new FormLogin())
            {
                if(login.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
            }
            this.Show();

        }

        private void btnDoithongtin_Click(object sender, EventArgs e)
        {
            //gọi form đổi mật khẩu
            this.Hide();
            using (FormChangePassword changePassword = new FormChangePassword())
            {
                if (changePassword.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
            }
            this.Show();

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            nv.LogOut();

            this.Hide();
            FormLogin login = new FormLogin();
            login.ShowDialog();
            this.Close();
        }

        private void btnQuanlinhanvien_Click_1(object sender, EventArgs e)
        {
            ShowPanel(panelQLNhanVien);
        }
        private void ShowPanel(Panel panel)
        {
            // Ẩn tất cả panel con trong panelContainer
            foreach (Control ctrl in panelContainer.Controls)
            {
                if (ctrl is Panel)
                    ctrl.Visible = false;
            }

            // Hiển thị panel được chọn
            panel.Visible = true;
            panel.BringToFront();
        }

        private void btnLichsuhoatdong_Click(object sender, EventArgs e)
        {
            ShowPanel(panelHistory);
        }

        private void panelQLNhanVien_VisibleChanged(object sender, EventArgs e)
        {
            if (panelQLNhanVien.Visible)
            {
                GetNhanVien nhanVien = new GetNhanVien();
                nhanVien.LoadNhanVien(dataGridViewNhanVien);
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FormNhanvien NV = new FormNhanvien())
            {
                if (NV.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
            }
            this.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewNhanVien.SelectedRows[0];

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

                this.Hide();

                using (FormSuaNhanVien suaNhanVien = new FormSuaNhanVien(id, hoTen, ngaySinh, gioiTinh,
                                                                         queQuan, email, sdt, soCCCD, diaChi, phongBan, chucVu))
                {
                    suaNhanVien.ShowDialog();
                }
                this.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(dataGridViewNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewNhanVien.SelectedRows[0];
                string id = row.Cells["ID_NhanVien"].Value.ToString();

                try
                {
                    using (SqlConnection sqlconnect = new SqlConnection(sqlString))
                    {
                        sqlconnect.Open();
                        string query = "DELETE FROM NhanVien WHERE NhanVien.ID_NhanVien = @id";
                        using(SqlCommand cmd = new SqlCommand(query, sqlconnect))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int check = cmd.ExecuteNonQuery();
                            if(check > 0)
                            {
                              DialogResult result =  MessageBox.Show("Bạn có muốn xóa thông tin nhân viên này không? ","Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if(result == DialogResult.Yes)
                                {
                                    MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    GetNhanVien nv = new GetNhanVien();
                                    nv.LoadNhanVien(dataGridViewNhanVien);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Chọn nhân viên cần xóa");
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Có lỗi: " + ex);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string result = tbSearch.Text.Trim();
            GetNhanVien nv = new GetNhanVien();
            if (!string.IsNullOrEmpty(result))
            {
                nv.SearchNhanVien(result, dataGridViewNhanVien);
            }
            else
            {
                nv.LoadNhanVien(dataGridViewNhanVien);
            }
        }

        private void dataGridViewNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
