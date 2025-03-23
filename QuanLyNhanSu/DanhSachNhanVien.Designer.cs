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
            this.ChuotPhai = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TimKiem = new System.Windows.Forms.Button();
            this.TimKiemTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChuotPhai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChuotPhai
            // 
            this.ChuotPhai.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ChuotPhai.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemThôngTinChiTiếtNhânViênToolStripMenuItem});
            this.ChuotPhai.Name = "contextMenuStrip1";
            this.ChuotPhai.Size = new System.Drawing.Size(256, 26);
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
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(822, 348);
            this.dataGridView1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.TimKiem);
            this.panel1.Controls.Add(this.TimKiemTB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, -36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(822, 125);
            this.panel1.TabIndex = 8;
            // 
            // TimKiem
            // 
            this.TimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimKiem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimKiem.Location = new System.Drawing.Point(646, 61);
            this.TimKiem.Name = "TimKiem";
            this.TimKiem.Size = new System.Drawing.Size(117, 46);
            this.TimKiem.TabIndex = 2;
            this.TimKiem.Text = "Tìm Kiếm";
            this.TimKiem.UseVisualStyleBackColor = false;
            this.TimKiem.Click += new System.EventHandler(this.TimKiem_Click);
            // 
            // TimKiemTB
            // 
            this.TimKiemTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimKiemTB.Location = new System.Drawing.Point(315, 69);
            this.TimKiemTB.Name = "TimKiemTB";
            this.TimKiemTB.Size = new System.Drawing.Size(294, 29);
            this.TimKiemTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm kiếm theo ID_NhanVien:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // DanhSachNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DanhSachNhanVien";
            this.Size = new System.Drawing.Size(822, 433);
            this.Load += new System.EventHandler(this.DanhSachNhanVien_Load);
            this.ChuotPhai.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip ChuotPhai;
        private System.Windows.Forms.ToolStripMenuItem xemThôngTinChiTiếtNhânViênToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TimKiemTB;
        private System.Windows.Forms.Button TimKiem;
    }
}
