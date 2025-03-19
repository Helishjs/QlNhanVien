namespace QuanLyNhanSu
{
    partial class DanhSachNhanVien
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.OThayDoi = new System.Windows.Forms.Panel();
            this.Stoppp = new System.Windows.Forms.Button();
            this.ChinhSua = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Okkk = new System.Windows.Forms.Button();
            this.ChuotPhai = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chỉnhSửaÔToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OThayDoi.SuspendLayout();
            this.ChuotPhai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // OThayDoi
            // 
            this.OThayDoi.BackColor = System.Drawing.Color.White;
            this.OThayDoi.Controls.Add(this.Stoppp);
            this.OThayDoi.Controls.Add(this.ChinhSua);
            this.OThayDoi.Controls.Add(this.label1);
            this.OThayDoi.Controls.Add(this.Okkk);
            this.OThayDoi.Location = new System.Drawing.Point(199, 141);
            this.OThayDoi.Margin = new System.Windows.Forms.Padding(2);
            this.OThayDoi.Name = "OThayDoi";
            this.OThayDoi.Size = new System.Drawing.Size(206, 165);
            this.OThayDoi.TabIndex = 6;
            // 
            // Stoppp
            // 
            this.Stoppp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Stoppp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Stoppp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Stoppp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stoppp.Location = new System.Drawing.Point(92, 100);
            this.Stoppp.Margin = new System.Windows.Forms.Padding(2);
            this.Stoppp.Name = "Stoppp";
            this.Stoppp.Size = new System.Drawing.Size(48, 45);
            this.Stoppp.TabIndex = 3;
            this.Stoppp.Text = "Hủy";
            this.Stoppp.UseVisualStyleBackColor = false;
            this.Stoppp.Click += new System.EventHandler(this.Stoppp_Click);
            // 
            // ChinhSua
            // 
            this.ChinhSua.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ChinhSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChinhSua.Location = new System.Drawing.Point(80, 57);
            this.ChinhSua.Margin = new System.Windows.Forms.Padding(2);
            this.ChinhSua.Name = "ChinhSua";
            this.ChinhSua.Size = new System.Drawing.Size(106, 28);
            this.ChinhSua.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nhập: ";
            // 
            // Okkk
            // 
            this.Okkk.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Okkk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Okkk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Okkk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Okkk.Location = new System.Drawing.Point(145, 100);
            this.Okkk.Margin = new System.Windows.Forms.Padding(2);
            this.Okkk.Name = "Okkk";
            this.Okkk.Size = new System.Drawing.Size(48, 45);
            this.Okkk.TabIndex = 0;
            this.Okkk.Text = "Thay Đổi";
            this.Okkk.UseVisualStyleBackColor = false;
            this.Okkk.Click += new System.EventHandler(this.Okkk_Click);
            // 
            // ChuotPhai
            // 
            this.ChuotPhai.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ChuotPhai.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chỉnhSửaÔToolStripMenuItem,
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem});
            this.ChuotPhai.Name = "contextMenuStrip1";
            this.ChuotPhai.Size = new System.Drawing.Size(256, 48);
            // 
            // chỉnhSửaÔToolStripMenuItem
            // 
            this.chỉnhSửaÔToolStripMenuItem.Name = "chỉnhSửaÔToolStripMenuItem";
            this.chỉnhSửaÔToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.chỉnhSửaÔToolStripMenuItem.Text = "Chỉnh Sửa Ô";
            this.chỉnhSửaÔToolStripMenuItem.Click += new System.EventHandler(this.chỉnhSửaÔToolStripMenuItem_Click);
            // 
            // xemThôngTinChiTiếtNhânViênToolStripMenuItem
            // 
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem.Name = "xemThôngTinChiTiếtNhânViênToolStripMenuItem";
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem.Text = "Xem Thông Tin Chi Tiết Nhân Viên";
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem.Click += new System.EventHandler(this.xemThôngTinChiTiếtNhânViênToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1000, 600);
            this.dataGridView1.TabIndex = 7;
            // 
            // DanhSachNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OThayDoi);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DanhSachNhanVien";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.DanhSachNhanVien_Load);
            this.OThayDoi.ResumeLayout(false);
            this.OThayDoi.PerformLayout();
            this.ChuotPhai.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel OThayDoi;
        private System.Windows.Forms.Button Stoppp;
        private System.Windows.Forms.TextBox ChinhSua;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Okkk;
        private System.Windows.Forms.ContextMenuStrip ChuotPhai;
        private System.Windows.Forms.ToolStripMenuItem chỉnhSửaÔToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemThôngTinChiTiếtNhânViênToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
