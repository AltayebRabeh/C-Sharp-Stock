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
using System.Diagnostics;

namespace Stock.PL
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Process proc = Process.Start(@"C:\xampp\mysql_start.bat");
            
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == string.Empty)
            {
                MessageBox.Show("الرجاءادخال اسم المستخدم");
                txt_username.Focus();
                return;
            }
            if (txt_password.Text == string.Empty)
            {
                MessageBox.Show("الرجاءادخال كلمة المرور");
                txt_password.Focus();
                return;
            }
            DataTable dt = BL.Login.LoginUser(txt_username.Text, txt_password.Text);

            if (dt.Rows.Count > 0)
            {
                Program.UserID = int.Parse(dt.Rows[0][0].ToString());
                Program.UserPermission = dt.Rows[0][4].ToString();
                Main frm = new Main();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("إسم المستخدم او كلمة المرور خطأ");
            }
        }

        private void txt_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login_Click(null, null);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
