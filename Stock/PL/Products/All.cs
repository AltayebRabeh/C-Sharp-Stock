using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stock.BL;

namespace Stock.PL.Products
{
    public partial class All : Form
    {
        public All()
        {
            InitializeComponent();
            All_Activated(null, null);
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dr == DialogResult.Yes) {
                    int i = BL.Products.Delete(int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()));
                    All_Activated(null, null);
                    MessageBox.Show("نم حذف عدد " + i + " من المنتجات");
                }
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Add frm = new Add(true);
            frm.MdiParent = this.MdiParent;
            frm.P_ID = int.Parse(dgv.CurrentRow.Cells[0].Value.ToString());
            frm.txt_name.Text = dgv.CurrentRow.Cells[1].Value.ToString();
            frm.cb_cat.Text = dgv.CurrentRow.Cells[2].Value.ToString();
            frm.rh_desc.Text = dgv.CurrentRow.Cells[3].Value.ToString();
            if (dgv.CurrentRow.Cells[5].Value.ToString() == "مفعل")
            {
                frm.check_status.Checked = true;
            }
            else
            {
                frm.check_status.Checked = false;
            }

            frm.cb_unit.Text = dgv.CurrentRow.Cells[8].Value.ToString();

            frm.Show();
        }

        private void All_Activated(object sender, EventArgs e)
        {
            dgv.DataSource = BL.Products.All();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (txt_search.Text == "مفعل")
            {
                dgv.DataSource = BL.Products.SearchActivation("1");
            }
            else if (txt_search.Text == "غير مفعل")
            {
                dgv.DataSource = BL.Products.SearchActivation("0");
            } else {
                dgv.DataSource = BL.Products.Search(txt_search.Text);
            }
        }
    }
}
