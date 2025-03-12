using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace QuanLyNhanSu
{
    public partial class FormSuaNhanVien: Form
    {
        string connectionString = @"Data Source=DESKTOP-B4J24OU\MSSQLSERVER01;Initial Catalog=QLNhanVien;Integrated Security=True;TrustServerCertificate=True";
        public string ID;
        public FormSuaNhanVien(string id,string hoTen, DateTime ngaySinh, string gioiTinh,
                           string queQuan, string email, string sdt, string soCCCD,
                           string diaChi, string phongBan, string chucVu)
        {
            InitializeComponent();
            this.ID = id;
            tbHoten.Text = hoTen;
            dtNgaysinh.Value = ngaySinh;
            tbGioitinh.Text = gioiTinh;
            tbQuequan.Text = queQuan;
            tbEmail.Text = email;
            tbSDT.Text = sdt;
            tbCCCD.Text = soCCCD;
            tbDiachi.Text = diaChi;
            cbPhongban.SelectedItem = phongBan;
            cbChucvu.SelectedItem = chucVu;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            GetNhanVien nv = new GetNhanVien();
            nv.UpdateNhanVien(ID, tbHoten.Text, dtNgaysinh.Value, tbGioitinh.Text, tbQuequan.Text,
                      tbEmail.Text, tbSDT.Text, tbCCCD.Text, tbDiachi.Text,
                      cbPhongban.SelectedValue.ToString(), cbChucvu.SelectedValue.ToString());
            this.Close();


        }

        private void FormSuaNhanVien_Load(object sender, EventArgs e)
        {
            GetNhanVien Nv = new GetNhanVien();
            Nv.LoadNhanVien(dataGridViewNhanVien);
            using (SqlConnection sqlconnect = new SqlConnection(connectionString))
            {
                sqlconnect.Open();
                string query_1 = @"SELECT ChucVu.ID_ChucVu, ChucVu.Ten_ChucVu, PhongBan.ID_PhongBan, PhongBan.Ten_PhongBan FROM ChucVu JOIN NhanVien ON ChucVu.ID_ChucVu = NhanVien.ID_ChucVu JOIN PhongBan ON PhongBan.ID_PhongBan = NhanVien.ID_PhongBan";
                using(SqlDataAdapter sqldata = new SqlDataAdapter(query_1, sqlconnect))
                {
                    DataTable dt = new DataTable();
                    sqldata.Fill(dt);

                    cbChucvu.DataSource = dt;
                    cbChucvu.DisplayMember = "Ten_ChucVu";
                    cbChucvu.ValueMember = "ID_ChucVu";

                    cbPhongban.DataSource = dt;
                    cbPhongban.DisplayMember = "Ten_PhongBan";
                    cbPhongban.ValueMember = "ID_PhongBan";
                }
            }
        }
    }
}
