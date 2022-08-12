using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_ThiCK_Nhom4.Forms
{
    public partial class fQuanLyNhanVien : Form
    {
        private string mainTableName = "NhanVien";

        private DataSet ds = new DataSet();

        private BindingSource bs;

        public fQuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void fQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM {mainTableName}";

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName));

            bs = new BindingSource(ds, mainTableName);

            dgvNhanVien.DataSource = bs;
            fMain.SetDataGridViewProperties(dgvNhanVien);

            txtMaNV.DataBindings.Add("Text", bs, "MaNV");
            txtTenNV.DataBindings.Add("Text", bs, "TenNV");
            dtpNgaySinh.DataBindings.Add("Text", bs, "NgaySinh");
            cbxGioiTinh.DataBindings.Add("Text", bs, "GioiTinh");
            cbxVaiTro.DataBindings.Add("Text", bs, "VaiTro");
            txtDiaChi.DataBindings.Add("Text", bs, "DiaChi");
            txtDienThoai.DataBindings.Add("Text", bs, "DienThoai");
            txtEmail.DataBindings.Add("Text", bs, "Email");
            lblCurrentRecord.Text = (bs.Position + 1) + "/" + bs.Count;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bs.Position = 0;
            lblCurrentRecord.Text = (bs.Position + 1) + "/" + bs.Count;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (bs.Position > 0) {
                bs.Position--;
            }
            lblCurrentRecord.Text = (bs.Position + 1) + "/" + bs.Count;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (bs.Position < bs.Count)
            {
                bs.Position++;
            }
            lblCurrentRecord.Text = (bs.Position + 1) + "/" + bs.Count;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bs.Position = bs.Count - 1;
            lblCurrentRecord.Text = (bs.Position + 1) + "/" + bs.Count;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            bs.AddNew();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtTenNV, "");
            errorProvider1.SetError(cbxGioiTinh, "");
            errorProvider1.SetError(cbxVaiTro, "");

            if (txtTenNV.Text == "")
            {
                errorProvider1.SetError(txtTenNV, "Tên nhân viên không được để trống");
                txtTenNV.Focus();
                return;
            }

            if (cbxGioiTinh.Text == "")
            {
                errorProvider1.SetError(cbxGioiTinh, "Giới tính không được để trống");
                cbxGioiTinh.Focus();
                return;
            }

            if (cbxVaiTro.Text == "")
            {
                errorProvider1.SetError(cbxVaiTro, "Vai trò không được để trống");
                cbxVaiTro.Focus();
                return;
            }

            bs.EndEdit();

            int result = DBHandler.Instance.Update(ds, mainTableName);

            if (result > 0)
            {
                MessageBox.Show("Lưu thành công");
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Lưu thất bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bs.RemoveCurrent();

            int result = DBHandler.Instance.Update(ds, mainTableName);

            if (result > 0)
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtTenNV, "");
            errorProvider1.SetError(cbxGioiTinh, "");
            errorProvider1.SetError(cbxVaiTro, "");
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblCurrentRecord.Text = (bs.Position + 1) + "/" + bs.Count;
        }
    }
}
