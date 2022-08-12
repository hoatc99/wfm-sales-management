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
    public partial class fQuanLySanPham : Form
    {
        private string mainTableName = "SanPham";

        private DataSet ds = new DataSet();

        private DataRow row;

        int pos = 0;

        bool isBtnThemClicked = false;

        public fQuanLySanPham()
        {
            InitializeComponent();
        }

        private void fQuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            string queryNCC = "SELECT MaNCC, TenNCC FROM NhaCungCap";

            ds.Tables.Add(DBHandler.Instance.Read(queryNCC, "NhaCungCap"));

            cbxNhaCungCap.DataSource = ds.Tables["NhaCungCap"];
            cbxNhaCungCap.DisplayMember = "TenNCC";
            cbxNhaCungCap.ValueMember = "MaNCC";

            string query = $"SELECT * FROM {mainTableName}";
            
            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName));

            dgvSanPham.DataSource = ds.Tables[mainTableName];
            fMain.SetDataGridViewProperties(dgvSanPham);
            fMain.HandleCurrencyFormattingForDataGridViewColumn(dgvSanPham.Columns[3]);

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
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            isBtnThemClicked = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtTenSP, "");
            errorProvider1.SetError(cbxDonViTinh, "");
            errorProvider1.SetError(txtDonGia, "");
            errorProvider1.SetError(txtKhuyenMai, "");
            errorProvider1.SetError(cbxNhaCungCap, "");

            if (txtTenSP.Text == "")
            {
                errorProvider1.SetError(txtTenSP, "Tên sản phẩm không được để trống");
                txtTenSP.Focus();
                return;
            }

            if (cbxDonViTinh.Text == "")
            {
                errorProvider1.SetError(cbxDonViTinh, "Đơn vị tính không được để trống");
                cbxDonViTinh.Focus();
                return;
            }

            if (txtDonGia.Text == "")
            {
                errorProvider1.SetError(txtDonGia, "Đơn giá không được để trống");
                txtDonGia.Focus();
                return;
            }

            if (txtKhuyenMai.Text == "")
            {
                errorProvider1.SetError(txtKhuyenMai, "Chiết khấu không được để trống");
                txtKhuyenMai.Focus();
                return;
            }

            if (cbxNhaCungCap.Text == "")
            {
                errorProvider1.SetError(cbxNhaCungCap, "Nhà cung cấp không được để trống");
                cbxNhaCungCap.Focus();
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
            errorProvider1.SetError(txtTenSP, "");
            errorProvider1.SetError(cbxDonViTinh, "");
            errorProvider1.SetError(txtDonGia, "");
            errorProvider1.SetError(txtKhuyenMai, "");
            errorProvider1.SetError(cbxNhaCungCap, "");
            txtMaSP.ResetText();
            txtTenSP.ResetText();
            cbxDonViTinh.ResetText();
            txtDonGia.ResetText();
            txtKhuyenMai.ResetText();
            cbxNhaCungCap.ResetText();
            EnableComponents();
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = dgvSanPham.CurrentRow.Index;
            UpdateFields();
        }

        private void UpdateFields()
        {
            row = ds.Tables[mainTableName].Rows[pos];
            dgvSanPham.Rows[pos].Selected = true;
            txtMaSP.Text = row["MaSP"].ToString();
            txtTenSP.Text = row["TenSP"].ToString();
            cbxDonViTinh.Text = row["DonViTinh"].ToString();
            txtDonGia.Text = row["DonGia"].ToString();
            txtKhuyenMai.Text = row["KhuyenMai"].ToString();
            cbxNhaCungCap.SelectedValue = row["MaNCC"].ToString();
            lblCurrentRecord.Text = (pos + 1) + "/" + ds.Tables[mainTableName].Rows.Count;
        }

        private DataRow FillRow(DataRow row)
        {
            row["TenSP"] = txtTenSP.Text;
            row["DonViTinh"] = cbxDonViTinh.Text;
            row["DonGia"] = txtDonGia.Text;
            row["KhuyenMai"] = txtKhuyenMai.Text;
            row["MaNCC"] = cbxNhaCungCap.SelectedValue.ToString();
            return row;
        }

        private void EnableComponents()
        {
            dgvSanPham.Enabled = true;
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
            dgvSanPham.Enabled = false;
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
