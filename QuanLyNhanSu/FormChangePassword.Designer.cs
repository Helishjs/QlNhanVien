namespace QuanLyNhanSu
{
    partial class FormChangePassword
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
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbrepassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tboldpassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnshowpassword = new System.Windows.Forms.Button();
            this.tbnewpassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(170, 266);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(116, 29);
            this.btnChangePassword.TabIndex = 9;
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nhập lại mật khẩu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mật khẩu mới";
            // 
            // tbrepassword
            // 
            this.tbrepassword.Location = new System.Drawing.Point(96, 206);
            this.tbrepassword.Name = "tbrepassword";
            this.tbrepassword.Size = new System.Drawing.Size(272, 20);
            this.tbrepassword.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Mật khẩu";
            // 
            // tboldpassword
            // 
            this.tboldpassword.Location = new System.Drawing.Point(96, 93);
            this.tboldpassword.Name = "tboldpassword";
            this.tboldpassword.Size = new System.Drawing.Size(272, 20);
            this.tboldpassword.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(139, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 33);
            this.label4.TabIndex = 12;
            this.label4.Text = "Đổi mật khẩu";
            // 
            // btnshowpassword
            // 
            this.btnshowpassword.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnshowpassword.Location = new System.Drawing.Point(336, 149);
            this.btnshowpassword.Name = "btnshowpassword";
            this.btnshowpassword.Size = new System.Drawing.Size(32, 21);
            this.btnshowpassword.TabIndex = 13;
            this.btnshowpassword.Text = "👁";
            this.btnshowpassword.UseVisualStyleBackColor = false;
            this.btnshowpassword.Click += new System.EventHandler(this.btnshowpassword_Click);
            // 
            // tbnewpassword
            // 
            this.tbnewpassword.Location = new System.Drawing.Point(96, 150);
            this.tbnewpassword.Name = "tbnewpassword";
            this.tbnewpassword.Size = new System.Drawing.Size(272, 20);
            this.tbnewpassword.TabIndex = 5;
            // 
            // FormChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(475, 313);
            this.Controls.Add(this.btnshowpassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tboldpassword);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbrepassword);
            this.Controls.Add(this.tbnewpassword);
            this.Name = "FormChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbrepassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tboldpassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnshowpassword;
        private System.Windows.Forms.TextBox tbnewpassword;
    }
}