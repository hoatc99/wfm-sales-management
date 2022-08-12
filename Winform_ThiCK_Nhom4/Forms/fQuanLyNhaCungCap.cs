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
    public partial class fQuanLyNhaCungCap : Form
    {
        private string mainTableName = "NhaCungCap";

        private DataSet ds = new DataSet();

        private BindingSource bs;

        public fQuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private void fQuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM {mainTableName}";

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName));

            bs = new BindingSource(ds, mainTableName);

            dgvNhaCungCap.DataSource = bs;
            fMain.SetDataGridViewProperties(dgvNhaCungCap);

            txtMaNCC.DataBindings.Add("Text", bs, "MaNCC");
            txtTenNCC.DataBindings.Add("Text", bs, "TenNCC");
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
            txtTenNCC.Focus();
            bs.AddNew();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtTenNCC, "");
            if (txtTenNCC.Text == "")
            {
                errorProvider1.SetError(txtTenNCC, "Tên nhà cung cấp không được để trống");
                txtTenNCC.Focus();
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
            errorProvider1.SetError(txtTenNCC, "");
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblCurrentRecord.Text = (bs.Position + 1) + "/" + bs.Count;
        }
    }
}
