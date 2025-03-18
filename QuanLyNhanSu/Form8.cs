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
    public partial class Form8 : Form
    {
        string sqlstring = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";


        int NhanVienDuocChon = -1;
        public DataTable dt;

        ManHinhChao MHC = new ManHinhChao();
        DanhSachNhanVien DanhSach = new DanhSachNhanVien();
        QuanLyNhanSu1 QuanLynhansu = new QuanLyNhanSu1();
        TaoXoaTaiKhoan taoXoaTaikhoan = new TaoXoaTaiKhoan();

        public Form8()
        {
            InitializeComponent();


            dt = new DataTable();
            dt.Columns.Add("Họ Tên", typeof(string));
            dt.Columns.Add("Ngày Sinh", typeof(DateTime));
            dt.Columns.Add("Giới Tính", typeof(string));
            dt.Columns.Add("Quê Quán", typeof(string));
            dt.Columns.Add("CCCD", typeof(string));
            dt.Columns.Add("Số Điện Thoại", typeof(string));
            dt.Columns.Add("Địa Chỉ", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Chức Vụ", typeof(string));
            dt.Columns.Add("Phòng Ban", typeof(string));
            dt.Columns.Add("Bảo Hiểm", typeof(string));
            dt.Columns.Add("Trợ Cấp", typeof(string));

            panel1.Controls.Add(MHC);
            panel1.Controls.Add(DanhSach);
            panel1.Controls.Add(QuanLynhansu);
            panel1.Controls.Add(taoXoaTaikhoan);
            MHC.Dock = DockStyle.Fill;
            DanhSach.Dock = DockStyle.Fill;
            QuanLynhansu.Dock = DockStyle.Fill;
            taoXoaTaikhoan.Dock = DockStyle.Fill;

            QuanLynhansu.SetDataTable(dt);
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

        private void DSNV_Click(object sender, EventArgs e)
        {
            DanhSach.BringToFront();
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
                QuanLynhansu.BringToFront();
        }

        private void TKNV_Click(object sender, EventArgs e)
        {
            taoXoaTaikhoan.BringToFront();
        }
        private void User_NhanVienDuocChon(object sender, int newValue)
        {
            NhanVienDuocChon = newValue;
            QuanLynhansu.QuanLyMa(NhanVienDuocChon);
            NhanVienDuocChon = -1;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
          
        }
    }
}
