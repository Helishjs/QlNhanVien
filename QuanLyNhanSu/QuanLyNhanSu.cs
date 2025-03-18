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

namespace QuanLyNhanSu
{
    public partial class QuanLyNhanSu1 : UserControl
    {

        public event EventHandler DataChangedFix;
        public event EventHandler DataDelete;
        public event EventHandler HienThiDanhSach;
        private DataTable _dt;
        private int manhanvien = -1;

        public QuanLyNhanSu1()
        {
            InitializeComponent();
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
        public void SetDataTable(DataTable dt)
        {
            _dt = dt;
        }

        public void CapNhatQuanLy(DataTable dt)
        {
            if (manhanvien == -1) return;
            dt.Rows[manhanvien]["Họ Tên"] = HoTenTB.Text;
            dt.Rows[manhanvien]["Ngày Sinh"] = NgaySinhChoose.Value;
            dt.Rows[manhanvien]["Giới Tính"] = GioiTinhCB.SelectedItem.ToString();
            dt.Rows[manhanvien]["Quê Quán"] = QueQuanTB.Text;
            dt.Rows[manhanvien]["CCCD"] = CCCDTB.Text;
            dt.Rows[manhanvien]["Số Điện Thoại"] = SDTTB.Text;
            dt.Rows[manhanvien]["Địa Chỉ"] = DiaChiTB.Text;
            dt.Rows[manhanvien]["Email"] = EmailTB.Text;
            dt.Rows[manhanvien]["Chức Vụ"] = ChucVuCB.SelectedItem.ToString();
            dt.Rows[manhanvien]["Phòng Ban"] = PhongBanCB.SelectedItem.ToString();
            dt.Rows[manhanvien]["Bảo Hiểm"] = BaoHiemCB.SelectedItem.ToString();
            dt.Rows[manhanvien]["Trợ Cấp"] = TroCapCB.SelectedItem.ToString();
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

        public void HienThiThongTinCaNhan()
        {
            HoTenTB.ReadOnly = true;
            QueQuanTB.ReadOnly = true;
            CCCDTB.ReadOnly = true;
            SDTTB.ReadOnly = true;
            DiaChiTB.ReadOnly = true;
            EmailTB.ReadOnly = true;

            HoTenTB.Text = _dt.Rows[manhanvien]["Họ Tên"].ToString();
            NgaySinhTB.Text = ((DateTime)_dt.Rows[manhanvien]["Ngày Sinh"]).ToShortDateString();
            GioiTinhTB.Text = _dt.Rows[manhanvien]["Giới Tính"].ToString();
            QueQuanTB.Text = _dt.Rows[manhanvien]["Quê Quán"].ToString();
            CCCDTB.Text = _dt.Rows[manhanvien]["CCCD"].ToString();
            SDTTB.Text = _dt.Rows[manhanvien]["Số Điện Thoại"].ToString();
            DiaChiTB.Text = _dt.Rows[manhanvien]["Địa Chỉ"].ToString();
            EmailTB.Text = _dt.Rows[manhanvien]["Email"].ToString();
            ChucVuTB.Text = _dt.Rows[manhanvien]["Chức Vụ"].ToString();
            PhongBanTB.Text = _dt.Rows[manhanvien]["Phòng Ban"].ToString();
            BaoHiemTB.Text = _dt.Rows[manhanvien]["Bảo Hiểm"].ToString();
            TroCapTB.Text = _dt.Rows[manhanvien]["Trợ Cấp"].ToString();

            GioiTinhCB.Text = GioiTinhTB.Text;
            ChucVuCB.Text = ChucVuTB.Text;
            PhongBanCB.Text = PhongBanTB.Text;
            BaoHiemCB.Text = BaoHiemTB.Text;
            TroCapCB.Text = TroCapTB.Text;
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
