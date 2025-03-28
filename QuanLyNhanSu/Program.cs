﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormLogin loginForm = new FormLogin();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Form mainForm;

                if (User.Role == "admin")
                {
                    mainForm = new FormAdminHomePage();  // Admin form
                }
                else
                {
                    mainForm = new FormUser(User.ID_NhanVien);  // User form
                }

                Application.Run(mainForm);
            }
        }

    }
}
