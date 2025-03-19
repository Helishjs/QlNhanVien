using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
        string sqlString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";

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
            try
            {
                string hoTen = HoTenTB.Text.Trim();
                string email = EmailTB.Text.Trim();
                string queQuan = QueQuanTB.Text.Trim();
                string gioiTinh = GioiTinhCB.Text.Trim();
                DateTime ngaySinh = NgaySinhDT.Value;
                string Role = "user";
                int soDT;
                if (!int.TryParse(SDTTB.Text, out soDT))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string diaChi = DiaChiTB.Text.Trim();
                int phongBan = PhongBanCB.SelectedValue != null ? Convert.ToInt32(PhongBanCB.SelectedValue) : 0;
                int chucVu = ChucVuCB.SelectedValue != null ? Convert.ToInt32(ChucVuCB.SelectedValue) : 0;
                string soCCCD = CCCDTB.Text.Trim();

                // Chuyển họ tên thành username không dấu
                string username = Chuyenkhongdau.Convert(hoTen);
                string query_nhanvien = @"INSERT INTO NhanVien (Username, HoTen, Email, QueQuan, GioiTinh, NgaySinh, SDT, DiaChi, ID_PhongBan, ID_ChucVu, SoCCCD) 
                                  VALUES (@Username, @HoTen, @Email, @QueQuan, @GioiTinh, @NgaySinh, @SoDT, @DiaChi, @PhongBanID, @ChucVuID, @SoCCCD)";

                string query_account = @"INSERT INTO NguoiDung (Username, Password, Role) VALUES (@Username, @Password, @Role)";

                using (SqlConnection sqlConnect = new SqlConnection(sqlString))
                {
                    sqlConnect.Open();
                    using (SqlTransaction transaction = Connection.GetConnection().BeginTransaction())
                    {
                        try
                        {

                            using (SqlCommand cmd = new SqlCommand(query_account, Connection.GetConnection(), transaction))
                            {
                                cmd.Parameters.AddWithValue("@Username", username);
                                cmd.Parameters.AddWithValue("@Password", soCCCD);
                                cmd.Parameters.AddWithValue("@Role", Role);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand(query_nhanvien, Connection.GetConnection(), transaction))
                            {
                                cmd.Parameters.AddWithValue("@Username", username);
                                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                cmd.Parameters.AddWithValue("@Email", email);
                                cmd.Parameters.AddWithValue("@QueQuan", queQuan);
                                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                                cmd.Parameters.AddWithValue("@SoDT", soDT);
                                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                                cmd.Parameters.AddWithValue("@PhongBanID", phongBan);
                                cmd.Parameters.AddWithValue("@ChucVuID", chucVu);
                                cmd.Parameters.AddWithValue("@SoCCCD", soCCCD);

                                cmd.ExecuteNonQuery();
                            }


                            // Commit transaction nếu cả 2 INSERT thành công
                            transaction.Commit();

                            MessageBox.Show("Thêm nhân viên & tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            HoTenTB.Text = "";
                            EmailTB.Text = "";
                            QueQuanTB.Text = "";
                            GioiTinhCB.Text = "";
                            SDTTB.Text = "";
                            DiaChiTB.Text = "";
                            CCCDTB.Text = "";
                            PhongBanCB.SelectedIndex = -1;
                            ChucVuCB.SelectedIndex = -1;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi thêm nhân viên & tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        void LoadPhongBan()
        {
            string query = "SELECT ID_PhongBan, Ten_PhongBan FROM PhongBan";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlString))
                {
                    sqlConnection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        PhongBanCB.DataSource = dt;
                        PhongBanCB.DisplayMember = "Ten_PhongBan";
                        PhongBanCB.ValueMember = "ID_PhongBan";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phòng ban: " + ex.Message);
            }
        }

        void LoadChucVu()
        {
            string query = "SELECT ID_ChucVu, Ten_ChucVu FROM ChucVu";

            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(sqlString))
                {
                    sqlConnect.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnect))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        ChucVuCB.DataSource = dt;
                        ChucVuCB.DisplayMember = "Ten_ChucVu";
                        ChucVuCB.ValueMember = "ID_ChucVu";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chức vụ: " + ex.Message);
            }
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

        private void TaoXoaTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadChucVu();
            LoadPhongBan();
        }
    }
}
