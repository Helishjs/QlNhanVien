using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNhanSu;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace QuanLyNhanSu
{
    public partial class DanhSachNhanVien : UserControl
    {
        string sqlstring = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        int _Row, _Col;

        public event EventHandler<int> LayMaNhanVien;
        public event EventHandler DataChangedView;
        public event EventHandler HienThiQLNV;

        public DataTable _dt;
        public void SetDataTable1(DataTable dt)
        {
            _dt = dt;
            CapNhat(_dt);
        }
        public void CapNhat(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow row in _dt.Rows)
            {
                dataGridView1.Rows.Add(row["NhanVien.HoTen"], row["Số Điện Thoại"], row["Giới Tính"], row["Phòng Ban"], row["Chức Vụ"]);
            }
        }
        public void TaoXoaTaiKhoan_DataChanged(object sender, EventArgs e)
        {
            CapNhat(_dt);
            dataGridView1.Refresh();
        }
        public void QuanLyNhanSu_DataChanged(object sender, EventArgs e)
        {
            CapNhat(_dt);
            dataGridView1.Refresh();
        }
        public void QuanLyNhanTu_XoaData(object sender, EventArgs e)
        {
            _dt.Rows.RemoveAt(_Row);
            CapNhat(_dt);
            dataGridView1.Refresh();
        }
        public void ListenToDataChanged(TaoXoaTaiKhoan taoxoataikhoan)
        {
            taoxoataikhoan.DataChanged += TaoXoaTaiKhoan_DataChanged;
        }
        public void ListenToDataFix(QuanLyNhanSu1 quanlynhansu)
        {
            quanlynhansu.DataChangedFix += QuanLyNhanSu_DataChanged;
        }
        public void ListenDataDie(QuanLyNhanSu1 quanlynhansu)
        {
            quanlynhansu.DataDelete += QuanLyNhanTu_XoaData;
        }
        public DanhSachNhanVien()
        {
            InitializeComponent();

            dataGridView1.CellMouseDown += DinhViTheoChuot;

            OThayDoi.Visible = false;
        }

        private void DinhViTheoChuot(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;

                _Row = e.RowIndex;
                _Col = e.ColumnIndex;

                ChuotPhai.Show(dataGridView1, dataGridView1.PointToClient(Cursor.Position));
            }
        }

        private void chỉnhSửaÔToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[_Row].Cells[_Col].Value != null)
                OThayDoi.Visible = true;
            else
                MessageBox.Show("Chưa có thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Stoppp_Click(object sender, EventArgs e)
        {
            OThayDoi.Visible = false;
        }

        private void xemThôngTinChiTiếtNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HienThiQLNV?.Invoke(this, EventArgs.Empty);
            int MaDaLay = _Row;
            LayMaNhanVien?.Invoke(this, MaDaLay);
        }

        private void Okkk_Click(object sender, EventArgs e)
        {
            OThayDoi.Visible = false;
            _dt.Rows[_Row][dataGridView1.Columns[_Col].HeaderText] = ChinhSua.Text;
            CapNhat(_dt);
            ChinhSua.Text = "";
            On_DataChanged();
        }

        private void DanhSachNhanVien_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlconnect = new SqlConnection(sqlstring))
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
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Gán dữ liệu vào DataGridView
                dataGridView1.DataSource = dt;  
            }
        }

        protected virtual void On_DataChanged()
        {
            DataChangedView?.Invoke(this, EventArgs.Empty);
        }
    }
}
