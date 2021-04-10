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

namespace Stock.PL.ReOrders
{
    public partial class Add : Form
    {
        private bool IsUpdate;
        public int CatID;

        public Add(bool IsUpdate = false)
        {
            InitializeComponent();
            this.IsUpdate = IsUpdate;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_choose_su_Click(object sender, EventArgs e)
        {
            Choose frm = new Choose("orders_reback");
            frm.ShowDialog();
            if (frm.dgv.Rows.Count > 0)
            {
                txt_or_id.Text = frm.dgv.CurrentRow.Cells[0].Value.ToString();
                txt_re_date.Text = frm.dgv.CurrentRow.Cells[1].Value.ToString();
                cu_name.Text = frm.dgv.CurrentRow.Cells[2].Value.ToString();
                txt_driver_name.Text = frm.dgv.CurrentRow.Cells[3].Value.ToString();
                txt_driver_name.Enabled = txt_re_date.Enabled = cu_name.Enabled = false;
            }
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows.Remove(dgv.Rows[i]);
            }
            txt_p_id.Text = txt_p_name.Text = string.Empty;
        }

        private void btn_add_product_Click(object sender, EventArgs e)
        {
            lbl_qty.Text = "الكمية بــ ";
            lbl_min_qty.Text = "الكمية بــ ";

            if (txt_or_id.Text == string.Empty)
            {
                MessageBox.Show("الرجاء اختيار الفاتورة");
                return;
            }

            Choose frm = new Choose("orders_details", txt_or_id.Text);
            frm.ShowDialog();
            txt_p_id.Text = frm.dgv.CurrentRow.Cells[0].Value.ToString();
            txt_p_name.Text = frm.dgv.CurrentRow.Cells[1].Value.ToString();

            this.qty = 0;
            if (frm.dgv.CurrentRow.Cells[4].Value.ToString() != string.Empty)
            {
                this.qty = (Convert.ToDecimal(frm.dgv.CurrentRow.Cells[2].Value.ToString()) * Convert.ToDecimal(frm.dgv.CurrentRow.Cells[4].Value.ToString())) + Convert.ToDecimal(frm.dgv.CurrentRow.Cells[3].Value.ToString());
            }
            else
            {
                this.qty = Convert.ToDecimal(frm.dgv.CurrentRow.Cells[2].Value.ToString());
            }

            txt_min_qty.Text = string.Empty;
            txt_qty.Text = string.Empty;
            if (frm.dgv.CurrentRow.Cells[4].Value.ToString() != string.Empty)
            {
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
        public decimal qty;
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

                }
                else
                {
                    this.t_min_qty = 0;
                    this.t_qty = Convert.ToDecimal(txt_qty.Text);
                    this.total_qty = Convert.ToDecimal(txt_qty.Text);
                }

                if (this.qty < this.total_qty)
                {
                    MessageBox.Show("الكمية المدخلة اكثر من الكمية في الفاتورة");
                    txt_min_qty.Focus();
                    return;
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

        private void btn_new_su_Click(object sender, EventArgs e)
        {
            txt_driver_name.Text = txt_re_date.Text = txt_or_id.Text = cu_name.Text = string.Empty;
            txt_driver_name.Enabled = txt_re_date.Enabled = cu_name.Enabled = true;
        }

        private void btn_choose_driver_Click(object sender, EventArgs e)
        {
            Choose frm = new Choose("drivers");
            frm.ShowDialog();
            if (frm.dgv.Rows.Count > 0)
            {
                txt_d_id.Text = frm.dgv.CurrentRow.Cells[0].Value.ToString();
                txt_d_name.Text = frm.dgv.CurrentRow.Cells[1].Value.ToString();
                txt_d_natnum.Text = frm.dgv.CurrentRow.Cells[2].Value.ToString();
                txt_car_type.Text = frm.dgv.CurrentRow.Cells[3].Value.ToString();
                txt_car_num.Text = frm.dgv.CurrentRow.Cells[4].Value.ToString();
                txt_car_num.Enabled = txt_d_name.Enabled = txt_d_natnum.Enabled = txt_car_type.Enabled = false;
            }
        }

        private void btn_new_driver_Click(object sender, EventArgs e)
        {
            txt_car_num.Text = txt_d_name.Text = txt_d_id.Text = txt_d_natnum.Text = txt_car_type.Text = string.Empty;
            txt_car_num.Enabled = txt_d_name.Enabled = txt_d_natnum.Enabled = txt_car_type.Enabled = true;
        }

        private void btn_close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        int or_id;
        int d_id;
        int re_or_id;
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("الفاتورة فارغة");
                return;
            }

            if(txt_or_id.Text == string.Empty)
            {
                MessageBox.Show("الرجاء إختيار الفاتورة");
                return;
            }
            or_id = Convert.ToInt32(txt_or_id.Text);
            
            if (txt_d_id.Text == string.Empty)
            {
                if (txt_d_name.Text == string.Empty && txt_d_natnum.Text == string.Empty && txt_car_type.Text == string.Empty && txt_car_num.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء إدخال بيانات السائق");
                    return;
                }
                else
                {
                    BL.Drivers.Save(txt_d_name.Text, txt_d_natnum.Text, txt_car_type.Text, txt_car_num.Text);
                    d_id = Convert.ToInt32(BL.Drivers.GetLastDriverId());
                }
            } 
            else 
            {
                d_id = Convert.ToInt32(txt_d_id.Text);
            }

            BL.ReOrders.Save(d_id, Program.UserID, or_id);
            re_or_id = Convert.ToInt32(BL.ReOrders.GetLastReOrderId());

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (int.Parse(dgv.Rows[i].Cells[4].Value.ToString()) == 0)
                {
                    BL.ReOrders.SaveDetails(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()), 0, re_or_id);
                    BL.Products.AddQty(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()));
                }
                else
                {
                    BL.ReOrders.SaveDetails(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()), Convert.ToDecimal(dgv.Rows[i].Cells[3].Value.ToString()), re_or_id);
                    BL.Products.AddQty(Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString()),
                        Convert.ToDecimal(dgv.Rows[i].Cells[2].Value.ToString()) * Convert.ToDecimal(dgv.Rows[i].Cells[4].Value.ToString()) + Convert.ToDecimal(dgv.Rows[i].Cells[3].Value.ToString())
                        );
                }
            }

            BL.ReOrders.ReBack(or_id, 1);
            MessageBox.Show("تم حفظ الفاتورة بنجاح");
            btn_print_Click(null, null);

            btn_save.Enabled = false;
            btn_print.Enabled = true;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows.Remove(dgv.Rows[i]);
            }
            txt_p_id.Text = txt_p_name.Text = txt_qty.Text = txt_min_qty.Text =
                txt_or_id.Text = txt_re_date.Text = cu_name.Text = txt_driver_name.Text =
                txt_d_id.Text = txt_d_name.Text = txt_d_natnum.Text = txt_car_type.Text = txt_car_num.Text = string.Empty;
            lbl_min_qty.Text = "الكمية بــ ";
            lbl_qty.Text = "الكمية بــ ";
            txt_or_id.Enabled = txt_re_date.Enabled = cu_name.Enabled = txt_driver_name.Enabled =
                txt_d_id.Enabled = txt_d_name.Enabled = txt_d_natnum.Enabled = txt_car_type.Enabled = txt_car_num.Enabled = false;
            btn_save.Enabled = btn_print.Enabled = false;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.ReOrderReport rep = new Reports.ReOrderReport();
            frm.rep_veiw.DocumentSource = rep;
            rep.Parameters["parameter1"].Value = re_or_id;
            frm.ShowDialog();
        }
    }
}
