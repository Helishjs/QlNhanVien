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
            this.tbusername = new System.Windows.Forms.TextBox();
            this.tbpassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblpassword = new System.Windows.Forms.Label();
            this.btnlogin = new System.Windows.Forms.Button();
            this.btnressetpassword = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnshowpassword = new System.Windows.Forms.Button();
            this.tbcccd = new System.Windows.Forms.TextBox();
            this.lblcccd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbusername
            // 
            this.tbusername.Location = new System.Drawing.Point(188, 150);
            this.tbusername.Name = "tbusername";
            this.tbusername.Size = new System.Drawing.Size(272, 20);
            this.tbusername.TabIndex = 0;
            this.tbusername.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tbpassword
            // 
            this.tbpassword.Location = new System.Drawing.Point(188, 206);
            this.tbpassword.Name = "tbpassword";
            this.tbpassword.Size = new System.Drawing.Size(272, 20);
            this.tbpassword.TabIndex = 1;
            this.tbpassword.UseSystemPasswordChar = true;
            this.tbpassword.TextChanged += new System.EventHandler(this.tbpassword_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(185, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên đăng nhập";
            // 
            // lblpassword
            // 
            this.lblpassword.AutoSize = true;
            this.lblpassword.ForeColor = System.Drawing.Color.White;
            this.lblpassword.Location = new System.Drawing.Point(185, 190);
            this.lblpassword.Name = "lblpassword";
            this.lblpassword.Size = new System.Drawing.Size(52, 13);
            this.lblpassword.TabIndex = 3;
            this.lblpassword.Text = "Mật khẩu";
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(283, 295);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(88, 38);
            this.btnlogin.TabIndex = 4;
            this.btnlogin.Text = "Đăng nhập";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btnressetpassword
            // 
            this.btnressetpassword.Location = new System.Drawing.Point(365, 244);
            this.btnressetpassword.Name = "btnressetpassword";
            this.btnressetpassword.Size = new System.Drawing.Size(95, 29);
            this.btnressetpassword.TabIndex = 5;
            this.btnressetpassword.Text = "Quên mật khẩu?";
            this.btnressetpassword.UseVisualStyleBackColor = true;
            this.btnressetpassword.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(241, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 37);
            this.label3.TabIndex = 8;
            this.label3.Text = "Đăng nhập";
            // 
            // btnshowpassword
            // 
            this.btnshowpassword.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnshowpassword.Location = new System.Drawing.Point(421, 204);
            this.btnshowpassword.Name = "btnshowpassword";
            this.btnshowpassword.Size = new System.Drawing.Size(39, 23);
            this.btnshowpassword.TabIndex = 9;
            this.btnshowpassword.Text = "👁";
            this.btnshowpassword.UseVisualStyleBackColor = false;
            this.btnshowpassword.Click += new System.EventHandler(this.btnshowpassword_Click);
            // 
            // tbcccd
            // 
            this.tbcccd.Location = new System.Drawing.Point(188, 206);
            this.tbcccd.Name = "tbcccd";
            this.tbcccd.Size = new System.Drawing.Size(237, 20);
            this.tbcccd.TabIndex = 10;
            // 
            // lblcccd
            // 
            this.lblcccd.AutoSize = true;
            this.lblcccd.Location = new System.Drawing.Point(188, 190);
            this.lblcccd.Name = "lblcccd";
            this.lblcccd.Size = new System.Drawing.Size(36, 13);
            this.lblcccd.TabIndex = 11;
            this.lblcccd.Text = "CCCD";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(644, 371);
            this.Controls.Add(this.btnshowpassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnressetpassword);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.lblpassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbpassword);
            this.Controls.Add(this.tbusername);
            this.Controls.Add(this.lblcccd);
            this.Controls.Add(this.tbcccd);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbusername;
        private System.Windows.Forms.TextBox tbpassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblpassword;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btnressetpassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnshowpassword;
        private System.Windows.Forms.TextBox tbcccd;
        private System.Windows.Forms.Label lblcccd;
    }
}