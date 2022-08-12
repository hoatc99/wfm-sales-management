using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_ThiCK_Nhom4.Forms
{
    public partial class fNghiepVuMuaHang : Form
    {
        private DataSet ds = new DataSet();

        private DataRow row;

        int pos = 0;

        int userId = 0;

        int posCTHD = 0;

        public fNghiepVuMuaHang(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void fNghiepVuMuaHang_Load(object sender, EventArgs e)
        {
            string queryNV = $"SELECT MaNV, TenNV FROM NhanVien WHERE MaNV = {userId}";

            ds.Tables.Add(DBHandler.Instance.Read(queryNV, "NhanVien"));

            cbxNhanVien.DataSource = ds.Tables["NhanVien"];
            cbxNhanVien.DisplayMember = "TenNV";
            cbxNhanVien.ValueMember = "MaNV";

            string queryNCC = "SELECT MaNCC, TenNCC FROM NhaCungCap";

            ds.Tables.Add(DBHandler.Instance.Read(queryNCC, "NhaCungCap"));

            cbxNhaCungCap.DataSource = ds.Tables["NhaCungCap"];
            cbxNhaCungCap.DisplayMember = "TenNCC";
            cbxNhaCungCap.ValueMember = "MaNCC";

            string queryMaxMaHDMH = "SELECT max(maHDMH) + 1 FROM HDMH";

            ds.Tables.Add(DBHandler.Instance.Read(queryMaxMaHDMH, "MaxMaHDMH"));

            txtMaHDMH.Text = ds.Tables["MaxMaHDMH"].Rows[0][0].ToString();
            txtMaNV.Text = cbxNhanVien.SelectedValue.ToString();
            txtMaNCC.Text = cbxNhaCungCap.SelectedValue.ToString();

            string queryHDMH = "SELECT * FROM HDMH";

            ds.Tables.Add(DBHandler.Instance.Read(queryHDMH, "HDMH"));

            string queryCTHDMH = "SELECT * FROM CTHDMH";

            ds.Tables.Add(DBHandler.Instance.Read(queryCTHDMH, "CTHDMH"));

            LoadProductListToDataGridView();

            fMain.SetDataGridViewProperties(dgvSanPham);

            dgvCTHD.ColumnCount = 6;
            dgvCTHD.Columns[0].HeaderText = "MaHD";
            dgvCTHD.Columns[1].HeaderText = "MaSP";
            dgvCTHD.Columns[2].HeaderText = "SL";
            dgvCTHD.Columns[3].HeaderText = "DG";
            dgvCTHD.Columns[4].HeaderText = "CK";
            dgvCTHD.Columns[5].HeaderText = "CP";
            fMain.SetDataGridViewProperties(dgvCTHD);

            UpdateTotal();

            UpdateFields();
        }
        
        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtMaSP, "");
            errorProvider1.SetError(txtSoLuong, "");
            errorProvider1.SetError(txtDonGia, "");
            errorProvider1.SetError(nudChietKhau, "");
            errorProvider1.SetError(txtChiPhi, "");

            if (txtMaSP.Text == "")
            {
                errorProvider1.SetError(txtMaSP, "Bạn chưa chọn sản phẩm");
                return;
            }

            if (txtSoLuong.Text == "")
            {
                errorProvider1.SetError(txtSoLuong, "Số lượng không được để trống");
                txtSoLuong.Focus();
                return;
            }

            if (txtDonGia.Text == "")
            {
                errorProvider1.SetError(txtDonGia, "Đơn giá không được để trống");
                txtDonGia.Focus();
                return;
            }

            if (nudChietKhau.Text == "")
            {
                errorProvider1.SetError(nudChietKhau, "Chiết khấu không được để trống");
                nudChietKhau.Focus();
                return;
            }

            if (txtChiPhi.Text == "")
            {
                errorProvider1.SetError(txtChiPhi, "Chi phí không được để trống");
                txtChiPhi.Focus();
                return;
            }

            string maHDMH = txtMaHDMH.Text;
            string maSP = txtMaSP.Text;
            string soLuong = txtSoLuong.Text;
            string donGia = txtDonGia.Text;
            string chietKhau = nudChietKhau.Text;
            string chiPhi = txtChiPhi.Text;

            for (int i = 0; i < dgvCTHD.Rows.Count; i++)
            {
                string cthdMaSP = dgvCTHD.Rows[i].Cells[1].Value.ToString();
                string cthdDonGia = dgvCTHD.Rows[i].Cells[3].Value.ToString();
                string cthdChietKhau = dgvCTHD.Rows[i].Cells[4].Value.ToString();
                string cthdChiPhi = dgvCTHD.Rows[i].Cells[5].Value.ToString();
                if (cthdMaSP == maSP && cthdDonGia == donGia && cthdChietKhau == chietKhau && cthdChiPhi == chiPhi)
                {
                    int sl = int.Parse(dgvCTHD.Rows[i].Cells[2].Value.ToString()) + int.Parse(soLuong);
                    dgvCTHD.Rows[i].Cells[2].Value = sl;
                    cbxNhaCungCap.Enabled = false;
                    UpdateTotal();
                    return;
                }
            }

            cbxNhaCungCap.Enabled = false;
            dgvCTHD.Rows.Add(maHDMH, maSP, soLuong, donGia, chietKhau, chiPhi);

            UpdateTotal();
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (dgvCTHD.Rows.Count > 0)
            {
                dgvCTHD.Rows.RemoveAt(posCTHD);
            }

            UpdateTotal();
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắc muốn lưu hóa đơn này không? Hóa đơn sau khi lưu sẽ không thể chỉnh sửa", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                row = ds.Tables["HDMH"].NewRow();

                row["NgayTao"] = DateTime.Today.ToString("dd/MM/yyyy");
                row["MaNV"] = txtMaNV.Text;
                row["MaNCC"] = txtMaNCC.Text;

                ds.Tables["HDMH"].Rows.Add(row);

                int resultHDMH = DBHandler.Instance.Update(ds, "HDMH");

                foreach (DataGridViewRow dgvrow in dgvCTHD.Rows)
                {
                    row = ds.Tables["CTHDMH"].NewRow();

                    row["MaHDMH"] = dgvrow.Cells[0].Value.ToString();
                    row["MaSP"] = dgvrow.Cells[1].Value.ToString();
                    row["SoLuong"] = dgvrow.Cells[2].Value.ToString();
                    row["DonGia"] = dgvrow.Cells[3].Value.ToString();
                    row["KhuyenMai"] = dgvrow.Cells[4].Value.ToString();
                    row["ChiPhi"] = dgvrow.Cells[5].Value.ToString();

                    ds.Tables["CTHDMH"].Rows.Add(row);
                }

                int resultCTHDMH = DBHandler.Instance.Update(ds, "CTHDMH");

                if (resultHDMH > 0 && resultCTHDMH > 0)
                {
                    MessageBox.Show("Lưu thành công");

                    EnableComponents();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
        }
        
        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = dgvSanPham.CurrentRow.Index;
            UpdateFields();
        }

        private void UpdateFields()
        {
            //row = ds.Tables[mainTableName].Rows[pos];
            //dgvPhieuChi.Rows[pos].Selected = true;
            //txtMaHDMH.Text = row["MaPC"].ToString();
            //cbxNhanVien.SelectedValue = row["MaNV"].ToString();
            //cbxNhaCungCap.SelectedValue = row["MaNCC"].ToString();
        }

        private DataRow FillRow(DataRow row)
        {
            row["MaNV"] = cbxNhanVien.SelectedValue.ToString();
            row["MaNCC"] = cbxNhaCungCap.SelectedValue.ToString();
            return row;
        }

        private void EnableComponents()
        {
            dgvSanPham.Enabled = true;
            btnThemSP.Enabled = true;
            btnLuuHoaDon.Enabled = true;
        }

        private void DisableComponents()
        {
            dgvSanPham.Enabled = false;
            btnThemSP.Enabled = false;
            btnLuuHoaDon.Enabled = false;
        }

        private void LoadProductListToDataGridView()
        {
            string querySP = $"SELECT * FROM SanPham WHERE MaNCC = {txtMaNCC.Text}";

            if (ds.Tables.Contains("SanPham"))
            {
                ds.Tables.Remove("SanPham");
            }

            ds.Tables.Add(DBHandler.Instance.Read(querySP, "SanPham"));

            dgvSanPham.DataSource = ds.Tables["SanPham"];
        }

        private void cbxNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaNV.Text = cbxNhanVien.SelectedValue.ToString();
        }

        private void cbxNhaCungCap_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtMaNCC.Text = cbxNhaCungCap.SelectedValue.ToString();
            LoadProductListToDataGridView();
        }

        private void UpdateTotal()
        {
            long tongTien = 0;
            long tongChietKhau = 0;
            int tongSoLuong = 0;
            long tongTienPhaiTT = 0;
            foreach (DataGridViewRow row in dgvCTHD.Rows)
            {
                int SL = int.Parse(row.Cells[2].Value.ToString());
                int DG = int.Parse(row.Cells[3].Value.ToString());
                int CK = int.Parse(row.Cells[4].Value.ToString());
                int CP = int.Parse(row.Cells[5].Value.ToString());
                tongTien += SL * DG + CP;
                tongChietKhau += SL * DG * CK / 100;
                tongSoLuong += SL;
            }
            tongTienPhaiTT = tongTien - tongChietKhau;

            txtTongTien.Text = tongTien.ToString();
            txtTongCK.Text = tongChietKhau.ToString();
            txtTongSL.Text = tongSoLuong.ToString();
            txtPhaiTT.Text = tongTienPhaiTT.ToString();
        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            posCTHD = e.RowIndex;
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            pos = e.RowIndex;
            row = ds.Tables["SanPham"].Rows[pos];
            txtMaSP.Text = row["MaSP"].ToString();
            txtTenSP.Text = row["TenSP"].ToString();
            txtDonGia.Text = int.Parse(row["DonGia"].ToString()).ToString();
            nudChietKhau.Value = int.Parse(row["KhuyenMai"].ToString());
        }
    }
}
