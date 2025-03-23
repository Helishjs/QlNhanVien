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
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;

namespace QuanLyNhanSu
{
    public partial class QuanLyNhanSu1 : UserControl
    {

        public event EventHandler DataChangedFix;
        public event EventHandler DataDelete;
        public event EventHandler HienThiDanhSach;
        public event EventHandler LoadDanhSach;

        private DataTable _dt = new DataTable();
        private int manhanvien = -1;
        DataRow[] rows;
        string sqlstring = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        public QuanLyNhanSu1()
        {
            InitializeComponent();
            using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
            {
                sqlconnect.Open();
                string query = @"SELECT NhanVien.ID_NhanVien, NhanVien.HoTen, NhanVien.NgaySinh, 
                        NhanVien.GioiTinh, NhanVien.QueQuan, NhanVien.Email, 
                        NhanVien.SDT, NhanVien.SoCCCD, NhanVien.DiaChi, 
                        ChucVu.Ten_ChucVu, PhongBan.Ten_PhongBan,BaoHiem.LoaiBaoHiem,TroCap.LoaiTroCap
                 FROM NhanVien 
                 JOIN ChucVu ON ChucVu.ID_ChucVu = NhanVien.ID_ChucVu 
                 JOIN PhongBan ON PhongBan.ID_PhongBan = NhanVien.ID_PhongBan
                 JOIN TroCap ON TroCap.ID_TroCap = NhanVien.ID_TroCap
                 JOIN BaoHiem ON BaoHiem.ID_BaoHiem = NhanVien.ID_BaoHiem";
                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlconnect);
                adapter.Fill(_dt);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CapNhat.BringToFront();
            QueQuanTB.ReadOnly = false;
            CCCDTB.ReadOnly = false;
            SDTTB.ReadOnly = false;
            DiaChiTB.ReadOnly = false;
            EmailTB.ReadOnly = false;

            GioiTinhCB.SelectedItem = "Nam";

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
        public void CapNhat_Click(object sender, EventArgs e)
        {
            button1.BringToFront();

            if (manhanvien == -1) return;
            GetNhanVien nv = new GetNhanVien();
            string ID_nhanvien = manhanvien.ToString();
            nv.UpdateNhanVien(ID_nhanvien, HoTenTB.Text, NgaySinhChoose.Value, GioiTinhCB.SelectedItem.ToString(), QueQuanTB.Text, EmailTB.Text, SDTTB.Text, CCCDTB.Text, DiaChiTB.Text, PhongBanCB.SelectedValue.ToString(), ChucVuCB.SelectedValue.ToString());

            DataGridView dgv = DanhSachNhanVien.DanhSachnv;
            nv.LoadNhanVien(dgv);

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
            tableLayoutPanel1.Controls.Add(NgaySinhTB, 1, 1);

            HienThiThongTinCaNhan();

            //OnDataChanged();
            LoadDanhSach?.Invoke(this , EventArgs.Empty);
        }
        private void XoaTK_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string username = Chuyenkhongdau.Convert(HoTenTB.Text);
                GetNhanVien nv = new GetNhanVien();
                nv.XoaNhanVien(manhanvien,username);
                DataGridView dgv = DanhSachNhanVien.DanhSachnv;
                nv.LoadNhanVien(dgv);
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
            DataTable dt = new DataTable();
            LoadDuLieu(dt);

            DataRow[] rows = dt.Select($"ID_NhanVien = {manhanvien}");
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
                NgaySinhTB.Text = ((DateTime)row["NgaySinh"]).ToShortDateString();
                GioiTinhTB.Text = row["GioiTinh"].ToString();
                QueQuanTB.Text = row["QueQuan"].ToString();
                CCCDTB.Text = row["SoCCCD"].ToString();
                SDTTB.Text = row["SDT"].ToString();
                DiaChiTB.Text = row["DiaChi"].ToString();
                EmailTB.Text = row["Email"].ToString();
                ChucVuTB.Text = row["Ten_ChucVu"].ToString();
                PhongBanTB.Text = row["Ten_PhongBan"].ToString();

                if (_dt.Columns.Contains("LoaiBaoHiem"))
                    BaoHiemTB.Text = row["LoaiBaoHiem"].ToString();
                if (_dt.Columns.Contains("LoaiTroCap"))
                    TroCapTB.Text = row["LoaiTroCap"].ToString();
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
            //HienThiThongTinCaNhan();
        }

        private void QuanLyNhanSu1_Load(object sender, EventArgs e)
        {
            LoadComboBox("ChucVu", ChucVuCB);
            LoadComboBox("PhongBan", PhongBanCB);
            LoadComboBox_2("BaoHiem", BaoHiemCB);
            LoadComboBox_2("TroCap", TroCapCB);
        }

        /*internal class DataSet1 : global::QuanLyNhanSu.DataSet1
        {
        }*/
        public void LoadComboBox(string value, ComboBox cb)
        {
            string query = $"SELECT ID_{value}, Ten_{value} FROM {value}";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlstring))
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
                using (SqlConnection sqlConnection = new SqlConnection(sqlstring))
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
        public void LoadDuLieu(DataTable dt)
        {
            using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
            {
                sqlconnect.Open();
                string query = @"SELECT NhanVien.ID_NhanVien, NhanVien.HoTen, NhanVien.NgaySinh, 
                        NhanVien.GioiTinh, NhanVien.QueQuan, NhanVien.Email, 
                        NhanVien.SDT, NhanVien.SoCCCD, NhanVien.DiaChi, 
                        ChucVu.Ten_ChucVu, PhongBan.Ten_PhongBan,BaoHiem.LoaiBaoHiem,TroCap.LoaiTroCap
                 FROM NhanVien 
                 JOIN ChucVu ON ChucVu.ID_ChucVu = NhanVien.ID_ChucVu 
                 JOIN PhongBan ON PhongBan.ID_PhongBan = NhanVien.ID_PhongBan
                 JOIN TroCap ON TroCap.ID_TroCap = NhanVien.ID_TroCap
                 JOIN BaoHiem ON BaoHiem.ID_BaoHiem = NhanVien.ID_BaoHiem";
                using(SqlCommand cmd = new SqlCommand(query, sqlconnect))
                {
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                
            }
        }
    }

}
