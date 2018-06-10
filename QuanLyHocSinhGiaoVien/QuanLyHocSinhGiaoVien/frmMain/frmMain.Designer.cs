namespace QuanLyHocSinhGiaoVien
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hỌCSINHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gIÁOVIÊNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lỚPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mÔNHỌCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tRỢGIÚPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đĂNGXUẤTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hỌCSINHToolStripMenuItem,
            this.gIÁOVIÊNToolStripMenuItem,
            this.lỚPToolStripMenuItem,
            this.mÔNHỌCToolStripMenuItem,
            this.tRỢGIÚPToolStripMenuItem,
            this.đĂNGXUẤTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(857, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hỌCSINHToolStripMenuItem
            // 
            this.hỌCSINHToolStripMenuItem.Name = "hỌCSINHToolStripMenuItem";
            this.hỌCSINHToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.hỌCSINHToolStripMenuItem.Text = "HỌC SINH";
            this.hỌCSINHToolStripMenuItem.Click += new System.EventHandler(this.hỌCSINHToolStripMenuItem_Click);
            // 
            // gIÁOVIÊNToolStripMenuItem
            // 
            this.gIÁOVIÊNToolStripMenuItem.Name = "gIÁOVIÊNToolStripMenuItem";
            this.gIÁOVIÊNToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.gIÁOVIÊNToolStripMenuItem.Text = "GIÁO VIÊN";
            this.gIÁOVIÊNToolStripMenuItem.Click += new System.EventHandler(this.gIÁOVIÊNToolStripMenuItem_Click);
            // 
            // lỚPToolStripMenuItem
            // 
            this.lỚPToolStripMenuItem.Name = "lỚPToolStripMenuItem";
            this.lỚPToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.lỚPToolStripMenuItem.Text = "LỚP";
            this.lỚPToolStripMenuItem.Click += new System.EventHandler(this.lỚPToolStripMenuItem_Click);
            // 
            // mÔNHỌCToolStripMenuItem
            // 
            this.mÔNHỌCToolStripMenuItem.Name = "mÔNHỌCToolStripMenuItem";
            this.mÔNHỌCToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.mÔNHỌCToolStripMenuItem.Text = "MÔN HỌC";
            this.mÔNHỌCToolStripMenuItem.Click += new System.EventHandler(this.mÔNHỌCToolStripMenuItem_Click);
            // 
            // tRỢGIÚPToolStripMenuItem
            // 
            this.tRỢGIÚPToolStripMenuItem.Name = "tRỢGIÚPToolStripMenuItem";
            this.tRỢGIÚPToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.tRỢGIÚPToolStripMenuItem.Text = "TRỢ GIÚP";
            // 
            // đĂNGXUẤTToolStripMenuItem
            // 
            this.đĂNGXUẤTToolStripMenuItem.Name = "đĂNGXUẤTToolStripMenuItem";
            this.đĂNGXUẤTToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.đĂNGXUẤTToolStripMenuItem.Text = "ĐĂNG XUẤT";
            this.đĂNGXUẤTToolStripMenuItem.Click += new System.EventHandler(this.đĂNGXUẤTToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(857, 395);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHƯƠNG TRÌNH QUẢN LÝ HỌC SINH, GIÁO VIÊN TRƯỜNG THPT";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hỌCSINHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gIÁOVIÊNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lỚPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mÔNHỌCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tRỢGIÚPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đĂNGXUẤTToolStripMenuItem;
    }
}