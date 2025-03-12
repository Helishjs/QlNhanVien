namespace QuanLyNhanSu
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblusername = new System.Windows.Forms.Label();
            this.lblpassword = new System.Windows.Forms.Label();
            this.tbusername = new System.Windows.Forms.TextBox();
            this.tbpassword = new System.Windows.Forms.TextBox();
            this.btnĐăngNhập = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Đăng nhập";
            // 
            // lblusername
            // 
            this.lblusername.AutoSize = true;
            this.lblusername.Location = new System.Drawing.Point(59, 71);
            this.lblusername.Name = "lblusername";
            this.lblusername.Size = new System.Drawing.Size(87, 13);
            this.lblusername.TabIndex = 3;
            this.lblusername.Text = "Tên đăng nhập :";
            // 
            // lblpassword
            // 
            this.lblpassword.AutoSize = true;
            this.lblpassword.Location = new System.Drawing.Point(59, 136);
            this.lblpassword.Name = "lblpassword";
            this.lblpassword.Size = new System.Drawing.Size(58, 13);
            this.lblpassword.TabIndex = 4;
            this.lblpassword.Text = "Mật khẩu :";
            // 
            // tbusername
            // 
            this.tbusername.Location = new System.Drawing.Point(62, 87);
            this.tbusername.Name = "tbusername";
            this.tbusername.Size = new System.Drawing.Size(329, 20);
            this.tbusername.TabIndex = 5;
            this.tbusername.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tbpassword
            // 
            this.tbpassword.Location = new System.Drawing.Point(62, 152);
            this.tbpassword.Name = "tbpassword";
            this.tbpassword.Size = new System.Drawing.Size(329, 20);
            this.tbpassword.TabIndex = 6;
            // 
            // btnĐăngNhập
            // 
            this.btnĐăngNhập.Location = new System.Drawing.Point(178, 211);
            this.btnĐăngNhập.Name = "btnĐăngNhập";
            this.btnĐăngNhập.Size = new System.Drawing.Size(94, 40);
            this.btnĐăngNhập.TabIndex = 0;
            this.btnĐăngNhập.Text = "Đăng nhập";
            this.btnĐăngNhập.UseVisualStyleBackColor = true;
            this.btnĐăngNhập.Click += new System.EventHandler(this.btnĐăngNhập_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 293);
            this.Controls.Add(this.btnĐăngNhập);
            this.Controls.Add(this.tbusername);
            this.Controls.Add(this.tbpassword);
            this.Controls.Add(this.lblpassword);
            this.Controls.Add(this.lblusername);
            this.Controls.Add(this.label1);
            this.Name = "FormLogin";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblusername;
        private System.Windows.Forms.Label lblpassword;
        private System.Windows.Forms.TextBox tbusername;
        private System.Windows.Forms.TextBox tbpassword;
        private System.Windows.Forms.Button btnĐăngNhập;
    }
}