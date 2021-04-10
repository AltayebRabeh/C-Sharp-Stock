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
    public partial class Choose : Form
    {
        string search_type;
        public Choose(string search_type, string search = null)
        {
            InitializeComponent();
            this.search_type = search_type;
            if (search_type == "products")
            {
                dgv.DataSource = BL.Products.AllProductsActive();
            }
            else if (search_type == "customers")
            {
                dgv.DataSource = BL.Customers.All();
            }
            else if (search_type == "suppliers")
            {
                dgv.DataSource = BL.Suppliers.All();
            }
            else if (search_type == "drivers")
            {
                dgv.DataSource = BL.Drivers.All();
            }
            else if (search_type == "requests")
            {
                dgv.DataSource = BL.Requests.AllRequests();
            }
            else if (search_type == "requests_reback")
            {
                dgv.DataSource = BL.Requests.AllRequestsReback(0);
            }
            else if (search_type == "requests_details")
            {
                dgv.DataSource = BL.Requests.RequestsDetails(Convert.ToInt32(search));
                dgv.Columns[4].Visible = true;
            }
            else if (search_type == "orders")
            {
                dgv.DataSource = BL.Orders.AllOrders();
            }
            else if (search_type == "orders_reback")
            {
                dgv.DataSource = BL.Orders.AllOrdersReback(0);
            }
            else if (search_type == "orders_details")
            {
                dgv.DataSource = BL.Orders.OrdersDetails(Convert.ToInt32(search));
                dgv.Columns[4].Visible = true;
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (search_type == "products")
            {
                dgv.DataSource = BL.Products.SearchProductsActive(txt_search.Text);
            }
            else if (search_type == "customers")
            {
                dgv.DataSource = BL.Customers.Search(txt_search.Text);
            }
            else if (search_type == "suppliers")
            {
                dgv.DataSource = BL.Suppliers.Search(txt_search.Text);
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
        }

        private void Choose_Load(object sender, EventArgs e)
        {

        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

    }
}
