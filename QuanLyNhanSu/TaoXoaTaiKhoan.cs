using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class TaoXoaTaiKhoan : UserControl
    {
        public event EventHandler DataChanged;
        public DataTable _dt;
        public void SetDataTable(DataTable dt)
        {
            _dt = dt;
        }

        public TaoXoaTaiKhoan()
        {
            InitializeComponent();
            TaoTK.Visible = false;

            HoTenTB.TextChanged += TextBox_TextChange;
            QueQuanTB.TextChanged += TextBox_TextChange;
            CCCDTB.TextChanged += TextBox_TextChange;
            SDTTB.TextChanged += TextBox_TextChange;
            DiaChiTB.TextChanged += TextBox_TextChange;
            EmailTB.TextChanged += TextBox_TextChange;

            GioiTinhCB.SelectedIndexChanged += TextBox_TextChange;
            ChucVuCB.SelectedIndexChanged += TextBox_TextChange;
            PhongBanCB.SelectedIndexChanged += TextBox_TextChange;
            BaoHiemCB.SelectedIndexChanged += TextBox_TextChange;
            TroCapCB.SelectedIndexChanged += TextBox_TextChange;
        }
        public void clear()
        {
            HoTenTB.Text = "";
            QueQuanTB.Text = "";
            CCCDTB.Text = "";
            SDTTB.Text = "";
            DiaChiTB.Text = "";
            EmailTB.Text = "";
            GioiTinhCB.Text = "";
            ChucVuCB.Text = "";
            PhongBanCB.Text = "";
            BaoHiemCB.Text = "";
            TroCapCB.Text = "";
            GioiTinhCB.SelectedIndex = -1;
            ChucVuCB.SelectedIndex = -1;
            PhongBanCB.SelectedIndex = -1;
            BaoHiemCB.SelectedIndex = -1;
            TroCapCB.SelectedIndex = -1;
        }
        private void TaoTK_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tài khoản đã được tạo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ThemGiaTri
            _dt.Rows.Add(HoTenTB.Text, dateTimePicker1.Value, GioiTinhCB.Text, QueQuanTB.Text, CCCDTB.Text, SDTTB.Text, DiaChiTB.Text, EmailTB.Text, ChucVuCB.Text, PhongBanCB.Text, BaoHiemCB.Text, TroCapCB.Text);

            clear();

            OnDataChanged();
        }

        private void TextBox_TextChange(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HoTenTB.Text) &&
                !string.IsNullOrEmpty(QueQuanTB.Text) &&
                !string.IsNullOrEmpty(CCCDTB.Text) &&
                !string.IsNullOrEmpty(SDTTB.Text) &&
                !string.IsNullOrEmpty(DiaChiTB.Text) &&
                !string.IsNullOrEmpty(EmailTB.Text) &&
                GioiTinhCB.SelectedIndex != -1 &&
                ChucVuCB.SelectedIndex != -1 &&
                PhongBanCB.SelectedIndex != -1 &&
                BaoHiemCB.SelectedIndex != -1 &&
                TroCapCB.SelectedIndex != -1
                )
                TaoTK.Visible = true;
            else
                TaoTK.Visible = false;
        }
        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
