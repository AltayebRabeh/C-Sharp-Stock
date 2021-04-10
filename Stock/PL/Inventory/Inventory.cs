using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock.PL.Inventory
{
    public partial class Inventory : Form
    {
        int inv_id;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        public Inventory()
        {
            InitializeComponent();
            cb_type.SelectedIndex = 0;
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_type.Text != "جرد عادي")
            {
                dtp_from.Value = DateTime.Now;
                dtp_to.Value = DateTime.Now;
                dtp_from.Enabled = false;
                dtp_to.Enabled = false;
            }
            else
            {
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            if (cb_type.Text != "جرد عادي")
            {
                DialogResult d = MessageBox.Show("هل انت متاكد من انكـ تريد جرد نهائي وترحيل", null, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.No == d)
                {
                    return;
                }
            }


            if ((dtp_from.Value - dtp_to.Value).Days > 0)
            {
                MessageBox.Show("التاريخ الاول اكبر من الثاني");
                return;
            }

            mergeTables();
            if (this.dt.Rows.Count < 1)
            {
                MessageBox.Show("لا يوجد بيانات لجردها");
                return;
            }

            if (cb_type.Text != "جرد عادي")
            {
                if (BL.Inventory.GetLastInventoryDate().GetType() != typeof(DBNull))
                {
                    DateTime date = Convert.ToDateTime(BL.Inventory.GetLastInventoryDate());
                    if ((DateTime.Now - date).Days < 90)
                    {
                        MessageBox.Show("لا يمكنك جرد المخزن في اقل من ثلاث اشهر");
                        return;
                    }
                }
            }

            int type = cb_type.Text != "جرد عادي" ? 1 : 0;

            BL.Inventory.Save(dtp_from.Value.ToString("yyyy-MM-dd"), dtp_to.Value.ToString("yyyy-MM-dd"), rh_desc.Text, type, Program.UserID);
            this.inv_id = Convert.ToInt32(BL.Inventory.GetLastInventoryId());
            for (int i = 0; i < this.dt.Rows.Count; i++)
            {
                BL.Inventory.SaveDetails(
                        Convert.ToInt32(this.dt.Rows[i][0].ToString()),
                        this.dt.Rows[i][1].GetType() == typeof(DBNull) ? 0 : Convert.ToDecimal(this.dt.Rows[i][1].ToString()),
                        this.dt.Rows[i][3].GetType() == typeof(DBNull) ? 0 : Convert.ToDecimal(this.dt.Rows[i][3].ToString()),
                        this.dt.Rows[i][4].GetType() == typeof(DBNull) ? 0 : Convert.ToDecimal(this.dt.Rows[i][4].ToString()),
                        this.dt.Rows[i][2].GetType() == typeof(DBNull) ? 0 : Convert.ToDecimal(this.dt.Rows[i][2].ToString()),
                        this.inv_id
                    );
            }

            if (cb_type.Text != "جرد عادي")
            {
                BL.Inventory.UpdateData();
            }

            MessageBox.Show("تم الجرد بنجاح");
            btn_print_Click(null, null);
            btn_save.Enabled = false;
            btn_print.Enabled = true;
        }

        void mergeTables()
        {
            this.dt.Clear();
            this.dt2.Clear();
            this.dt3.Clear();

            if (cb_type.Text == "جرد عادي")
            {
                this.dt = BL.Inventory.getAllRequests(dtp_from.Value.ToString("yyyy-MM-dd"), dtp_to.Value.ToString("yyyy-MM-dd"));
                this.dt2 = BL.Inventory.getAllOrders(dtp_from.Value.ToString("yyyy-MM-dd"), dtp_to.Value.ToString("yyyy-MM-dd"));
                this.dt3 = BL.Inventory.getAllEOG(dtp_from.Value.ToString("yyyy-MM-dd"), dtp_to.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                this.dt = BL.Inventory.getAllRequests();
                this.dt2 = BL.Inventory.getAllOrders();
                this.dt3 = BL.Inventory.getAllEOG();
            }

            if (dt.Rows.Count > 0)
            {
                this.dt.Columns.Add("out_qty", typeof(decimal));
                this.dt.Columns.Add("lose_qty", typeof(decimal));

                for (int i = 0; i < this.dt.Rows.Count; i++)
                {
                    for (int n = 0; n < this.dt2.Rows.Count; n++)
                    {
                        if (int.Parse(this.dt.Rows[i][0].ToString()) == int.Parse(this.dt2.Rows[n][0].ToString()))
                        {
                            this.dt.Rows[i][3] = this.dt2.Rows[n][1];
                        }
                    }

                    for (int x = 0; x < dt3.Rows.Count; x++)
                    {
                        if (int.Parse(this.dt.Rows[i][0].ToString()) == int.Parse(this.dt3.Rows[x][0].ToString()))
                        {
                            this.dt.Rows[i][4] = this.dt3.Rows[x][1];
                        }
                    }
                }
            }
            else
            {
                dt = dt2;
                DataColumn dc = this.dt.Columns.Add("in_qty", typeof(decimal));
                dc.SetOrdinal(1);
                this.dt.Columns.Add("lose_qty", typeof(decimal));
                dt.Columns[3].SetOrdinal(2);

                for (int i = 0; i < this.dt.Rows.Count; i++)
                {
                    for (int x = 0; x < dt3.Rows.Count; x++)
                    {
                        if (int.Parse(this.dt.Rows[i][0].ToString()) == int.Parse(this.dt3.Rows[x][0].ToString()))
                        {
                            this.dt.Rows[i][4] = this.dt3.Rows[x][1];
                        }
                    }
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = true;
            btn_print.Enabled = false;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.InventoryReport rep = new Reports.InventoryReport();
            frm.rep_veiw.DocumentSource = rep;
            rep.Parameters["parameter1"].Value = inv_id;
            frm.ShowDialog();
        }
    }
}
