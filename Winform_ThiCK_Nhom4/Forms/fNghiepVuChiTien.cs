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
    public partial class fNghiepVuChiTien : Form
    {
        private string mainTableName = "PhieuChi";

        private DataSet ds = new DataSet();

        private DataRow row;

        int pos = 0;

        bool isBtnThemClicked = false;

        public fNghiepVuChiTien()
        {
            InitializeComponent();
        }

        private void fNghiepVuChiTien_Load(object sender, EventArgs e)
        {
            string queryNV = "SELECT MaNV, TenNV FROM NhanVien";

            ds.Tables.Add(DBHandler.Instance.Read(queryNV, "NhanVien"));

            cbxNhanVien.DataSource = ds.Tables["NhanVien"];
            cbxNhanVien.DisplayMember = "TenNV";
            cbxNhanVien.ValueMember = "MaNV";

            string queryNCC = "SELECT MaNCC, TenNCC FROM NhaCungCap";

            ds.Tables.Add(DBHandler.Instance.Read(queryNCC, "NhaCungCap"));

            cbxNhaCungCap.DataSource = ds.Tables["NhaCungCap"];
            cbxNhaCungCap.DisplayMember = "TenNCC";
            cbxNhaCungCap.ValueMember = "MaNCC";

            string queryCongNo = "SELECT * FROM V_NhaCungCap_CongNo";

            ds.Tables.Add(DBHandler.Instance.Read(queryCongNo, "CongNo"));

            dgvCongNo.DataSource = ds.Tables["CongNo"];
            fMain.SetDataGridViewProperties(dgvCongNo);
            fMain.HandleCurrencyFormattingForDataGridViewColumn(dgvCongNo.Columns[2]);
            fMain.HandleCurrencyFormattingForDataGridViewColumn(dgvCongNo.Columns[3]);
            fMain.HandleCurrencyFormattingForDataGridViewColumn(dgvCongNo.Columns[4]);

            string query = $"SELECT * FROM {mainTableName}";

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName));

            dgvPhieuChi.DataSource = ds.Tables[mainTableName];
            fMain.SetDataGridViewProperties(dgvPhieuChi);
            fMain.HandleCurrencyFormattingForDataGridViewColumn(dgvPhieuChi.Columns[4]);

            UpdateFields();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            UpdateFields();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                pos--;
            }
            UpdateFields();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pos < ds.Tables[mainTableName].Rows.Count - 1)
            {
                pos++;
            }
            UpdateFields();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            pos = ds.Tables[mainTableName].Rows.Count - 1;
            UpdateFields();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnHuy.PerformClick();
            DisableComponents();
            EnableFields();
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            isBtnThemClicked = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(cbxNhanVien, "");
            errorProvider1.SetError(cbxNhaCungCap, "");
            errorProvider1.SetError(txtSoTien, "");

            if (cbxNhanVien.Text == "")
            {
                errorProvider1.SetError(cbxNhanVien, "Nhân viên không được để trống");
                cbxNhanVien.Focus();
                return;
            }

            if (cbxNhaCungCap.Text == "")
            {
                errorProvider1.SetError(cbxNhaCungCap, "Nhà cung cấp không được để trống");
                cbxNhaCungCap.Focus();
                return;
            }

            if (txtSoTien.Text == "")
            {
                errorProvider1.SetError(txtSoTien, "Số tiền không được để trống");
                txtSoTien.Focus();
                return;
            }

            if (isBtnThemClicked) // Add new row
            {
                row = ds.Tables[mainTableName].NewRow();

                FillRow(row);

                ds.Tables[mainTableName].Rows.Add(row);

                int result = DBHandler.Instance.Update(ds, mainTableName);

                if (result > 0)
                {
                    MessageBox.Show("Thêm thành công");
                    EnableComponents();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
            else // Update row
            {
                row.BeginEdit();

                FillRow(row);

                row.EndEdit();

                int result = DBHandler.Instance.Update(ds, mainTableName);

                if (result > 0)
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            row.Delete();

            int result = DBHandler.Instance.Update(ds, mainTableName);

            if (result > 0)
            {
                MessageBox.Show("Xóa thành công");
                EnableComponents();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(cbxNhanVien, "");
            errorProvider1.SetError(cbxNhaCungCap, "");
            errorProvider1.SetError(txtSoTien, "");
            txtMaPC.ResetText();
            cbxNhanVien.ResetText();
            cbxNhaCungCap.ResetText();
            dtpNgayTao.ResetText();
            txtSoTien.ResetText();
            EnableComponents();
        }

        private void dgvPhieuChi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = dgvPhieuChi.CurrentRow.Index;
            DisableFields();
            UpdateFields();
        }

        private void UpdateFields()
        {
            row = ds.Tables[mainTableName].Rows[pos];
            dgvPhieuChi.Rows[pos].Selected = true;
            txtMaPC.Text = row["MaPC"].ToString();
            cbxNhanVien.SelectedValue = row["MaNV"].ToString();
            cbxNhaCungCap.SelectedValue = row["MaNCC"].ToString();
            dtpNgayTao.Text = row["NgayTao"].ToString();
            txtSoTien.Text = row["SoTien"].ToString();
            lblCurrentRecord.Text = (pos + 1) + "/" + ds.Tables[mainTableName].Rows.Count;
        }

        private DataRow FillRow(DataRow row)
        {
            row["MaNV"] = cbxNhanVien.SelectedValue.ToString();
            row["MaNCC"] = cbxNhaCungCap.SelectedValue.ToString();
            row["NgayTao"] = dtpNgayTao.Text;
            row["SoTien"] = txtSoTien.Text;
            return row;
        }

        private void EnableFields()
        {
            cbxNhanVien.Enabled = true;
            cbxNhaCungCap.Enabled = true;
            dtpNgayTao.Enabled = true;
            txtSoTien.Enabled = true;
        }

        private void DisableFields()
        {
            cbxNhanVien.Enabled = false;
            cbxNhaCungCap.Enabled = false;
            dtpNgayTao.Enabled = false;
            txtSoTien.Enabled = false;
        }

        private void EnableComponents()
        {
            dgvPhieuChi.Enabled = true;
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnThem.Enabled = true;
            btnLuu.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void DisableComponents()
        {
            dgvPhieuChi.Enabled = false;
            btnFirst.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            btnThem.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = false;
        }
    }
}
