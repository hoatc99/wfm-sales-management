using System;
using System.Collections;
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
    public partial class fTimKiemNhanVien : Form
    {
        private string mainTableName = "NhanVien";

        private DataSet ds = new DataSet();

        public fTimKiemNhanVien()
        {
            InitializeComponent();
        }
        
        private void GetData()
        {
            string query = "P_TimKiem_NhanVien";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@manv", txtMaNV.Text == "" ? (object)DBNull.Value : txtMaNV.Text));
            paramList.Add(new SqlParameter("@tennv", txtTenNV.Text == "" ? (object)DBNull.Value : txtTenNV.Text));
            paramList.Add(new SqlParameter("@gioitinh", txtGioiTinh.Text == "" ? (object)DBNull.Value : txtGioiTinh.Text));
            paramList.Add(new SqlParameter("@vaitro", txtVaiTro.Text == "" ? (object)DBNull.Value : txtVaiTro.Text));
            paramList.Add(new SqlParameter("@diachi", txtDiaChi.Text == "" ? (object)DBNull.Value : txtDiaChi.Text));
            paramList.Add(new SqlParameter("@dienthoai", txtDienThoai.Text == "" ? (object)DBNull.Value : txtDienThoai.Text));
            paramList.Add(new SqlParameter("@email", txtEmail.Text == "" ? (object)DBNull.Value : txtEmail.Text));

            if (ds.Tables.Contains(mainTableName))
            {
                ds.Tables.Remove(mainTableName);
            }

            ds.Tables.Add(DBHandler.Instance.Read(query, mainTableName, paramList));
            
            dgvNhanVien.DataSource = ds.Tables[mainTableName];
        }

        private void fTimKiemNhaCungCap_Load(object sender, EventArgs e)
        {
            GetData();
            fMain.SetDataGridViewProperties(dgvNhanVien);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            fMain.HandleOpenReportViewer("rNhanVien", ds.Tables[mainTableName]);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtGioiTinh.ResetText();
            txtVaiTro.ResetText();
            txtDiaChi.ResetText();
            txtDienThoai.ResetText();
            txtEmail.ResetText();
            GetData();
        }
    }
}
