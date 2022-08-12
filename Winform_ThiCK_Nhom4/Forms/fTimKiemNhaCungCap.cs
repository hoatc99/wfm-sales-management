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
using Winform_ThiCK_Nhom4.Forms;

namespace Winform_ThiCK_Nhom4.Forms
{
    public partial class fTimKiemNhaCungCap : Form
    {
        private string mainTableName = "NhaCungCap";

        private DataSet ds = new DataSet();

        public fTimKiemNhaCungCap()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            string query = "P_TimKiem_NhaCungCap";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@mancc", txtMaNCC.Text == "" ? (object)DBNull.Value : txtMaNCC.Text));
            paramList.Add(new SqlParameter("@tenncc", txtTenNCC.Text == "" ? (object)DBNull.Value : txtTenNCC.Text));
            paramList.Add(new SqlParameter("@diachi", txtDiaChi.Text == "" ? (object)DBNull.Value : txtDiaChi.Text));
            paramList.Add(new SqlParameter("@dienthoai", txtDienThoai.Text == "" ? (object)DBNull.Value : txtDienThoai.Text));
            paramList.Add(new SqlParameter("@email", txtEmail.Text == "" ? (object)DBNull.Value : txtEmail.Text));

            if (ds.Tables.Contains(mainTableName))
            {
                ds.Tables.Remove(mainTableName);
            }

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName, paramList));

            dgvNhaCungCap.DataSource = ds.Tables[mainTableName];
        }
        
        private void fTimKiemNhaCungCap_Load(object sender, EventArgs e)
        {
            GetData();
            fMain.SetDataGridViewProperties(dgvNhaCungCap);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            fMain.HandleOpenReportViewer("rNhaCungCap", ds.Tables[mainTableName]);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaNCC.ResetText();
            txtTenNCC.ResetText();
            txtDiaChi.ResetText();
            txtDienThoai.ResetText();
            txtEmail.ResetText();
            GetData();
        }
    }
}
