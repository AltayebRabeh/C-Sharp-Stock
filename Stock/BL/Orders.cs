using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class Orders
    {
        public static int Save(int cu_id, int d_id, int u_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `orders` (`cu_id`, `dr_id`, `u_id`) VALUES (" + cu_id + ", " + d_id + ", " + u_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int SaveDetails(int p_id, decimal qty, decimal min_qty, int or_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `order_details` (`p_id`, `qty`, `min_qty`, `or_id`) VALUES (" + p_id + ", " + qty + ", " + min_qty + ", " + or_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object GetLastOrderId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM orders", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }


        public static DataTable AllOrders(int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT o.id as 'المعرف', o.date as 'التاريخ',cu.name as 'إسم المورد', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM orders o "
                                                            + "JOIN customers cu ON cu.id = o.cu_id "
                                                            + "JOIN drivers dr on dr.id = o.dr_id "
                                                            + "JOIN users u on u.id = o.u_id WHERE inventory = " + inventory + " "
                                                            + "ORDER BY o.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable Search(string search, int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT o.id as 'المعرف', o.date as 'التاريخ',cu.name as 'إسم المورد', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM orders o "
                                                            + "JOIN customers cu ON cu.id = o.cu_id "
                                                            + "JOIN drivers dr on dr.id = o.dr_id "
                                                            + "JOIN users u on u.id = o.u_id WHERE inventory = " + inventory + " "
                                                            + "AND o.id LIKE '%" + search + "%' OR cu.name LIKE '%" + search + "%' OR dr.name LIKE '%" + search + "%' OR u.full_name LIKE '%" + search + "%' "
                                                            + "ORDER BY o.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable AllOrdersReback(int reback)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT o.id as 'المعرف', o.date as 'التاريخ',cu.name as 'إسم المورد', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM orders o "
                                                            + "JOIN customers cu ON cu.id = o.cu_id "
                                                            + "JOIN drivers dr on dr.id = o.dr_id "
                                                            + "JOIN users u on u.id = o.u_id "
                                                            + "WHERE inventory =0 AND  reback = " + reback + " ORDER BY o.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable OrdersDetails(int or_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'اسم المنتج', od.qty as 'الكمية', od.min_qty as 'الكمية الصغري', if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM order_details od "
                                                            + "JOIN products p ON p.id = od.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE od.or_id = " + or_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable AllOrderDetails(int o_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id, od.qty, od.min_qty, if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM order_details od "
                                                            + "JOIN products p ON p.id = od.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE od.or_id = " + o_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("DELETE FROM orders WHERE id = " + id + " ", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable ReOrdersDetails(int or_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id, re_or_d.qty, re_or_d.min_qty, if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM re_order_details re_or_d "
                                                            + "JOIN products p ON p.id = re_or_d.p_id "
                                                            + "JOIN re_orders re_o ON re_o.id = re_or_d.re_or_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE re_or_d.re_or_id = re_o.id"
                                                            + "AND re_o.or_id = " + or_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable OrderReport(int id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT o.id, o.date, d.name as 'driver_name', u.full_name as 'username', cu.name as 'customer_name', o.reback,"
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  od.qty, od.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM orders o "
                                                               + ", order_details od "
                                                               + ", products p "
                                                               + ", customers cu "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE od.or_id = o.id "
                                                               + "AND p.id = od.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = o.dr_id "
                                                               + "AND u.id = o.u_id "
                                                               + "AND o.id = " + id + " "
                                                               + "GROUP BY p.id, p.name, c.name, od.qty, od.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable OrdersReport()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT o.id, o.date, d.name as 'driver_name', u.full_name as 'username', cu.name as 'customer_name', o.reback,"
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  od.qty, od.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM orders o "
                                                               + ", order_details od "
                                                               + ", products p "
                                                               + ", customers cu "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE od.or_id = o.id "
                                                               + "AND p.id = od.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = o.dr_id "
                                                               + "AND u.id = o.u_id "
                                                               + "GROUP BY p.id, p.name, c.name, od.qty, od.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }
    }
}
