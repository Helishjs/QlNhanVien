using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }


        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnthongtin_Click(object sender, EventArgs e)
        {
            Show(panelthongtin);
        }

        private void btndangkylam_Click(object sender, EventArgs e)
        {
            Show(paneldangky);
        }
        private void btnketqua_Click(object sender, EventArgs e)
        {
            Show(panelketqua);
        }

        private void btnluutru_Click(object sender, EventArgs e)
        {
            Show(panelbangluong);
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            Show(panelthongtin);

            // Tạo cột checkbox cho DataGridView
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Name = "DangKy";
            checkBoxColumn.HeaderText = "Đăng ký";
            checkBoxColumn.Width = 50;
            checkBoxColumn.TrueValue = true;
            checkBoxColumn.FalseValue = false;

            // Thêm cột checkbox vào DataGridView
            dataGridView1.Columns.Add(checkBoxColumn);

        }
        private void Show(Panel panel)
        {
            foreach (Control ctrl in panelthongtin.Controls)
            {
                if (ctrl is Panel)
                    ctrl.Visible = false;
            }
            panel.Visible = true;
            panel.BringToFront();
        }
        private void panelthongtin_Paint(object sender, PaintEventArgs e)
        {
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tptuychon_Click(object sender, EventArgs e)
        {

        }
        ///bảng đăng ký công

        private void LoadDataGridView(int month, int year)
        {
            dataGridView1.Rows.Clear();
            DateTime firstDay = new DateTime(year, month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            for (DateTime date = firstDay; date <= lastDay; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    int index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells["ngaythang"].Value = date.ToString("dd/MM");
                    dataGridView1.Rows[index].Cells["calam"].Value = (date.DayOfWeek == DayOfWeek.Saturday) ? "Thứ 7" : "Chủ nhật";
                    dataGridView1.Rows[index].Cells["dangky"].Value = false; // Mặc định là chưa đăng ký
                }
            }
        }

        private void btnUpdatetable_Click(object sender, EventArgs e)
        {
            if (cboMonth.SelectedItem != null && cboYear.SelectedItem != null)
            {
                int month = (int)cboMonth.SelectedItem;
                int year = (int)cboYear.SelectedItem;
                LoadDataGridView(month, year);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMonth.SelectedItem != null && cboYear.SelectedItem != null)
            {
                LoadDataGridView((int)cboMonth.SelectedItem, (int)cboYear.SelectedItem);
            }
        }

        private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMonth.SelectedItem != null && cboYear.SelectedItem != null)
            {
                LoadDataGridView((int)cboMonth.SelectedItem, (int)cboYear.SelectedItem);
            }
        }

        private void paneldangky_VisibleChanged(object sender, EventArgs e)
        {
            if (paneldangky.Visible && cboMonth.Items.Count == 0) // Chỉ chạy 1 lần

                InitComboBoxes();
        }
        private void InitComboBoxes()
        {
            // Tạo danh sách tháng (1-12)
            cboMonth.DataSource = Enumerable.Range(1, 12).ToList();
            cboMonth.DropDownStyle = ComboBoxStyle.DropDownList; // Đảm bảo chỉ chọn từ danh sách
            cboMonth.SelectedIndex = DateTime.Now.Month - 1; // Chọn tháng hiện tại

            // Tạo danh sách năm (từ 5 năm trước đến 5 năm sau)
            int currentYear = DateTime.Now.Year;
            cboYear.DataSource = Enumerable.Range(currentYear - 5, 11).ToList();
            cboYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cboYear.SelectedItem = currentYear;
        }

        private void btnxacnhan_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells["dangky"] as DataGridViewCheckBoxCell;
                if (chk != null && Convert.ToBoolean(chk.Value) == true)
                {
                    string ngay = row.Cells["ngaythang"].Value.ToString();
                    string caLam = row.Cells["calam"].Value.ToString();
                    MessageBox.Show($"Bạn đã đăng ký ngày: {ngay} - Ca: {caLam}");
                }
            }
            string connectionString = @"Data Source=MSI;Initial Catalog=QLNhansu;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["dangky"] as DataGridViewCheckBoxCell;
                    if (chk != null && Convert.ToBoolean(chk.Value) == true)
                    {
                        string ngayLam = row.Cells["ngaythang"].Value.ToString();
                        string caLam = row.Cells["calam"].Value.ToString();
                        DateTime ngayLamDate = DateTime.ParseExact(ngayLam, "d/M", null);
                        int ID_NhanVien = 1;

                        string query = "INSERT INTO Dangkylamthem (ID_Nhanvien, NgayLam, CaLam) VALUES (@ID_NhanVien, @NgayLam, @CaLam)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID_NhanVien", 1);
                            cmd.Parameters.AddWithValue("@NgayLam", ngayLamDate);
                            cmd.Parameters.AddWithValue("@CaLam", caLam);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Đăng ký thành công!");

            }

        }
        private void Loadketquadangky()
        {
            string connectionString = @"Data Source=MSI;Initial Catalog=QLNhansu;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query_ketqua = @"SELECT NgayLam,CaLam FROM Dangkylamthem WHERE ID_Nhanvien = @ID_Nhanvien";
                using (SqlCommand cmd = new SqlCommand(query_ketqua, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Nhanvien", 1); // Thay bằng ID nhân viên thực tế
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView3.AutoGenerateColumns = true;
                    dataGridView3.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                }
            }
        }

        private void btnxemketqua_Click(object sender, EventArgs e)
        {
            Loadketquadangky();
        }



        ///bảng lương
        
    }
}
