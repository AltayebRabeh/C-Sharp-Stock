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
    public partial class Add : Form
    {
        public int CatID;

        public Add()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_add_product_Click(object sender, EventArgs e)
        {
            lbl_qty.Text = "الكمية بــ ";
            lbl_min_qty.Text = "الكمية بــ ";

            Choose frm = new Choose("products");
            frm.ShowDialog();
            txt_p_id.Text = frm.dgv.CurrentRow.Cells[0].Value.ToString();
            txt_p_name.Text = frm.dgv.CurrentRow.Cells[1].Value.ToString();
            lbl_qty.Text += frm.dgv.CurrentRow.Cells[3].Value.ToString();
            txt_min_qty.Text = string.Empty;
            txt_qty.Text = string.Empty;
            if (frm.dgv.CurrentRow.Cells[4].Value.ToString() != string.Empty)
            {
                lbl_min_qty.Text += frm.dgv.CurrentRow.Cells[4].Value.ToString();
                txt_min_qty.Enabled = true;
            }
            else
            {
                txt_min_qty.Enabled = false;
                txt_min_qty.Text = string.Empty;
                lbl_min_qty.Text = "الكمية بــ ";
            }
            txt_qty.Focus();
        }

        public decimal small_unit;
        public decimal total_qty;
        public decimal t_qty;
        public decimal t_min_qty;
        private void txt_min_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.small_unit = 0;
                this.total_qty = 0;
                t_min_qty = 0;
                t_qty = 0;
                if (txt_p_id.Text == string.Empty && txt_p_name.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء إختيار المنتج");
                    return;
                }

                if ((txt_qty.Text == string.Empty || Convert.ToDecimal(txt_qty.Text) <= 0) && (txt_min_qty.Text == string.Empty || Convert.ToDecimal(txt_min_qty.Text) <= 0))
                {
                    MessageBox.Show("الرجاء إدخال الكمية");
                    txt_qty.Focus();
                    return;
                }



                decimal qty = Convert.ToDecimal(BL.Products.qty(int.Parse(txt_p_id.Text)));
                int unit_id = Convert.ToInt32(BL.Products.unit_id(int.Parse(txt_p_id.Text)));

                object _small_unit = BL.Units.small_unit(unit_id);


                if (_small_unit.GetType() != typeof(DBNull))
                {
                    small_unit = Convert.ToInt32(_small_unit);

                    if (txt_qty.Text != string.Empty)
                    {
                        if (t_min_qty > small_unit - 1)
                        {
                            MessageBox.Show("لقد ادخلت كمية اكبر من المتوقع");
                            txt_min_qty.Focus();
                            return;
                        }
                        this.total_qty = (Convert.ToDecimal(txt_qty.Text) * small_unit);
                        this.t_qty = Convert.ToDecimal(txt_qty.Text);
                    }
                    else
                    {
                        this.t_qty = 0;
                    }
                    if (txt_min_qty.Text != string.Empty)
                    {
                        this.total_qty += Convert.ToDecimal(txt_min_qty.Text);
                        this.t_min_qty = Convert.ToDecimal(txt_min_qty.Text);
                        if (t_min_qty > small_unit - 1)
                        {
                            MessageBox.Show("لقد ادخلت كمية اكبر من المتوقع");
                            txt_min_qty.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.t_min_qty = 0;
                    }

                    if (qty < this.total_qty)
                    {
                        MessageBox.Show("الكمية غير كافية");
                        txt_qty.Focus();
                        return;
                    }
                }
                else
                {
                    if (qty < Convert.ToDecimal(txt_qty.Text))
                    {
                        MessageBox.Show("الكمية غير كافية");
                        txt_qty.Focus();
                        return;
                    }
                    this.t_min_qty = 0;
                    this.t_qty = Convert.ToDecimal(txt_qty.Text);
                }


                if (dgv.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (dgv.Rows[i].Cells[0].Value.ToString() == txt_p_id.Text.ToString())
                        {
                            MessageBox.Show("المنتج مدخل مسبقا");
                            dgv.Rows[i].Selected = true;
                            return;
                        }
                    }
                    dgv.Rows.Add(txt_p_id.Text, txt_p_name.Text, t_qty, t_min_qty, small_unit);
                }
                else
                {
                    dgv.Rows.Add(txt_p_id.Text, txt_p_name.Text, t_qty, t_min_qty, small_unit);
                    btn_save.Enabled = btn_print.Enabled = true;
                }
                txt_p_id.Text = string.Empty;
                txt_p_name.Text = string.Empty;
                txt_min_qty.Text = string.Empty;
                txt_qty.Text = string.Empty;
                lbl_min_qty.Text = "الكمية بــ ";
                lbl_qty.Text = "الكمية بــ ";


                dgv.Columns[0].Width = btn_add_product.Width + txt_p_id.Width - 2;
                dgv.Columns[1].Width = txt_p_name.Width - 1;
                dgv.Columns[2].Width = txt_qty.Width - 1;
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Remove(dgv.CurrentRow);
                if (dgv.Rows.Count == 0)
                {
                    btn_save.Enabled = btn_print.Enabled = false;
                }
                    
            }
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txt_p_id.Text != string.Empty)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void btn_close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        int eog_id;
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("الفاتورة فارغة");
                return;
            } 

            BL.EOG.Save(txt_desc.Text, Program.UserID);
            eog_id = Convert.ToInt32(BL.EOG.GetLastEOGId());

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (int.Parse(dgv.Rows[i].Cells[4].Value.ToString()) == 0)
                {
                    BL.EOG.SaveDetails(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()), 0, eog_id);
                    BL.Products.RemoveQty(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()));
                }
                else
                {
                    BL.EOG.SaveDetails(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[3].Value.ToString()), eog_id);
                    BL.Products.RemoveQty(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()),
                        Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()) * Convert.ToDecimal(dgv.Rows[i].Cells[4].Value.ToString()) + Convert.ToDecimal(dgv.Rows[i].Cells[3].Value.ToString())
                        );
                }
            }

            MessageBox.Show("تم حفظ الفاتورة بنجاح");

            btn_save.Enabled = false;
            btn_print.Enabled = true;
            btn_print_Click(null, null);
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows.Remove(dgv.Rows[i]);
            }
            txt_p_id.Text = txt_p_name.Text = txt_qty.Text = txt_min_qty.Text = string.Empty;
            lbl_min_qty.Text = "الكمية بــ ";
            lbl_qty.Text = "الكمية بــ ";
            
            btn_save.Enabled = btn_print.Enabled = false;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.EOGReport rep = new Reports.EOGReport();
            frm.rep_veiw.DocumentSource = rep;
            rep.Parameters["parameter1"].Value = eog_id;
            frm.ShowDialog();
        }
    }
}
