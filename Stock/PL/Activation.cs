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
    public partial class Activation : Form
    {
        public Activation()
        {
            InitializeComponent();
        }

        private void txt_serial_TextChanged(object sender, EventArgs e)
        {
            if (txt_serial.Text == Program.Serial)
            {
                btn_active.Enabled = true;
            }
            else
            {
                btn_active.Enabled = false;
            }
        }

        private void btn_active_Click(object sender, EventArgs e)
        {
            Program.activation();
            MessageBox.Show("");
            Login frm = new Login();
            frm.Show();
            this.Hide();
        }

        private void lbl_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
