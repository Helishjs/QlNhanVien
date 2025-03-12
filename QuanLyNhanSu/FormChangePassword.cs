using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FormChangePassword: Form
    {
        public FormChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tboldpassword.Text) || string.IsNullOrWhiteSpace(tbnewpassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            GetNhanVien nv = new GetNhanVien();
            bool result = nv.ChangePassword(tboldpassword.Text, User.Username, tbnewpassword.Text);
            if (result)
            {
                this.Hide();
                FormLogin login = new FormLogin();
                login.ShowDialog();
                this.Close();
            }
        }

        private void btnshowpassword_Click(object sender, EventArgs e)
        {
            tbnewpassword.UseSystemPasswordChar = !tbnewpassword.UseSystemPasswordChar;
        }
    }
}
