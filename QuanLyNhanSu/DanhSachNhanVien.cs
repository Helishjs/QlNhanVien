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
            //dataGridView1.Rows.Clear();
            //foreach (DataRow row in _dt.Rows)
            //{
            //    dataGridView1.Rows.Add(row["NhanVien.HoTen"], row["Số Điện Thoại"], row["Giới Tính"], row["Phòng Ban"], row["Chức Vụ"]);
            //}
        }
        public void TaoXoaTaiKhoan_DataChanged(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            nv.LoadNhanVien(dataGridView1);
        }
        public void QuanLyNhanSu_DataChanged(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            nv.LoadNhanVien(dataGridView1);
        }
        public void QuanLyNhanTu_XoaData(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            nv.LoadNhanVien(dataGridView1);
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

                _Row = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID_NhanVien"].Value);
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
            int MaDaLay =_Row ;
            LayMaNhanVien?.Invoke(this, MaDaLay);
            HienThiQLNV?.Invoke(this, EventArgs.Empty);
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
            GetNhanVien nv = new GetNhanVien();
            nv.LoadNhanVien(dataGridView1);
        }

        protected virtual void On_DataChanged()
        {
            DataChangedView?.Invoke(this, EventArgs.Empty);
        }
    }
}
