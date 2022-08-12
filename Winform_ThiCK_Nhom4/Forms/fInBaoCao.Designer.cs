namespace Winform_ThiCK_Nhom4.Forms
{
    partial class fInBaoCao
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
            this.rvBaoCao = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvBaoCao
            // 
            this.rvBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvBaoCao.Location = new System.Drawing.Point(0, 0);
            this.rvBaoCao.Name = "rvBaoCao";
            this.rvBaoCao.ServerReport.BearerToken = null;
            this.rvBaoCao.Size = new System.Drawing.Size(831, 442);
            this.rvBaoCao.TabIndex = 0;
            // 
            // fInBaoCao
            // 
            this.ClientSize = new System.Drawing.Size(831, 442);
            this.Controls.Add(this.rvBaoCao);
            this.Name = "fInBaoCao";
            this.Text = "fInBaoCao";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rfNhaCungCap_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Reporting.WinForms.ReportViewer rvBaoCao;
    }
}