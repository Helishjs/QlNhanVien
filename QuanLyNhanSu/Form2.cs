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
    public partial class FormAdminHomePage: Form
    {
        public FormAdminHomePage()
        {
            InitializeComponent();
        }
        private Form currentchildForm;
        private void OpenChildForm(Form children)
        {
            if (currentchildForm != null)
            {
                currentchildForm.Close();               
            }
            currentchildForm = children;
            children.TopLevel = false;
            children.FormBorderStyle = FormBorderStyle.None;
            children.Dock = DockStyle.Fill;
            panel3.Controls.Add(children);
            panel3.Tag = children;
            children.BringToFront();
            children.Show();
        }

        private void bangluong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormXemBangLuong());
            label1.Text = "BẢNG LƯƠNG";
        }

        private void loi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form5());
            label1.Text = "TRA CỨU LỖI";
        }

        private void khen_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form6());
            label1.Text = "TRA CỨU THƯỞNG";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentchildForm != null)
            {
                currentchildForm.Close();
            }
            label1.Text = "ADMIN HOME";
        }
    }
}
