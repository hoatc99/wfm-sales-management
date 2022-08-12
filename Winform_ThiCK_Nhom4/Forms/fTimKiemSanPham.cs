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
    public partial class fTimKiemSanPham : Form
    {
        private string mainTableName = "SanPham";

        private DataSet ds = new DataSet();

        public fTimKiemSanPham()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            string query = "P_TimKiem_SanPham";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@masp", txtMaSP.Text == "" ? (object)DBNull.Value : txtMaSP.Text));
            paramList.Add(new SqlParameter("@tensp", txtTenSP.Text == "" ? (object)DBNull.Value : txtTenSP.Text));
            paramList.Add(new SqlParameter("@donvitinh", txtDonViTinh.Text == "" ? (object)DBNull.Value : txtDonViTinh.Text));
            paramList.Add(new SqlParameter("@dgtu", txtDonGiaTu.Text == "" ? (object)DBNull.Value : txtDonGiaTu.Text));
            paramList.Add(new SqlParameter("@dgden", txtDonGiaDen.Text == "" ? (object)DBNull.Value : txtDonGiaDen.Text));
            paramList.Add(new SqlParameter("@kmtu", txtKhuyenMaiTu.Text == "" ? (object)DBNull.Value : txtKhuyenMaiTu.Text));
            paramList.Add(new SqlParameter("@kmden", txtKhuyenMaiDen.Text == "" ? (object)DBNull.Value : txtKhuyenMaiDen.Text));
            paramList.Add(new SqlParameter("@mancc", txtMaNCC.Text == "" ? (object)DBNull.Value : txtMaNCC.Text));

            if (ds.Tables.Contains(mainTableName))
            {
                ds.Tables.Remove(mainTableName);
            }

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName, paramList));

            dgvSanPham.DataSource = ds.Tables[mainTableName];
        }

        private void fTimKiemNhaCungCap_Load(object sender, EventArgs e)
        {
            GetData();
            fMain.SetDataGridViewProperties(dgvSanPham);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            fMain.HandleOpenReportViewer("rSanPham", ds.Tables[mainTableName]);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaSP.ResetText();
            txtTenSP.ResetText();
            txtDonViTinh.ResetText();
            txtDonGiaTu.ResetText();
            txtDonGiaDen.ResetText();
            txtKhuyenMaiTu.ResetText();
            txtKhuyenMaiDen.ResetText();
            txtMaNCC.ResetText();
            GetData();
        }
    }
}
