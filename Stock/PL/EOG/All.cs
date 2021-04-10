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

namespace Stock.PL.EOG
{
    public partial class All : Form
    {
        public All()
        {
            InitializeComponent();
            All_Activated(null, null);
        }

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("هل تريد الحذف ؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dr == DialogResult.Yes) {

                    // Return reorder QTY
                    dt2 = BL.ReOrders.ReOrdersDetails(int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()));
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i][3].ToString() != "")
                            {
                                BL.Products.RemoveQty(Convert.ToInt32(dt2.Rows[i][0].ToString()),
                                    Convert.ToDecimal(dt2.Rows[i][1].ToString()) * Convert.ToDecimal(dt2.Rows[i][3].ToString())
                                    + Convert.ToDecimal(dt2.Rows[i][2].ToString()));
                            }
                            else
                            {
                                BL.Products.RemoveQty(Convert.ToInt32(dt2.Rows[i][0].ToString()),
                                    Convert.ToDecimal(dt2.Rows[i][1].ToString()));
                            }
                        }
                    }

                    dt = BL.Orders.AllOrderDetails(int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][3].ToString() != "")
                        {
                            BL.Products.AddQty(Convert.ToInt32(dt.Rows[i][0].ToString()),
                                Convert.ToDecimal(dt.Rows[i][1].ToString()) * Convert.ToDecimal(dt.Rows[i][3].ToString())
                                + Convert.ToDecimal(dt.Rows[i][2].ToString()));
                        }
                        else
                        {
                            BL.Products.AddQty(Convert.ToInt32(dt.Rows[i][0].ToString()),
                                Convert.ToDecimal(dt.Rows[i][1].ToString()));
                        }
                    }
                    int count = BL.Orders.Delete(Convert.ToInt32(dgv.CurrentRow.Cells[0].Value.ToString()));
                    All_Activated(null, null);
                    MessageBox.Show("نم حذف عدد " + count + " من المبيعات");
                }
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void All_Activated(object sender, EventArgs e)
        {
            cb_inv_SelectedIndexChanged(null, null);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (cb_inv.Text == string.Empty)
            {
                dgv.DataSource = BL.EOG.Search(txt_search.Text);
            }
            else
            {
                dgv.DataSource = BL.EOG.Search(txt_search.Text, 1);
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.EOGReport rep = new Reports.EOGReport();
            frm.rep_veiw.DocumentSource = rep;
            rep.Parameters["parameter1"].Value = int.Parse(dgv.CurrentRow.Cells[0].Value.ToString());
            frm.ShowDialog();
        }

        private void cb_inv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_inv.Text == string.Empty)
            {
                dgv.DataSource = BL.EOG.AllEOG();
                btn_del.Enabled = true;
            }
            else
            {
                dgv.DataSource = BL.EOG.AllEOG(1);
                btn_del.Enabled = false;
            }
        }
    }
}
