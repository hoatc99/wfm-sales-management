using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Winform_ThiCK_Nhom4
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtUsername, "");
            errorProvider1.SetError(txtPassword, "");

            if (txtUsername.Text == "")
            {
                errorProvider1.SetError(txtUsername, "Tên tài khoản không được để trống");
                txtUsername.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                errorProvider1.SetError(txtPassword, "Mật khẩu không được để trống");
                txtPassword.Focus();
                return;
            }

            string queryLogin = $"EXEC P_Login {txtUsername.Text}, {txtPassword.Text}";

            DataTable dt = DBHandler.Instance.Read(queryLogin);

            if (dt.Rows.Count == 1)
            {
                fMain fMain = new fMain(int.Parse(dt.Rows[0]["MaNV"].ToString()));
                Hide();
                fMain.ShowDialog();
                txtUsername.Clear();
                txtPassword.Clear();
                Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Hãy kiểm tra lại tài khoản hoặc mật khẩu");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát?", "Xác nhận thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
