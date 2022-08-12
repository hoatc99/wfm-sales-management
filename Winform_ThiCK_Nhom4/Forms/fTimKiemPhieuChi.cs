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
    public partial class fTimKiemPhieuChi : Form
    {
        private string mainTableName = "PhieuChi";

        private DataSet ds = new DataSet();

        public fTimKiemPhieuChi()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            string query = "P_TimKiem_PhieuChi";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@mapc", txtMaPC.Text == "" ? (object)DBNull.Value : txtMaPC.Text));
            paramList.Add(new SqlParameter("@manv", txtMaNV.Text == "" ? (object)DBNull.Value : txtMaNV.Text));
            paramList.Add(new SqlParameter("@mancc", txtMaNCC.Text == "" ? (object)DBNull.Value : txtMaNCC.Text));
            paramList.Add(new SqlParameter("@sttu", txtSoTienTu.Text == "" ? (object)DBNull.Value : txtSoTienTu.Text));
            paramList.Add(new SqlParameter("@stden", txtSoTienDen.Text == "" ? (object)DBNull.Value : txtSoTienDen.Text));

            if (ds.Tables.Contains(mainTableName))
            {
                ds.Tables.Remove(mainTableName);
            }

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName, paramList));

            dgvPhieuChi.DataSource = ds.Tables[mainTableName];
        }

        private void fTimKiemNhaCungCap_Load(object sender, EventArgs e)
        {
            GetData();
            fMain.SetDataGridViewProperties(dgvPhieuChi);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            fMain.HandleOpenReportViewer("rPhieuChi", ds.Tables[mainTableName]);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPC.ResetText();
            txtMaNV.ResetText();
            txtMaNCC.ResetText();
            txtSoTienTu.ResetText();
            txtSoTienDen.ResetText();
            GetData();
        }
    }
}
