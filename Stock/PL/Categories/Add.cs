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

namespace Stock.PL.Categories
{
    public partial class Add : Form
    {
        private bool IsUpdate;
        public int CatID;

        public Add(bool IsUpdate = false)
        {
            InitializeComponent();
            this.IsUpdate = IsUpdate;
            if (this.IsUpdate)
            {
                lbl_title.Text = " شاشة تعديل الصنف";
                this.Text = " شاشة تعديل الصنف";
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
                MessageBox.Show("الرجاءادخال اسم الصنف");
                txt_name.Focus();
                return;
            }

            if (!this.IsUpdate)
            {
                int i = BL.Categories.Save(txt_name.Text, rh_desc.Text);
                txt_name.Clear();
                rh_desc.Clear();
                MessageBox.Show("نم إدخال عدد " + i + " من الاصناف");
            }
            else
            {
                int i = BL.Categories.Update(txt_name.Text, rh_desc.Text, CatID);
                MessageBox.Show("نم تعديل عدد " + i + " من الاصناف");
            }
        }
    }
}
