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
    public partial class fQuanLyHDMH : Form
    {
        private string mainTableName = "HDMH";

        private DataSet ds = new DataSet();

        private DataRow row;

        int pos = 0;

        public fQuanLyHDMH()
        {
            InitializeComponent();
        }

        private void fQuanLyHDMH_Load(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM {mainTableName}";

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName));

            dgvHoaDon.DataSource = ds.Tables[mainTableName];

            fMain.SetDataGridViewProperties(dgvHoaDon);

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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            row.Delete();

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

        private void UpdateFields()
        {
            row = ds.Tables[mainTableName].Rows[pos];
            dgvHoaDon.Rows[pos].Selected = true;
            lblCurrentRecord.Text = (pos + 1) + "/" + ds.Tables[mainTableName].Rows.Count;

            txtMaHD.Text = row["MaHDMH"].ToString();

            string queryNCC = $"SELECT TenNCC FROM NhaCungCap where MaNCC = {row["MaNCC"].ToString()}";

            txtTenNCC.Text = DBHandler.Instance.Read(queryNCC).Rows[0][0].ToString();

            string queryNV = $"SELECT TenNV FROM NhanVien where MaNV = {row["MaNV"].ToString()}";

            txtTenNV.Text = DBHandler.Instance.Read(queryNV).Rows[0][0].ToString();

            string queryCTHDMH = $"SELECT * FROM CTHDMH where MaHDMH = {txtMaHD.Text}";

            dgvChiTietHD.DataSource = DBHandler.Instance.Read(queryCTHDMH, "CTHDMH");

            fMain.SetDataGridViewProperties(dgvChiTietHD);

            long tongGiaTri = 0;
            long tongChietKhau = 0;
            int tongSoLuong = 0;
            long tongTienPhaiTT = 0;

            if (dgvChiTietHD.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvChiTietHD.Rows)
                {
                    int SL = int.Parse(row.Cells[3].Value.ToString());
                    int DG = int.Parse(row.Cells[4].Value.ToString());
                    int CK = int.Parse(row.Cells[5].Value.ToString());
                    int CP = int.Parse(row.Cells[6].Value.ToString());
                    tongGiaTri += SL * DG + CP;
                    tongChietKhau += SL * DG * CK / 100;
                    tongSoLuong += SL;
                }
                tongTienPhaiTT = tongGiaTri - tongChietKhau;
            }

            txtTongGiaTri.Text = tongGiaTri.ToString();
            txtTongChietKhau.Text = tongChietKhau.ToString();
            txtTongSoLuong.Text = tongSoLuong.ToString();
            txtTongPhaiTra.Text = tongTienPhaiTT.ToString();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = dgvHoaDon.CurrentRow.Index;
            UpdateFields();
        }
    }
}
