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



        private void btnLogin_Click(object sender, EventArgs e)
        {   
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
            this.Hide();
            using (FormChangePassword changePassword = new FormChangePassword())
            {
                changePassword.ShowDialog();
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
            GetNhanVien nv = new GetNhanVien();
            nv.SuaNhaVien(dataGridViewNhanVien);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            GetNhanVien nv = new GetNhanVien();
            nv.XoaNhanVien(dataGridViewNhanVien);
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

        private void btnBaoHiem_Click(object sender, EventArgs e)
        {
            this.Hide();
            using(Form15 form = new Form15())
            {
                form.ShowDialog();
            }
            this.Show();
        }

        private void btnHopdonglaodong_Click(object sender, EventArgs e)
        {
            this.Hide();
            using(Form8 form = new Form8())
            {
                form.ShowDialog();
            }
            this.Show();
        }

        private void btnTangCa_Click(object sender, EventArgs e)
        {
            this.Hide();
            using(FormAdminHomePage form = new FormAdminHomePage())
            {
                form.ShowDialog();
            }
            this.Show();
        }
    }
}
