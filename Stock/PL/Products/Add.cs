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
                lbl_title.Text = " شاشة تعديل المنتج";
                this.Text = " شاشة تعديل المنتج";
            }

            cb_cat.DataSource = BL.Categories.All();
            cb_cat.DisplayMember = "إسم الصنف";
            cb_cat.ValueMember = "المعرف";
            cb_cat.SelectedIndex = -1;

            cb_unit.DataSource = BL.Units.All();
            cb_unit.DisplayMember = "إسم وحدة القياس";
            cb_unit.ValueMember = "المعرف";
            cb_unit.SelectedIndex = -1;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (cb_cat.Text == string.Empty)
            {
                MessageBox.Show("الرجاءإختيار الصنف");
                cb_cat.Focus();
                return;
            }

            if (txt_name.Text == string.Empty)
            {
                MessageBox.Show("الرجاءادخال اسم المنتج");
                txt_name.Focus();
                return;
            }

            if (cb_unit.Text == string.Empty)
            {
                MessageBox.Show("الرجاءإختيار وحدة القياس");
                cb_unit.Focus();
                return;
            }

            int status = 1;
            int floating = 1;

            if (! check_status.Checked)
            {
                status = 0;
            }

            if (!this.IsUpdate)
            {
                int i = BL.Products.Save(txt_name.Text, rh_desc.Text, status, Convert.ToInt32(cb_unit.SelectedValue), Convert.ToInt32(cb_cat.SelectedValue), Program.UserID);
                txt_name.Clear();
                rh_desc.Clear();
                MessageBox.Show("نم إدخال عدد " + i + " من المنتجات");
            }
            else
            {
                int i = BL.Products.Update(P_ID, txt_name.Text, rh_desc.Text, status, Convert.ToInt32(cb_unit.SelectedValue), Convert.ToInt32(cb_cat.SelectedValue), Program.UserID);
                MessageBox.Show("نم تعديل عدد " + i + " من المنتجات");
            }
        }
    }
}
