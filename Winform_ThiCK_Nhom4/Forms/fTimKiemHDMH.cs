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
    public partial class fTimKiemHDMH : Form
    {
        private string mainTableName = "HDMH";

        private DataSet ds = new DataSet();

        public fTimKiemHDMH()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            string query = "P_TimKiem_HDMH";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@mahdmh", txtMaHDMH.Text == "" ? (object)DBNull.Value : txtMaHDMH.Text));
            paramList.Add(new SqlParameter("@manv", txtMaNV.Text == "" ? (object)DBNull.Value : txtMaNV.Text));
            paramList.Add(new SqlParameter("@mancc", txtMaNCC.Text == "" ? (object)DBNull.Value : txtMaNCC.Text));
            paramList.Add(new SqlParameter("@sltu", txtSoLuongTu.Text == "" ? (object)DBNull.Value : txtSoLuongTu.Text));
            paramList.Add(new SqlParameter("@slden", txtSoLuongDen.Text == "" ? (object)DBNull.Value : txtSoLuongDen.Text));
            paramList.Add(new SqlParameter("@sttu", txtGiaTienTu.Text == "" ? (object)DBNull.Value : txtGiaTienTu.Text));
            paramList.Add(new SqlParameter("@stden", txtGiaTienDen.Text == "" ? (object)DBNull.Value : txtGiaTienDen.Text));

            if (ds.Tables.Contains(mainTableName))
            {
                ds.Tables.Remove(mainTableName);
            }

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName, paramList));

            dgvHDMH.DataSource = ds.Tables[mainTableName];
        }
        
        private void fTimKiemNhaCungCap_Load(object sender, EventArgs e)
        {
            GetData();
            fMain.SetDataGridViewProperties(dgvHDMH);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            fMain.HandleOpenReportViewer("rHDMH", ds.Tables[mainTableName]);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaHDMH.ResetText();
            txtMaNV.ResetText();
            txtMaNCC.ResetText();
            txtSoLuongTu.ResetText();
            txtSoLuongDen.ResetText();
            txtGiaTienTu.ResetText();
            txtGiaTienDen.ResetText();
            GetData();
        }
    }
}
