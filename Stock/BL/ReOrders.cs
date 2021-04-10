using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class ReOrders
    {
        public static int Save(int d_id, int u_id, int or_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `re_orders` (`dr_id`, `u_id`, `or_id`) VALUES (" + d_id + ", " + u_id + ", " + or_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int SaveDetails(int p_id, decimal qty, decimal min_qty, int re_or_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `re_order_details` (`p_id`, `qty`, `min_qty`, `re_or_id`) VALUES (" + p_id + ", " + qty + ", " + min_qty + ", '" + re_or_id + "')", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int ReBack(int or_id, int reback)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE `orders` set `reback` = " + reback + " WHERE id = " + or_id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable AllReOrders(int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_or.id as 'المعرف', re_or.date as 'التاريخ', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "o.id as 'رقم الفاتورة', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM re_orders re_or "
                                                            + "JOIN orders o ON o.id = re_or.or_id "
                                                            + "JOIN drivers dr on dr.id = re_or.dr_id "
                                                            + "JOIN users u on u.id = re_or.u_id WHERE  o.inventory = " + inventory + " "
                                                            + "ORDER BY re_or.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable Search(string search, int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_or.id as 'المعرف', re_or.date as 'التاريخ', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "o.id as 'رقم الفاتورة', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM re_orders re_or "
                                                            + "JOIN orders o ON o.id = re_or.or_id "
                                                            + "JOIN drivers dr on dr.id = re_or.dr_id "
                                                            + "JOIN users u on u.id = re_or.u_id WHERE  o.inventory = " + inventory + " "
                                                            + "AND re_or.id LIKE '%" + search + "%' OR o.id LIKE '%" + search + "%' OR dr.name LIKE '%" + search + "%' OR u.full_name LIKE '%" + search + "%' "
                                                            + "ORDER BY re_or.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable ReOrdersDetails(int or_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id, re_or_d.qty, re_or_d.min_qty, if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM re_order_details re_or_d "
                                                            + "JOIN products p ON p.id = re_or_d.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE re_or_d.re_or_id = " + or_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("DELETE FROM re_orders WHERE id = " + id + " ", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object GetLastReOrderId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM re_orders", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static DataTable ReOrderReport(int id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_o.id, re_o.or_id, re_o.date, d.name as 'driver_name', u.full_name as 'username', "
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  re_od.qty, re_od.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM re_orders re_o "
                                                               + ", re_order_details re_od "
                                                               + ", products p "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE re_od.re_or_id = re_o.id "
                                                               + "AND p.id = re_od.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = re_o.dr_id "
                                                               + "AND u.id = re_o.u_id "
                                                               + "AND re_o.id = " + id + " "
                                                               + "GROUP BY p.id, p.name, c.name, re_od.qty, re_od.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable ReOrdersReport()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_o.id, re_o.or_id, re_o.date, d.name as 'driver_name', u.full_name as 'username', "
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  re_od.qty, re_od.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM re_orders re_o "
                                                               + ", re_order_details re_od "
                                                               + ", products p "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE re_od.re_or_id = re_o.id "
                                                               + "AND p.id = re_od.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = re_o.dr_id "
                                                               + "AND u.id = re_o.u_id "
                                                               + "GROUP BY p.id, p.name, c.name, re_od.qty, re_od.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }
    }
}
