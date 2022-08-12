using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_ThiCK_Nhom4
{
    class ButtonHandler
    {
        private BindingSource bs = new BindingSource();

        private Label lblCurrentRecord = new Label();

        private Button btnFirst = new Button();
        private Button btnPrevious = new Button();
        private Button btnNext = new Button();
        private Button btnLast = new Button();
        private Button btnThem = new Button();
        private Button btnLuu = new Button();
        private Button btnXoa = new Button();
        private Button btnHuy = new Button();

        public BindingSource Bs { get => bs; set => bs = value; }
        public Label LblCurrentRecord { get => lblCurrentRecord; set => lblCurrentRecord = value; }
        public Button BtnFirst { get => btnFirst; set => btnFirst = value; }
        public Button BtnPrevious { get => btnPrevious; set => btnPrevious = value; }
        public Button BtnNext { get => btnNext; set => btnNext = value; }
        public Button BtnLast { get => btnLast; set => btnLast = value; }
        public Button BtnThem { get => btnThem; set => btnThem = value; }
        public Button BtnLuu { get => btnLuu; set => btnLuu = value; }
        public Button BtnXoa { get => btnXoa; set => btnXoa = value; }
        public Button BtnHuy { get => btnHuy; set => btnHuy = value; }

        public ButtonHandler(BindingSource bindingSource)
        {
            Bs = bindingSource;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Bs.Position = 0;
            LblCurrentRecord.Text = (Bs.Position + 1) + "/" + Bs.Count;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Bs.Position > 0)
            {
                Bs.Position--;
            }
            LblCurrentRecord.Text = (Bs.Position + 1) + "/" + Bs.Count;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Bs.Position < Bs.Count)
            {
                Bs.Position++;
            }
            LblCurrentRecord.Text = (Bs.Position + 1) + "/" + Bs.Count;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Bs.Position = Bs.Count - 1;
            LblCurrentRecord.Text = (Bs.Position + 1) + "/" + Bs.Count;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            BtnThem.Enabled = false;
            BtnXoa.Enabled = false;
            Bs.AddNew();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Bs.EndEdit();

            //int result = DBHandler.Instance.Update(mainTableName);

            //if (result > 0)
            //{
            //    MessageBox.Show("Lưu thành công");
            //}
            //else
            //{
            //    MessageBox.Show("Lưu thất bại");
            //}
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Bs.RemoveCurrent();

            //int result = DBHandler.Instance.Update(mainTableName);

            //if (result > 0)
            //{
            //    MessageBox.Show("Xóa thành công");
            //}
            //else
            //{
            //    MessageBox.Show("Xóa thất bại");
            //}
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            BtnFirst.Enabled = true;
            BtnPrevious.Enabled = true;
            BtnNext.Enabled = true;
            BtnLast.Enabled = true;
            BtnThem.Enabled = true;
            BtnLuu.Enabled = true;
            BtnXoa.Enabled = true;
            BtnHuy.Enabled = true;
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LblCurrentRecord.Text = (Bs.Position + 1) + "/" + Bs.Count;
        }
    }
}
