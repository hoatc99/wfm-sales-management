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
    public partial class fInBaoCao : Form
    {
        public fInBaoCao()
        {
            InitializeComponent();
        }

        private void rfNhaCungCap_Load(object sender, EventArgs e)
        {
            this.rvBaoCao.RefreshReport();
        }
    }
}
