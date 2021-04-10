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

namespace Stock.PL.Unit
{
    public partial class Add : Form
    {
        private bool IsUpdate;
        public int P_ID;

        public Add(bool IsUpdate = false)
        {
            InitializeComponent();
            this.IsUpdate = IsUpdate;
            if (this.IsUpdate)
            {
                lbl_title.Text = " شاشة تعديل وحدة قياس";
                this.Text = " شاشة تعديل وحدة قياس";
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_unit_name.Text == string.Empty)
            {
                MessageBox.Show("الرجاءادخال اسم الوحدة");
                txt_unit_name.Focus();
                return;
            }

            if (txt_small_unit.Text != string.Empty)
            {
                if (txt_total_qty.Text == string.Empty)
                {
                    MessageBox.Show("الرجاءادخال عدد الوحدات بالحبة");
                    txt_total_qty.Focus();
                    return;
                }
            }

            if (!this.IsUpdate)
            {
                if (txt_total_qty.Text != string.Empty)
                {
                    int i = BL.Units.Save(txt_unit_name.Text, txt_small_unit.Text, int.Parse(txt_total_qty.Text));
                    txt_unit_name.Clear();
                    txt_small_unit.Clear();
                    txt_total_qty.Clear();
                    MessageBox.Show("نم إدخال عدد " + i + " من الوحداة");
                }
                else
                {
                    int i = BL.Units.SaveOne(txt_unit_name.Text);
                    txt_unit_name.Clear();
                    txt_small_unit.Clear();
                    txt_total_qty.Clear();
                    MessageBox.Show("نم إدخال عدد " + i + " من الوحداة");
                }
            }
            else
            {
                if (txt_total_qty.Text != string.Empty)
                {
                    int i = BL.Units.Update(txt_unit_name.Text, txt_small_unit.Text, int.Parse(txt_total_qty.Text), P_ID);
                    MessageBox.Show("نم تعديل عدد " + i + " من الوحداة");
                }
                else
                {
                    int i = BL.Units.UpdateOne(txt_unit_name.Text, P_ID);
                    MessageBox.Show("نم تعديل عدد " + i + " من الوحداة");
                }
            }
        }

        private void txt_total_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
