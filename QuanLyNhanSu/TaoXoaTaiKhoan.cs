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
                int baoHiem = BaoHiemCB.SelectedValue != null ? Convert.ToInt32(BaoHiemCB.SelectedValue) : 0;
                int troCap = TroCapCB.SelectedValue != null ? Convert.ToInt32(TroCapCB.SelectedValue) : 0;
                string soCCCD = CCCDTB.Text.Trim();

                // Chuyển họ tên thành username không dấu
                string username = Chuyenkhongdau.Convert(hoTen);
                string query_nhanvien = @"INSERT INTO NhanVien (Username, HoTen, Email, QueQuan, GioiTinh, NgaySinh, SDT, DiaChi, ID_PhongBan, ID_ChucVu, SoCCCD,ID_BaoHiem,ID_TroCap) 
                                  VALUES (@Username, @HoTen, @Email, @QueQuan, @GioiTinh, @NgaySinh, @SoDT, @DiaChi, @PhongBanID, @ChucVuID, @SoCCCD,@BaoHiemID,@TroCapID)";
                string query_luong = @"INSERT INTO Luong (ID_NhanVien,ThangNam,ID_LuongChucVu,ID_TroCap) VALUES (@ID_NhanVien,@ThangNam,@ID_LuongChucVu,@ID_TroCap)";
                string query_account = @"INSERT INTO NguoiDung (Username, Password, Role) VALUES (@Username, @Password, @Role)";

                using (SqlConnection sqlConnect = new SqlConnection(sqlString))
                {
                    sqlConnect.Open();
                    using (SqlTransaction transaction = sqlConnect.BeginTransaction()) // Sửa chỗ này
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(query_account, sqlConnect, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Username", username);
                                cmd.Parameters.AddWithValue("@Password", soCCCD);
                                cmd.Parameters.AddWithValue("@Role", Role);
                                cmd.ExecuteNonQuery();
                            }

                            int idNhanVien;
                            using (SqlCommand cmd = new SqlCommand(query_nhanvien + "; SELECT SCOPE_IDENTITY();", sqlConnect, transaction))
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
                                cmd.Parameters.AddWithValue("@BaoHiemID", baoHiem);
                                cmd.Parameters.AddWithValue("@TroCapID", troCap);

                                object result = cmd.ExecuteScalar();
                                if (result == null)
                                {
                                    MessageBox.Show("Lỗi khi lấy ID nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                idNhanVien = Convert.ToInt32(result);
                            }

                            using (SqlCommand cmd = new SqlCommand(query_luong, sqlConnect, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ID_NhanVien", idNhanVien);
                                cmd.Parameters.AddWithValue("@ThangNam", DateTime.Now);
                                cmd.Parameters.AddWithValue("@ID_LuongChucVu", chucVu);
                                cmd.Parameters.AddWithValue("@ID_TroCap", troCap);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            GetNhanVien nv = new GetNhanVien();

                            DataGridView dgv = DanhSachNhanVien.DanhSachnv;
                            nv.LoadNhanVien(dgv);

                            MessageBox.Show("Thêm nhân viên & tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void LoadComboBox(string value, ComboBox cb)
        {
            string query = $"SELECT ID_{value}, Ten_{value} FROM {value}";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlString))
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
                using (SqlConnection sqlConnection = new SqlConnection(sqlString))
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
            LoadComboBox_2("BaoHiem",BaoHiemCB);
            LoadComboBox_2("TroCap",TroCapCB);
            LoadComboBox("PhongBan", PhongBanCB);
            LoadComboBox("ChucVu", ChucVuCB);
        }
    }
}
