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

namespace Stock.PL.Suppliers
{
    public partial class Add : Form
    {
        private bool IsUpdate;
        public int S_ID;

        public Add(bool IsUpdate = false)
        {
            InitializeComponent();
            this.IsUpdate = IsUpdate;
            if (this.IsUpdate)
            {
                lbl_title.Text = " شاشة تعديل المورد";
                this.Text = " شاشة تعديل المورد";
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == string.Empty)
            {
                MessageBox.Show("الرجاءادخال اسم العميل");
                txt_name.Focus();
                return;
            }


            if (!this.IsUpdate)
            {
                int i = BL.Suppliers.Save(txt_name.Text, txt_address.Text, txt_phone.Text);
                txt_name.Clear();
                txt_address.Clear();
                txt_phone.Clear();
                MessageBox.Show("نم إدخال عدد " + i + " من العملاء");
            }
            else
            {
                int i = BL.Suppliers.Update(txt_name.Text, txt_address.Text, txt_phone.Text, S_ID);
                MessageBox.Show("نم تعديل عدد " + i + " من العملاء");
            }
        }
    }
}
