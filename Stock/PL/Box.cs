using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock.PL
{
    public partial class Box : Form
    {
        public string search;
        public Box(string search)
        {
            InitializeComponent();
            this.search = search;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (search == "order")
            {
                Reports.RepostView frm = new Reports.RepostView();
                Reports.OrderReport rep = new Reports.OrderReport();
                frm.rep_veiw.DocumentSource = rep;
                rep.Parameters["parameter1"].Value = txt_search.Text;
                frm.ShowDialog();
            } 
            else if (search == "request")
            {
                Reports.RepostView frm = new Reports.RepostView();
                Reports.RequestReport rep = new Reports.RequestReport();
                frm.rep_veiw.DocumentSource = rep;
                rep.Parameters["parameter1"].Value = txt_search.Text;
                frm.ShowDialog();
            }
            else if (search == "re_order")
            {
                Reports.RepostView frm = new Reports.RepostView();
                Reports.ReOrderReport rep = new Reports.ReOrderReport();
                frm.rep_veiw.DocumentSource = rep;
                rep.Parameters["parameter1"].Value = txt_search.Text;
                frm.ShowDialog();
            }
            else if (search == "re_request")
            {
                Reports.RepostView frm = new Reports.RepostView();
                Reports.ReRequestReport rep = new Reports.ReRequestReport();
                frm.rep_veiw.DocumentSource = rep;
                rep.Parameters["parameter1"].Value = txt_search.Text;
                frm.ShowDialog();
            }

            this.Close();
        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ok_Click(null, null);
            }
        }

        private void lbl_exit_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
