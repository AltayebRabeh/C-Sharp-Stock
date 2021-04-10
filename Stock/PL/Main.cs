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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {
                    client.BackColor = Color.White;
                    break;
                }
            }

            lbl_user_name.Text = BL.Users.GetUserFullname(Program.UserID).ToString();
            session_date.Text = DateTime.Now.ToString();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_add_category_Click(object sender, EventArgs e)
        {
            Categories.Add frm = new Categories.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_categories_Click(object sender, EventArgs e)
        {
            Categories.All frm = new Categories.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_unit_Click(object sender, EventArgs e)
        {
            Unit.Add frm = new Unit.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_product_Click(object sender, EventArgs e)
        {
            Products.Add frm = new Products.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_product_Click(object sender, EventArgs e)
        {
            Products.All frm = new Products.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_units_Click(object sender, EventArgs e)
        {
            Unit.All frm = new Unit.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_customer_Click(object sender, EventArgs e)
        {
            Customers.Add frm = new Customers.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_customers_Click(object sender, EventArgs e)
        {
            Customers.All frm = new Customers.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_request_Click(object sender, EventArgs e)
        {
            Requests.Add frm = new Requests.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_requests_Click(object sender, EventArgs e)
        {
            Requests.All frm = new Requests.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_order_Click(object sender, EventArgs e)
        {
            Orders.Add frm = new Orders.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_re_request_Click(object sender, EventArgs e)
        {
            ReRequests.Add frm = new ReRequests.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_re_order_Click(object sender, EventArgs e)
        {
            ReOrders.Add frm = new ReOrders.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_orders_Click(object sender, EventArgs e)
        {
            Orders.All frm = new Orders.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_re_request_Click(object sender, EventArgs e)
        {
            ReRequests.All frm = new ReRequests.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_re_orders_Click(object sender, EventArgs e)
        {
            ReOrders.All frm = new ReOrders.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_eog_Click(object sender, EventArgs e)
        {
            EOG.Add frm = new  EOG.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_eog_Click(object sender, EventArgs e)
        {
            EOG.All frm = new EOG.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_inventory_Click(object sender, EventArgs e)
        {
            Inventory.Inventory frm = new Inventory.Inventory();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_add_supplier_Click(object sender, EventArgs e)
        {
            Suppliers.Add frm = new Suppliers.Add();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_all_suppliers_Click(object sender, EventArgs e)
        {
            Suppliers.All frm = new Suppliers.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login frm = new Login();
            frm.Show();
        }

        private void btn_users_Click(object sender, EventArgs e)
        {
            Users frm = new Users();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btn_change_pass_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword();
            frm.ShowDialog();
        }

        private void btn_backup_Click(object sender, EventArgs e)
        {
            PL.Backup frm = new PL.Backup();
            frm.ShowDialog();
        }

        private void btn_all_inventory_Click(object sender, EventArgs e)
        {
            Inventory.All frm = new Inventory.All();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsm_request_Click(object sender, EventArgs e)
        {
            Box frm = new Box("request");
            frm.ShowDialog();
        }

        private void tsm_all_requests_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.RequestsReport rep = new Reports.RequestsReport();
            frm.rep_veiw.DocumentSource = rep;
            frm.ShowDialog();
        }

        private void tsm_order_Click(object sender, EventArgs e)
        {
            Box frm = new Box("order");
            frm.ShowDialog();
        }

        private void tsm_all_orders_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.OrdersReport rep = new Reports.OrdersReport();
            frm.rep_veiw.DocumentSource = rep;
            frm.ShowDialog();
        }

        private void tsm_all_re_orders_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.ReOrdersReport rep = new Reports.ReOrdersReport();
            frm.rep_veiw.DocumentSource = rep;
            frm.ShowDialog();
        }

        private void tsm_re_order_Click(object sender, EventArgs e)
        {
            Box frm = new Box("re_order");
            frm.ShowDialog();
        }

        private void tsm_all_re_requests_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.ReRequestsReport rep = new Reports.ReRequestsReport();
            frm.rep_veiw.DocumentSource = rep;
            frm.ShowDialog();
        }

        private void tsm_re_request_Click(object sender, EventArgs e)
        {
            Box frm = new Box("re_request");
            frm.ShowDialog();
        }

        private void tsm_products_Click(object sender, EventArgs e)
        {
            Reports.RepostView frm = new Reports.RepostView();
            Reports.ProductsReport rep = new Reports.ProductsReport();
            frm.rep_veiw.DocumentSource = rep;
            frm.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //مدير
            //مساعد مدير
            //مدير مبيعات
            //مدير مشتريات
            //موظف مبيعات
            //موظف مشاريات
            //مستخرج تقارير

            if (Program.UserPermission == "مدير")
            {
                btn_backup.Enabled = btn_restore.Enabled = btn_users.Enabled = btn_add_category.Enabled = btn_all_categories.Enabled =
                btn_add_product.Enabled = btn_all_product.Enabled = btn_add_unit.Enabled = btn_all_units.Enabled =
                btn_add_supplier.Enabled = btn_all_suppliers.Enabled = btn_add_customer.Enabled = btn_all_customers.Enabled =
                btn_add_order.Enabled = btn_all_orders.Enabled = btn_add_re_order.Enabled = btn_all_re_orders.Enabled =
                btn_add_request.Enabled = btn_all_requests.Enabled = btn_add_re_request.Enabled = btn_all_re_request.Enabled =
                btn_eog.Enabled = btn_all_eog.Enabled = btn_inventory.Enabled = btn_all_inventory.Enabled = reports.Enabled = true;
            }
            else if (Program.UserPermission == "مساعد مدير")
            {
                btn_backup.Enabled = btn_add_category.Enabled = btn_all_categories.Enabled =
                btn_add_product.Enabled = btn_all_product.Enabled = btn_add_unit.Enabled = btn_all_units.Enabled =
                btn_add_supplier.Enabled = btn_all_suppliers.Enabled = btn_add_customer.Enabled = btn_all_customers.Enabled =
                btn_add_order.Enabled = btn_all_orders.Enabled = btn_add_re_order.Enabled = btn_all_re_orders.Enabled =
                btn_add_request.Enabled = btn_all_requests.Enabled = btn_add_re_request.Enabled = btn_all_re_request.Enabled =
                btn_eog.Enabled = btn_all_eog.Enabled = btn_inventory.Enabled = btn_all_inventory.Enabled = reports.Enabled = true;
            }
            else if (Program.UserPermission == "مدير مبيعات")
            {
                btn_add_order.Enabled = btn_all_orders.Enabled = btn_add_re_order.Enabled = btn_all_re_orders.Enabled = true;
            }
            else if (Program.UserPermission == "مدير مشتريات")
            {
                btn_add_request.Enabled = btn_all_requests.Enabled = btn_add_re_request.Enabled = btn_all_re_request.Enabled = true;
            }
            else if (Program.UserPermission == "موظف مبيعات")
            {
                btn_add_order.Enabled = true;
            }
            else if (Program.UserPermission == "موظف مشاريات")
            {
                btn_add_request.Enabled = true;
            }
            else if (Program.UserPermission == "مستخرج تقارير")
            {
                reports.Enabled = true;
            }
        }

    }
}
