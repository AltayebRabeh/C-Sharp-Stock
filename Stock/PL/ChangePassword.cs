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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string old_pass = BL.Users.OldPass(Program.UserID).ToString();

            if (old_pass != txt_old_pass.Text)
            {
                MessageBox.Show("كلمة المرور غير صحيحة");
                txt_old_pass.Focus();
                return;
            }

            if (txt_password.Text.Length < 6)
            {
                MessageBox.Show("يجب إدخال كلمة مرور لاتقل عن 6 خانات");
                txt_password.Focus();
                return;
            }

            if (txt_password.Text != txt_repassword.Text)
            {
                MessageBox.Show("كلمات المرور غر متطابقة");
                txt_password.Focus();
                return;
            }

            BL.Users.UpdatePassword(txt_password.Text, Program.UserID);

            MessageBox.Show("تم التغيير بنجاح");
        }
    }
}
