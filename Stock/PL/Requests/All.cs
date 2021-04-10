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

namespace Stock.PL.Requests
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
                if (dr == DialogResult.Yes)
                {
                    //ReRqquest Add Qty
                    dt2 = BL.ReRequests.ReRequestsDetails(int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()));
                    dt = BL.Requests.AllRequestDetails(int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()));

                    // Check if product qty > this qty
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        decimal qty = Convert.ToDecimal(BL.Products.qty(Convert.ToInt32(dt.Rows[i][0].ToString())));
                        if (dt.Rows[i][3].ToString() != "")
                        {
                            if (qty < Convert.ToDecimal(dt.Rows[i][1].ToString()) * Convert.ToDecimal(dt.Rows[i][3].ToString())
                                + Convert.ToDecimal(dt.Rows[i][2].ToString()))
                            {
                                MessageBox.Show("الكميات في المخزن اقل من الكميات المراد حذفها");
                                return;
                            }
                        }
                        else
                        {
                            if (qty < Convert.ToDecimal(dt.Rows[i][1].ToString()))
                            {
                                MessageBox.Show("الكميات في المخزن اقل من الكميات المراد حذفها");
                                return;
                            }
                        }
                    }

                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt.Rows[i][3].ToString() != "")
                            {
                                BL.Products.AddQty(Convert.ToInt32(dt2.Rows[i][0].ToString()),
                                    Convert.ToDecimal(dt2.Rows[i][1].ToString()) * Convert.ToDecimal(dt2.Rows[i][3].ToString())
                                    + Convert.ToDecimal(dt2.Rows[i][2].ToString()));
                            }
                            else
                            {
                                BL.Products.AddQty(Convert.ToInt32(dt2.Rows[i][0].ToString()),
                                    Convert.ToDecimal(dt2.Rows[i][1].ToString()));
                            }
                        }
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][3].ToString() != "")
                        {
                            BL.Products.RemoveQty(Convert.ToInt32(dt.Rows[i][0].ToString()),
                                Convert.ToDecimal(dt.Rows[i][1].ToString()) * Convert.ToDecimal(dt.Rows[i][3].ToString())
                                + Convert.ToDecimal(dt.Rows[i][2].ToString()));
                        }
                        else
                        {
                            BL.Products.RemoveQty(Convert.ToInt32(dt.Rows[i][0].ToString()),
                                Convert.ToDecimal(dt.Rows[i][1].ToString()));
                        }
                    }
                    int count = BL.Requests.Delete(Convert.ToInt32(dgv.CurrentRow.Cells[0].Value.ToString()));
                    All_Activated(null, null);
                    MessageBox.Show("نم حذف عدد " + count + " من المشتريات");
                }
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

        }

        private void All_Activated(object sender, EventArgs e)
        {
            cb_inv_SelectedIndexChanged(null, null);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (cb_inv.Text == string.Empty)
            {
                dgv.DataSource = BL.Requests.Search(txt_search.Text);
            }
            else
            {
                dgv.DataSource = BL.Requests.Search(txt_search.Text, 1);
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.RequestReport rep = new Reports.RequestReport();
            frm.rep_veiw.DocumentSource = rep;
            rep.Parameters["parameter1"].Value = int.Parse(dgv.CurrentRow.Cells[0].Value.ToString());
            frm.ShowDialog();
        }

        private void cb_inv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_inv.Text == string.Empty)
            {
                dgv.DataSource = BL.Requests.AllRequests();
                btn_del.Enabled = true;
            }
            else
            {
                dgv.DataSource = BL.Requests.AllRequests(1);
                btn_del.Enabled = false;
            }
        }
    }
}
