using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    

    public partial class FormUser: Form
    {
        private string connectionString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        private int currentUserID;
        public FormUser(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            if (string.IsNullOrEmpty(User.Username))
            {
                MessageBox.Show("Bạn cần đăng nhập để sử dụng ứng dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FormLogin loginForm = new FormLogin();
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                }
            }

        }
        //Chuyển panel
        private void ShowPanel(Panel panel)
        {
            foreach (Control ctrl in panelcontrol.Controls)
            {
                if (ctrl is Panel)
                    ctrl.Visible = false;
            }

            panel.Visible = true;
            panel.BringToFront();
        }
        private void btnthongtin_Click(object sender, EventArgs e)
        {
            ShowPanel(panelthongtin);
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            ShowPanel(paneldangky);
        }

        private void btnketqua_Click(object sender, EventArgs e)
        {
            ShowPanel(panelketqua);
        }

        private void btnbangluong_Click(object sender, EventArgs e)
        {
            ShowPanel(panelbangluong);
            textBox1.Text = (User.ID_NhanVien).ToString();
            textBox2.Text = User.Username;
            LoadBangLuong();

        }

        private void LoadNhanVienData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Nhanvien,HoTen, NgaySinh, GioiTinh, QueQuan, SoCCCD, SDT, Email FROM NhanVien WHERE ID_NhanVien = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", currentUserID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read()) // Nếu có dữ liệu
                        {
                            txbmanhanvien.Text = reader["ID_NhanVien"].ToString();
                            txbhoten.Text = reader["HoTen"].ToString();
                            txbngaysinh.Text = Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");
                            txbgioitinh.Text = reader["GioiTinh"].ToString();
                            txbquequan.Text = reader["QueQuan"].ToString();
                            txbcccd.Text = reader["SoCCCD"].ToString();
                            txbsodienthoai.Text = reader["SDT"].ToString();
                            txbemail.Text = reader["Email"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin nhân viên.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void Form12_Load(object sender, EventArgs e)
        {
            //dataGridView1.CellValueChanged += dataGridView1_CellContentClick;
            ShowPanel(panelthongtin);
            LoadNhanVienData();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadDanhSachThang()
        {
            cbchonthang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cbchonthang.Items.Add(i);
            }
            cbchonthang.SelectedIndex = DateTime.Now.Month - 1; // Mặc định chọn tháng hiện tại
        }
        private void cbchonthang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dataGridView1.Columns.Clear(); // Xóa hết các cột cũ

            // Thêm cột đầu tiên là "Loại"
            dataGridView1.Columns.Add("Loai", "Loại");

            // Lấy tháng hiện tại từ ComboBox
            int selectedMonth = Convert.ToInt32(cbchonthang.SelectedItem);
            // Xóa dữ liệu cũ của nhân viên trong tháng đã chọn
            string deleteQuery = "DELETE FROM DangKyLam WHERE ID_NhanVien = @ID AND MONTH(NgayDangKy) <> @Month";
            using (SqlConnection conn1 = new SqlConnection(connectionString))
            {
                conn1.Open();
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn1))
                {
                    cmd.Parameters.AddWithValue("@ID", currentUserID);
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);
                    cmd.ExecuteNonQuery();
                }
            }
                // Lấy năm hiện tại
                int selectedYear = DateTime.Now.Year;

            // Tạo danh sách các ngày thứ 7 và chủ nhật trong tháng
            List<int> ngayThuBay = new List<int>();
            List<int> ngayChuNhat = new List<int>();

            DateTime firstDayOfMonth = new DateTime(selectedYear, selectedMonth, 1);
            int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(selectedYear, selectedMonth, day);
                if (currentDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    ngayThuBay.Add(day);
                }
                else if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    ngayChuNhat.Add(day);
                }
            }

            // Thêm các cột ngày vào DataGridView
            foreach (var ngay in ngayThuBay.Concat(ngayChuNhat))
            {
                DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
                chkColumn.Name = "Ngay_" + ngay;
                chkColumn.HeaderText = ngay + "/" + selectedMonth;
                dataGridView1.Columns.Add(chkColumn);
            }

            // Thêm hàng thứ 7 và chủ nhật
            dataGridView1.Rows.Add("Chọn ca làm");


            // Chuyển các ô trong bảng thành checkbox
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Index != 0) // Không thay đổi cột đầu tiên "Loại"
                {
                    col.CellTemplate = new DataGridViewCheckBoxCell();
                }
            }
            // Truy vấn SQL để lấy danh sách ngày đã đăng ký
            string query = "SELECT NgayDangKy FROM DangKyLam WHERE ID_NhanVien = @ID AND MONTH(NgayDangKy) = @Month AND YEAR(NgayDangKy) = @Year";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", currentUserID);
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);
                    cmd.Parameters.AddWithValue("@Year", selectedYear);

                    SqlDataReader reader = cmd.ExecuteReader();
                    HashSet<int> ngayDaDangKy = new HashSet<int>();

                    while (reader.Read())
                    {
                        DateTime ngayDangKy = Convert.ToDateTime(reader["NgayDangKy"]);
                        ngayDaDangKy.Add(ngayDangKy.Day);
                    }

                    // Đánh dấu các ngày đã đăng ký và vô hiệu hóa checkbox
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (col.Index == 0) continue;

                        int ngay;
                        if (int.TryParse(col.HeaderText.Split('/')[0], out ngay) && ngayDaDangKy.Contains(ngay))
                        {
                            dataGridView1.Rows[0].Cells[col.Index].Value = true; // Đánh dấu checkbox đã đăng ký
                            dataGridView1.Rows[0].Cells[col.Index].ReadOnly = true; // Khóa ô checkbox
                        }
                    }
                }
            }
        }
        private void paneldangky_Paint(object sender, PaintEventArgs e)
        {
            LoadDanhSachThang();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnxacnhan_Click(object sender, EventArgs e)
        {
            int currentUserID = Convert.ToInt32(txbmanhanvien.Text); // Lấy ID nhân viên hiện tại
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int selectedMonth = Convert.ToInt32(cbchonthang.SelectedItem);
                int selectedYear = DateTime.Now.Year;
                string deleteQuery = "DELETE FROM DangKyLam WHERE ID_NhanVien = @ID AND MONTH(NgayDangKy) = @Month AND YEAR(NgayDangKy) = @Year";
                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@ID", currentUserID);
                    deleteCmd.Parameters.AddWithValue("@Month", selectedMonth);
                    deleteCmd.Parameters.AddWithValue("@Year", selectedYear);
                    deleteCmd.ExecuteNonQuery();
                }

                // Lặp qua từng cột trong DataGridView (bỏ qua cột "Loại")
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Index == 0) continue; // Bỏ qua cột đầu tiên chứa "Loại"

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[col.Index] is DataGridViewCheckBoxCell checkBoxCell &&
                            checkBoxCell.Value != null && (bool)checkBoxCell.Value == true)
                        {
                            string ngayThang = col.HeaderText; // Tiêu đề cột (ví dụ: "1/3")
                            DateTime ngayLam;

                            // Chuyển đổi tiêu đề cột thành DateTime
                            if (DateTime.TryParseExact(ngayThang + "/" + DateTime.Now.Year,
                                                      "d/M/yyyy",
                                                      CultureInfo.InvariantCulture,
                                                      DateTimeStyles.None,
                                                      out ngayLam))
                            {
                                // Chèn dữ liệu vào bảng DangKyLam2
                                string query = "INSERT INTO DangKyLam (ID_NhanVien, NgayDangKy) VALUES (@ID, @NgayDangKy)";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@ID", currentUserID);
                                    cmd.Parameters.AddWithValue("@NgayDangKy", ngayLam);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lỗi định dạng ngày: " + ngayThang, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    string truyvan = @"
        UPDATE Luong
        SET TongLuong = Luong_ChucVu.LuongCoBan * DATEDIFF(DAY, Luong.ThangNam, GETDATE()) 
                     + Luong.TongSoNgayLamThem * 600000
                     + COALESCE((SELECT SUM(Thuong.SoTienThuong) 
                                 FROM Thuong 
                                 WHERE Thuong.ID_NhanVien = Luong.ID_NhanVien), 0)
                     + COALESCE(TroCap.SoTien, 0)
        FROM Luong
        JOIN Luong_ChucVu ON Luong.ID_LuongChucVu = Luong_ChucVu.ID_LuongChucVu
        LEFT JOIN TroCap ON Luong.ID_TroCap = TroCap.ID_TroCap;
    ";

                    using (SqlCommand cmd = new SqlCommand(truyvan, connect))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Cập nhật thành công {rowsAffected} dòng.");
                    }
                }
            }
            MessageBox.Show("Đăng ký ca làm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataGridView();
            LoadDangKyLamThem(currentUserID);
            
        }
        private void LoadDangKyLamThem(int idNhanVien)
        {
           
            string query = "SELECT * FROM DangKyLam WHERE ID_NhanVien = @ID_NhanVien ORDER BY NgayDangKy DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_NhanVien", idNhanVien);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView2.DataSource = dt;

                    // Xóa cột trước khi gán vào DataGridView
                    //dt.Columns.Remove("TongSoNgayLamThem");
                    //dt.Columns.Remove("DaXacNhan");

                    dataGridView2.DataSource = dt;
                    HienThiTongSoNgayDangKy();
                }
            }
        }
        
        private void LoadNhanVienData2()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Nhanvien,HoTen FROM NhanVien WHERE ID_NhanVien = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", currentUserID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read()) // Nếu có dữ liệu
                        {
                            txbmanhanvien2.Text = reader["ID_NhanVien"].ToString();
                            txbhoten2.Text = reader["HoTen"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin nhân viên.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        //tổng
        private void HienThiTongSoNgayDangKy()
        {
            int selectedMonth = Convert.ToInt32(cbchonthang.SelectedItem);
            int selectedYear = DateTime.Now.Year;
            int tongSoNgay = 0;

            string query = "SELECT COUNT(*) AS TongSoNgay FROM DangKyLam " +
                           "WHERE ID_NhanVien = @ID AND MONTH(NgayDangKy) = @Month AND YEAR(NgayDangKy) = @Year";
            string query_1 = "Update Luong set TongSoNgayLamThem = @result Where ID_NhanVien = @ID "; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", currentUserID);
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);
                    cmd.Parameters.AddWithValue("@Year", selectedYear);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tongSoNgay = Convert.ToInt32(result);
                        using(SqlCommand cmd_1 =  new SqlCommand(query_1,conn))
                        {
                            cmd_1.Parameters.AddWithValue("@ID", currentUserID);
                            cmd_1.Parameters.AddWithValue("@result", tongSoNgay);
                            cmd_1.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Hiển thị kết quả lên Label hoặc TextBox
            txbtongngaydangky.Text = "Tổng số ngày đăng ký: " + tongSoNgay.ToString();
        }

        private void panelketqua_Paint(object sender, PaintEventArgs e)
        {
            LoadDangKyLamThem(currentUserID);
            LoadNhanVienData2();
        }

        ////bảng lương
        ///
        

        private void panelbangluong_Paint(object sender, PaintEventArgs e)
        {
            
            
        }

        
        // Hàm lấy dữ liệu từ SQL Server
        private void LoadBangLuong()
        {
            int ID_NhanVien;
            if (!int.TryParse(txbmanhanvien.Text, out ID_NhanVien))
            {
                MessageBox.Show("ID Nhân Viên không hợp lệ!");
                return;
            }
            try
            {
                string query = @"
            SELECT Luong.ID_Luong, NhanVien.ID_NhanVien, NhanVien.HoTen, Luong.ThangNam, 
                   Luong.TongSoNgayLamThem, Luong_ChucVu.LuongCoBan, 
                   TroCap.SoTien AS SoTienTroCap, 
                   COALESCE(SUM(Thuong.SoTienThuong), 0) AS TongSoTienThuong, 
                   Luong.TongLuong 
            FROM Luong 
            JOIN NhanVien ON Luong.ID_NhanVien = NhanVien.ID_NhanVien 
            JOIN Luong_ChucVu ON Luong.ID_LuongChucVu = Luong_ChucVu.ID_LuongChucVu 
            LEFT JOIN TroCap ON Luong.ID_TroCap = TroCap.ID_TroCap 
            LEFT JOIN Thuong ON Luong.ID_NhanVien = Thuong.ID_NhanVien 
            LEFT JOIN PhamLoi ON NhanVien.ID_NhanVien = PhamLoi.ID_NhanVien 
            WHERE NhanVien.ID_NhanVien = @ID
            GROUP BY Luong.ID_Luong, NhanVien.ID_NhanVien, NhanVien.HoTen, 
                     Luong.ThangNam, Luong.TongSoNgayLamThem, Luong_ChucVu.LuongCoBan, 
                     TroCap.SoTien, Luong.TongLuong 
            ORDER BY Luong.ThangNam DESC;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID_NhanVien); // Truyền ID nhân viên vào query

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView3.DataSource = dt; // Gán dữ liệu vào DataGridView
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            this.Hide();
            using(FormChangePassword formChangePassword = new FormChangePassword())
            {
                formChangePassword.ShowDialog();
            }
            this.Show();
        }

        private void btnDangSuat_Click(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            nv.LogOut(this);


        }
    }
}
