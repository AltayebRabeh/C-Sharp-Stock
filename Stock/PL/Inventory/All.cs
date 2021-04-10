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

namespace Stock.PL.Inventory
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
                    if (dgv.CurrentRow.Cells[5].Value.ToString() != "جرد عادي")
                    {
                        MessageBox.Show("لايمكن حذف الجرد النهائي");
                        return;
                    }
                    int i = BL.Inventory.Delete(int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()));
                    All_Activated(null, null);
                    MessageBox.Show("نم الحذف بنجاح ");
                }
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void All_Activated(object sender, EventArgs e)
        {
            dgv.DataSource = BL.Inventory.All();
        }

        private void All_Load(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.InventoryReport rep = new Reports.InventoryReport();
            frm.rep_veiw.DocumentSource = rep;
            rep.Parameters["parameter1"].Value = int.Parse(dgv.CurrentRow.Cells[0].Value.ToString());
            frm.ShowDialog();
        }
    }
}
