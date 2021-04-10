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

namespace Stock.PL
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Users_Activated(object sender, EventArgs e)
        {
            
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dgv.DataSource = BL.Users.Search(txt_search.Text);
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                txt_id.Text = dgv.CurrentRow.Cells[0].Value.ToString();
                txt_name.Text = dgv.CurrentRow.Cells[1].Value.ToString();
                txt_username.Text = dgv.CurrentRow.Cells[2].Value.ToString();
                cb_per.Text = dgv.CurrentRow.Cells[3].Value.ToString();
                if (dgv.CurrentRow.Cells[4].Value.ToString() == "مفعل")
                {
                    check_active.Checked = true;
                }
                else
                {
                    check_active.Checked = false;
                }

                txt_password.Enabled = txt_repassword.Enabled = txt_username.Enabled = false;
                
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_password.Enabled = txt_repassword.Enabled = txt_username.Enabled = true;
            txt_id.Text = txt_name.Text = txt_username.Text = string.Empty;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_username.Text.Length < 3)
            {
                MessageBox.Show("الرجاء إدخال اسم مستخدم لايقل عن 3");
                txt_username.Focus();
                return;
            }

            if (txt_name.Text.Length < 10)
            {
                MessageBox.Show("الرجاء إدخال الاسم الكامل و لايقل عن 10");
                txt_name.Focus();
                return;
            }

            if (cb_per.Text == string.Empty)
            {
                MessageBox.Show("الرجاء إختيار الصلاحية");
                cb_per.Focus();
                return;
            }

            if (txt_password.Text != txt_repassword.Text)
            {
                MessageBox.Show("كلمات المرور غر متطابقة");
                txt_password.Focus();
                return;
            }

            if (txt_id.Text == string.Empty)
            {
                if (txt_password.Text.Length < 6)
                {
                    MessageBox.Show("يجب إدخال كلمة مرور لاتقل عن 6 خانات");
                    txt_password.Focus();
                    return;
                }
                DataTable dt = BL.Users.CheckUsername(txt_username.Text);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("إسم مستخدم غير صالح");
                    return;
                }
                
                int active = 0;
                if (check_active.Checked == true)
                    active = 1;

                BL.Users.Save(txt_name.Text, txt_username.Text, txt_password.Text, cb_per.Text, active);
                Users_Load(null, null);
            }
            else
            {
                if (dgv.CurrentRow.Cells[2].Value.ToString() != txt_username.Text)
                {
                    DataTable dt = BL.Users.CheckUsername(txt_username.Text);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("إسم مستخدم غير صالح");
                        return;
                    }
                }
                int active = 1;
                if (check_active.Checked != true)
                {
                    if (Convert.ToInt32(dgv.CurrentRow.Cells[0].Value.ToString()) == Program.UserID)
                    {
                        return;
                    }
                    active = 0;
                }

                BL.Users.Update(txt_name.Text, cb_per.Text, active, Convert.ToInt32(txt_id.Text));
                Users_Load(null, null);
            }

            MessageBox.Show("تم الحفظ بنجاح");
        }

        private void Users_Load(object sender, EventArgs e)
        {
            dgv.DataSource = BL.Users.All();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                if (Convert.ToInt32(dgv.CurrentRow.Cells[0].Value.ToString()) == Program.UserID)
                {
                    return;
                }
                BL.Users.Delete(Convert.ToInt32(dgv.CurrentRow.Cells[0].Value.ToString()));
                Users_Load(null, null);
                MessageBox.Show("تم الحذف بنجاح");
            }
        }
    }
}
