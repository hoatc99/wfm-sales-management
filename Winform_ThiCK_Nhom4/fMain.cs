using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winform_ThiCK_Nhom4.Forms;

namespace Winform_ThiCK_Nhom4
{
    public partial class fMain : Form
    {
        public int userId = 0;

        Form currentOpenForm = null;

        Button currentBtn = null;

        public fMain(int userId)
        {
            InitializeComponent();
            mnuDong.Visible = false;
            KeyUp += new KeyEventHandler(HandleKeyUp);
            timer1.Start();

            this.userId = userId;

            string queryNV = $"SELECT TenNV FROM NhanVien WHERE MaNV = {userId}";

            DataTable dt = new DataTable();

            dt = DBHandler.Instance.Read(queryNV, "NhanVien");

            lblWelcome.Text += dt.Rows[0][0].ToString();

            string wkd = DateTime.Today.ToString("ddd");
            switch (wkd)
            {
                case "Sun":
                    wkd = "Chủ nhật";
                    break;
                case "Mon":
                    wkd = "Thứ hai";
                    break;
                case "Tue":
                    wkd = "Thứ ba";
                    break;
                case "Wed":
                    wkd = "Thứ tư";
                    break;
                case "Thu":
                    wkd = "Thứ năm";
                    break;
                case "Fri":
                    wkd = "Thứ sáu";
                    break;
                case "Sat":
                    wkd = "Thứ bảy";
                    break;
            }
            lblDate.Text = wkd + DateTime.Today.ToString(", dd/MM/yyyy");
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            btnNghiepVu.PerformClick();
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.W)
            {
                mnuDong.PerformClick();
            }
            if (e.KeyCode == Keys.F1)
            {

            }
        }

        private void HideSubMenus()
        {
            pnlMenuNghiepVu.Visible = false;
            pnlMenuQuanLy.Visible = false;
            pnlMenuTimKiem.Visible = false;
            pnlMenuBaoCao.Visible = false;
            pnlMenuHeThong.Visible = false;
        }

        public static DataGridView SetDataGridViewProperties(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.Font = new Font("Arial", 14F, GraphicsUnit.Pixel);
            dgv.Columns[0].Width = 50;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            return dgv;
        }

        private void HandleOpenForm(Form form, object senderBtn)
        {
            mnuDong.PerformClick();
            form.TopLevel = false;
            form.AutoScroll = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(form);
            form.BringToFront();
            form.Show();
            currentOpenForm = form;
            mnuDong.Visible = true;
            ActiveButton(senderBtn);
        }

        public static void HandleOpenReportViewer(string reportName, DataTable dt)
        {
            fInBaoCao fInBaoCao = new fInBaoCao();
            ReportDataSource rds = new ReportDataSource();
            fInBaoCao.rvBaoCao.LocalReport.ReportEmbeddedResource = $"Winform_ThiCK_Nhom4.Reports.{reportName}.rdlc";
            rds.Name = "DataSet1";
            rds.Value = dt;
            fInBaoCao.rvBaoCao.LocalReport.DataSources.Add(rds);
            fInBaoCao.rvBaoCao.RefreshReport();
            fInBaoCao.Show();
        }

        public static void HandleCurrencyFormattingForDataGridViewColumn(DataGridViewColumn dgvc)
        {
            dgvc.DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN").NumberFormat;
            dgvc.DefaultCellStyle.Format = "c0";
            dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void mnuNghiepVuMuaHang_Click(object sender, EventArgs e)
        {
            pnlMenuNghiepVu.Visible = true;
            btnNghiepVuMuaHang.PerformClick();
        }

        private void mnuNghiepVuChiTien_Click(object sender, EventArgs e)
        {
            pnlMenuNghiepVu.Visible = true;
            btnNghiepVuChiTien.PerformClick();
        }

        private void mnuQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            pnlMenuQuanLy.Visible = true;
            btnQuanLyNhaCungCap.PerformClick();
        }

