using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FormAdminHomePage: Form
    {
        int NhanVienDuocChon = -1;
        public DataTable dt;

        ManHinhChao MHC = new ManHinhChao();
        DanhSachNhanVien DanhSach = new DanhSachNhanVien();
        QuanLyNhanSu1 QuanLynhansu = new QuanLyNhanSu1();
        TaoXoaTaiKhoan taoXoaTaikhoan = new TaoXoaTaiKhoan();

        public FormAdminHomePage()
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

            panel3.Controls.Add(MHC);
            panel3.Controls.Add(DanhSach);
            panel3.Controls.Add(QuanLynhansu);
            panel3.Controls.Add(taoXoaTaikhoan);
            MHC.Dock = DockStyle.Fill;
            DanhSach.Dock = DockStyle.Fill;
            QuanLynhansu.Dock = DockStyle.Fill;
            taoXoaTaikhoan.Dock = DockStyle.Fill;

            //QuanLynhansu.SetDataTable(dt);
            taoXoaTaikhoan.SetDataTable(dt);
            DanhSach.SetDataTable1(dt);

            DanhSach.ListenToDataChanged(taoXoaTaikhoan);
            DanhSach.ListenToDataFix(QuanLynhansu);
            QuanLynhansu.ListenToDataView(DanhSach);
            DanhSach.ListenDataDie(QuanLynhansu);

            DanhSach.LayMaNhanVien += User_NhanVienDuocChon;
            DanhSach.HienThiQLNV += QLNS_Click;
            QuanLynhansu.HienThiDanhSach += DSNV_Click;

        }
        private Form currentchildForm;
        private void OpenChildForm(Form children)
        {
            if (currentchildForm != null)
            {
                currentchildForm.Close();               
            }
            currentchildForm = children;
            children.TopLevel = false;
            children.FormBorderStyle = FormBorderStyle.None;
            children.Dock = DockStyle.Fill;
            panel3.Controls.Add(children);
            panel3.Tag = children;
            children.BringToFront();
            children.Show();
        }

        private void bangluong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormXemBangLuong());
            label1.Text = "BẢNG LƯƠNG";
        }

        private void loi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormPhamLoi());
            label1.Text = "TRA CỨU LỖI";
        }

        private void khen_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormKhenThuong());
            label1.Text = "TRA CỨU THƯỞNG";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            nv.LogOut(this);
        }
        private void QLNS_Click(object sender, EventArgs e)
        {
            if (NhanVienDuocChon == -1)
            {
                DialogResult result = MessageBox.Show(
               "Bạn chưa chọn tài khoản nhân viên",
               "Thông báo",
               MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    DanhSach.BringToFront();
                }
            }
            else
            {
                QuanLynhansu.BringToFront();
                NhanVienDuocChon = -1;
            }
            label1.Text = "THÔNG TIN CHI TIẾT";
        }

        private void DSNV_Click(object sender, EventArgs e)
        {
            DanhSach.BringToFront();
            label1.Text = "DANH SÁCH NHÂN VIÊN";
        }
        private void TKNV_Click(object sender, EventArgs e)
        {
            taoXoaTaikhoan.BringToFront();
            label1.Text = "ĐĂNG KÍ THÔNG TIN NHÂN VIÊN";

        }
        private void User_NhanVienDuocChon(object sender, int newValue)
        {
            NhanVienDuocChon = newValue;
            QuanLynhansu.QuanLyMa(NhanVienDuocChon);
            QuanLynhansu.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormDangKyLam());
            label1.Text = "BẢNG ĐĂNG KÍ LÀM THÊM";
        }
    }
}
