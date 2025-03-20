using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Data.SqlClient;

namespace QuanLyNhanSu
{
    public partial class QuanLyNhanSu1 : UserControl
    {

        public event EventHandler DataChangedFix;
        public event EventHandler DataDelete;
        public event EventHandler HienThiDanhSach;
        private DataTable _dt = new DataTable();
        private int manhanvien = -1;
        DataRow[] rows;
        string sqlstring = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        public QuanLyNhanSu1()
        {
            InitializeComponent();
            using(SqlConnection sqlconnect = new SqlConnection(sqlstring))
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
                adapter.Fill(_dt);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CapNhat.BringToFront();
            XoaTK.Visible = false;
            tableLayoutPanel2.Controls.Remove(ChucVuTB);
            tableLayoutPanel2.Controls.Add(ChucVuCB, 1, 0);
            tableLayoutPanel2.Controls.Remove(PhongBanTB);
            tableLayoutPanel2.Controls.Add(PhongBanCB, 1, 1);
            tableLayoutPanel2.Controls.Remove(BaoHiemTB);
            tableLayoutPanel2.Controls.Add(BaoHiemCB, 1, 2);
            tableLayoutPanel2.Controls.Remove(TroCapTB);
            tableLayoutPanel2.Controls.Add(TroCapCB, 1, 3);
            tableLayoutPanel1.Controls.Remove(GioiTinhTB);
            tableLayoutPanel1.Controls.Add(GioiTinhCB, 1, 2);
            tableLayoutPanel1.Controls.Remove(NgaySinhTB);
            tableLayoutPanel1.Controls.Add(NgaySinhChoose, 1, 1);
        }
        /*public void SetDataTable(DataTable dt)
        {
            _dt = dt;
        }*/

        public void CapNhatQuanLy(DataTable dt)
        {
            if (manhanvien == -1) return;
            row["HoTen"] = HoTenTB.Text;
            row["NgaySinh"] = NgaySinhChoose.Value;
            row["GioiTinh"] = GioiTinhCB.SelectedItem.ToString();
            row["QueQuan"] = QueQuanTB.Text;
            row["SoCCCD"] = CCCDTB.Text;
            row["SDT"] = SDTTB.Text;
            row["DiaChi"] = DiaChiTB.Text;
            row["Email"] = EmailTB.Text;
            row["Ten_ChucVu"] = ChucVuCB.SelectedItem.ToString();
            row["Ten_PhongBan"] = PhongBanCB.SelectedItem.ToString();
            row["BaoHiem"] = BaoHiemCB.SelectedItem.ToString();
            row["TroCap"] = TroCapCB.SelectedItem.ToString();
        }
        public void CapNhat_Click(object sender, EventArgs e)
        {
            button1.BringToFront();

            if (manhanvien == -1) return;

            CapNhatQuanLy(_dt);

            HoTenTB.ReadOnly = true;
            QueQuanTB.ReadOnly = true;
            CCCDTB.ReadOnly = true;
            SDTTB.ReadOnly = true;
            DiaChiTB.ReadOnly = true;
            EmailTB.ReadOnly = true;

            XoaTK.Visible = true;
            tableLayoutPanel2.Controls.Remove(ChucVuCB);
            tableLayoutPanel2.Controls.Add(ChucVuTB, 1, 0);
            tableLayoutPanel2.Controls.Remove(PhongBanCB);
            tableLayoutPanel2.Controls.Add(PhongBanTB, 1, 1);
            tableLayoutPanel2.Controls.Remove(BaoHiemCB);
            tableLayoutPanel2.Controls.Add(BaoHiemTB, 1, 2);
            tableLayoutPanel2.Controls.Remove(TroCapCB);
            tableLayoutPanel2.Controls.Add(TroCapTB, 1, 3);
            tableLayoutPanel1.Controls.Remove(GioiTinhCB);
            tableLayoutPanel1.Controls.Add(GioiTinhTB, 1, 2);
            tableLayoutPanel1.Controls.Remove(NgaySinhChoose);
            tableLayoutPanel2.Controls.Add(NgaySinhTB, 1, 1);

            HienThiThongTinCaNhan();

            OnDataChanged();
        }
        private void XoaTK_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Tài khoản đã được xóa");
                XoaData();
                HienThiDanhSach?.Invoke(this, EventArgs.Empty);
                HoTenTB.Text = "";
                NgaySinhTB.Text = "";
                QueQuanTB.Text = "";
                CCCDTB.Text = "";
                SDTTB.Text = "";
                DiaChiTB.Text = "";
                EmailTB.Text = "";
                ChucVuTB.Text = "";
                PhongBanTB.Text = "";
                BaoHiemTB.Text = "";
                TroCapTB.Text = "";
                manhanvien = -1;
            }
        }
        public void QuanLyMa(int _manhanvien)
        {
            manhanvien = _manhanvien;
            HienThiThongTinCaNhan();
        }
        DataRow row;
        public void HienThiThongTinCaNhan()
        {

            DataRow[] rows = _dt.Select($"ID_NhanVien = {manhanvien}");
            //MessageBox.Show("ID = " + manhanvien);

            if (rows.Length > 0)
            {
                row = rows[0]; // Lấy dòng đầu tiên nếu tìm thấy
                HoTenTB.ReadOnly = true;
                QueQuanTB.ReadOnly = true;
                CCCDTB.ReadOnly = true;
                SDTTB.ReadOnly = true;
                DiaChiTB.ReadOnly = true;
                EmailTB.ReadOnly = true;

                HoTenTB.Text = row["HoTen"].ToString();
                NgaySinhTB.Text = row["NgaySinh"] != DBNull.Value ? ((DateTime)row["NgaySinh"]).ToShortDateString() : "";
                GioiTinhTB.Text = row["GioiTinh"].ToString();
                QueQuanTB.Text = row["QueQuan"].ToString();
                CCCDTB.Text = row["SoCCCD"].ToString();
                SDTTB.Text = row["SDT"].ToString();
                DiaChiTB.Text = row["DiaChi"].ToString();
                EmailTB.Text = row["Email"].ToString();
                ChucVuTB.Text = row["Ten_ChucVu"].ToString();
                PhongBanTB.Text = row["Ten_PhongBan"].ToString();

                if (_dt.Columns.Contains("BaoHiem"))
                    BaoHiemTB.Text = row["BaoHiem"].ToString();
                if (_dt.Columns.Contains("TroCap"))
                    TroCapTB.Text = row["TroCap"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên với ID: " + manhanvien, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        protected virtual void OnDataChanged()
        {
            DataChangedFix?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void XoaData()
        {
            DataDelete?.Invoke(this, EventArgs.Empty);
        }
        public void ListenToDataView(DanhSachNhanVien danhsachnhanvien)
        {
            danhsachnhanvien.DataChangedView += DanhSachNhanVien_DataChanged;
        }
        public void DanhSachNhanVien_DataChanged(object sender, EventArgs e)
        {
            HienThiThongTinCaNhan();
        }

        /*internal class DataSet1 : global::QuanLyNhanSu.DataSet1
        {
        }*/
    }
}