        private void mnuQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            pnlMenuQuanLy.Visible = true;
            btnQuanLyNhanVien.PerformClick();
        }

        private void mnuQuanLyHoaDonMuaHang_Click(object sender, EventArgs e)
        {
            pnlMenuQuanLy.Visible = true;
            btnQuanLyHoaDonMuaHang.PerformClick();
        }

        private void mnuQuanLySanPham_Click(object sender, EventArgs e)
        {
            pnlMenuQuanLy.Visible = true;
            btnQuanLySanPham.PerformClick();
        }

        private void mnuTimKiemNhaCungCap_Click(object sender, EventArgs e)
        {
            pnlMenuTimKiem.Visible = true;
            btnTimKiemNhaCungCap.PerformClick();
        }

        private void mnuTimKiemNhanVien_Click(object sender, EventArgs e)
        {
            pnlMenuTimKiem.Visible = true;
            btnTimKiemNhanVien.PerformClick();
        }

        private void mnuTimKiemSanPham_Click(object sender, EventArgs e)
        {
            pnlMenuTimKiem.Visible = true;
            btnTimKiemSanPham.PerformClick();
        }

        private void mnuTimKiemHoaDonMuaHang_Click(object sender, EventArgs e)
        {
            pnlMenuTimKiem.Visible = true;
            btnTimKiemHoaDonMuaHang.PerformClick();
        }

        private void mnuTimKiemPhieuChi_Click(object sender, EventArgs e)
        {
            pnlMenuTimKiem.Visible = true;
            btnTimKiemPhieuChi.PerformClick();
        }

        private void mnuLapBaoCaoTinhHinhMuaHang_Click(object sender, EventArgs e)
        {
            pnlMenuBaoCao.Visible = true;
            btnBaoCaoTinhHinhMuaHang.PerformClick();
        }

        private void mnuLapBaoCaoCongNoNCC_Click(object sender, EventArgs e)
        {
            pnlMenuBaoCao.Visible = true;
            btnBaoCaoCongNoNCC.PerformClick();
        }

        private void mnuLapBaoCaoTonKho_Click(object sender, EventArgs e)
        {
            pnlMenuBaoCao.Visible = true;
            btnBaoCaoTonKho.PerformClick();
        }

        private void mnuHeThongThongTin_Click(object sender, EventArgs e)
        {
            pnlMenuHeThong.Visible = true;
            btnHeThongThongTin.PerformClick();
        }

        private void mnuHeThongDangXuat_Click(object sender, EventArgs e)
        {
            pnlMenuHeThong.Visible = true;
            btnHeThongDangXuat.PerformClick();
        }

        private void mnuHeThongThoat_Click(object sender, EventArgs e)
        {
            pnlMenuHeThong.Visible = true;
            btnHeThongThoat.PerformClick();
        }

        private void mnuDong_Click(object sender, EventArgs e)
        {
            if (currentOpenForm != null)
            {
                currentOpenForm.Close();
            }
            mnuDong.Visible = false;
            DisableButton();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void ActiveButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (Button)senderBtn;
                currentBtn.BackColor = Color.FromArgb(192, 255, 255);
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(255, 192, 192);
            }
        }

        private void btnNghiepVu_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            pnlMenuNghiepVu.Visible = true;
        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            pnlMenuQuanLy.Visible = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            pnlMenuTimKiem.Visible = true;
        }

        private void btnLapBaoCao_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            pnlMenuBaoCao.Visible = true;
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            pnlMenuHeThong.Visible = true;
        }

        private void btnNghiepVuMuaHang_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnNghiepVu.PerformClick();
            HandleOpenForm(new fNghiepVuMuaHang(userId), sender);
        }

        private void btnNghiepVuChiTien_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnNghiepVu.PerformClick();
            HandleOpenForm(new fNghiepVuChiTien(), sender);
        }

        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnQuanLy.PerformClick();
            HandleOpenForm(new fQuanLyNhaCungCap(), sender);
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnQuanLy.PerformClick();
            HandleOpenForm(new fQuanLyNhanVien(), sender);
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnQuanLy.PerformClick();
            HandleOpenForm(new fQuanLySanPham(), sender);
        }

        private void btnQuanLyHoaDonMuaHang_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnQuanLy.PerformClick();
            HandleOpenForm(new fQuanLyHDMH(), sender);
        }

        private void btnTimKiemNhaCungCap_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnTimKiem.PerformClick();
            HandleOpenForm(new fTimKiemNhaCungCap(), sender);
        }

        private void btnTimKiemNhanVien_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnTimKiem.PerformClick();
            HandleOpenForm(new fTimKiemNhanVien(), sender);
        }

        private void btnTimKiemSanPham_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnTimKiem.PerformClick();
            HandleOpenForm(new fTimKiemSanPham(), sender);
        }

        private void btnTimKiemHoaDonMuaHang_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnTimKiem.PerformClick();
            HandleOpenForm(new fTimKiemHDMH(), sender);
        }

        private void btnTimKiemPhieuChi_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnTimKiem.PerformClick();
            HandleOpenForm(new fTimKiemPhieuChi(), sender);
        }

        private void btnLapBaoCaoTonKho_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnLapBaoCao.PerformClick();
            ActiveButton(sender);
            string query = "SELECT * FROM V_SanPham_TonKho ORDER BY SoLuongTon DESC";
            DataTable dt = DBHandler.Instance.Read(query);
            HandleOpenReportViewer("rSanPhamTonKho", dt);
        }

        private void btnLapBaoCaoCongNoNCC_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnLapBaoCao.PerformClick();
            ActiveButton(sender);
            string query = "SELECT * FROM V_NhaCungCap_CongNo";
            DataTable dt = DBHandler.Instance.Read(query);
            HandleOpenReportViewer("rCongNoNCC", dt);
        }

        private void btnBaoCaoTinhHinhMuaHang_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnLapBaoCao.PerformClick();
            ActiveButton(sender);
            string query = "SELECT * FROM V_TinhHinhMuaHang ORDER BY Thang ASC, Nam ASC";
            DataTable dt = DBHandler.Instance.Read(query);
            HandleOpenReportViewer("rTinhHinhMuaHang", dt);
        }

        private void btnHeThongThongTin_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnHeThong.PerformClick();
            MessageBox.Show("Tác giả:\n1. Trần Cẩm Hòa - 20661002\n2. Trần Văn Trí - 20661040", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHeThongDangXuat_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnHeThong.PerformClick();
            userId = 0;
            Close();
        }

        private void btnHeThongThoat_Click(object sender, EventArgs e)
        {
            DisableButton();
            btnHeThong.PerformClick();
            Application.Exit();
        }
    }
}
