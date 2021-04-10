using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Stock.DAL;

namespace Stock.BL
{
    class Inventory
    {
        public static int Save(string start_date, string end_date, string description, int type , int u_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `inventory` (`start_date`, `end_date`, `description`, `type`, `user_id`) VALUES ('" + start_date + "', '" + end_date + "', '" + description + "'," + type + ", " + u_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int SaveDetails(int p_id, decimal in_qty, decimal out_qty, decimal lose_qty, decimal qty, int in_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `inventory_details` (`p_id`, `in_qty`, `out_qty`, `lose_qty`, `qty`, `in_id`) VALUES (" + p_id + ", " + in_qty + ", " + out_qty + ", " + lose_qty + ", " + qty + ", " + in_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object GetLastInventoryId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM inventory", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static object GetLastInventoryDate()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(date) FROM inventory WHERE type = 1", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static DataTable All()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , date as 'التاريخ' , start_date as 'من' , end_date as 'الي', description as 'ملاحظات' , if(type = 0, 'جرد عادي', 'جرد نهائي وترحيل') as 'نوع الجرد' FROM inventory ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("DELETE FROM `inventory` WHERE id = " + id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static void UpdateData()
        {
            DataAccessLayer.Open();
            DataAccessLayer.ExecuteNonQuery("UPDATE `eog` set `inventory` = 1", CommandType.Text);
            DataAccessLayer.ExecuteNonQuery("UPDATE `requests` set `inventory` = 1", CommandType.Text);
            DataAccessLayer.ExecuteNonQuery("UPDATE `orders` set `inventory` = 1", CommandType.Text);
            DataAccessLayer.Close();
        }

        public static DataTable getAllRequests(string from, string to)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id,"
                                                                    + " CONVERT( "
                                                                    + " if(u.small_unit = '',  SUM(rd.qty) - if(re_rd.qty IS NULL, 0, SUM(re_rd.qty)), "
                                                                    + " (SUM(rd.qty) * u.total_qty + SUM(rd.min_qty) ) - "
                                                                    + "if(SUM(re_rd.qty) IS NULL, 0 + if(re_rd.min_qty IS NULL, 0, SUM(re_rd.min_qty)),  "
                                                                    + " SUM(re_rd.qty) + if(re_rd.min_qty IS NULL, 0, SUM(re_rd.min_qty))) "
                                                                    + " ) , DECIMAL(9,2)) AS in_qty , "
                                                                    + " p.qty"
                                                                    + " FROM "
                                                                    + " requests r "
                                                                    + " JOIN request_details rd on rd.re_id = r.id"
                                                                    + " left JOIN re_requests re_r on re_r.re_id = r.id"
                                                                    + " left JOIN re_request_details re_rd on re_rd.re_re_id = re_r.id"
                                                                    + " JOIN products p on p.id = rd.p_id"
                                                                    + " JOIN categories c on c.id = p.cat_id"
                                                                    + " JOIN units u on u.id = p.unit_id"
                                                                    + " WHERE r.inventory = 0"
                                                                    + " AND (r.date BETWEEN '" + from + "' AND '" + to + "')"
                                                                    + " GROUP By"
                                                                    + " p.id"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable getAllRequests()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id,"
                                                                    + " CONVERT( "
                                                                    + " if(u.small_unit = '',  SUM(rd.qty) - if(re_rd.qty IS NULL, 0, SUM(re_rd.qty)), "
                                                                    + " (SUM(rd.qty) * u.total_qty + SUM(rd.min_qty) ) - "
                                                                    + "if(SUM(re_rd.qty) IS NULL, 0 + if(re_rd.min_qty IS NULL, 0, SUM(re_rd.min_qty)),  "
                                                                    + " SUM(re_rd.qty) + if(re_rd.min_qty IS NULL, 0, SUM(re_rd.min_qty))) "
                                                                    + " ) , DECIMAL(9,2)) AS in_qty, "
                                                                    + " p.qty"
                                                                    + " FROM "
                                                                    + " requests r "
                                                                    + " JOIN request_details rd on rd.re_id = r.id"
                                                                    + " left JOIN re_requests re_r on re_r.re_id = r.id"
                                                                    + " left JOIN re_request_details re_rd on re_rd.re_re_id = re_r.id"
                                                                    + " JOIN products p on p.id = rd.p_id"
                                                                    + " JOIN categories c on c.id = p.cat_id"
                                                                    + " JOIN units u on u.id = p.unit_id"
                                                                    + " WHERE r.inventory = 0"
                                                                    + " GROUP By"
                                                                    + " p.id"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable getAllOrders(string from, string to)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id,"
                                                                    + " CONVERT( "
                                                                    + " if(u.small_unit = '',  SUM(od.qty) - if(re_od.qty IS NULL, 0, SUM(re_od.qty)), "
                                                                    + " (SUM(od.qty) * u.total_qty + SUM(od.min_qty) ) - "
                                                                    + "if(SUM(re_od.qty) IS NULL, 0 + if(re_od.min_qty IS NULL, 0, SUM(re_od.min_qty)),  "
                                                                    + " SUM(re_od.qty) + if(re_od.min_qty IS NULL, 0, SUM(re_od.min_qty))) "
                                                                    + " ) , DECIMAL(9,2)) AS out_qty , "
                                                                    + " p.qty"
                                                                    + " FROM "
                                                                    + " orders o "
                                                                    + " JOIN order_details od on od.or_id = o.id"
                                                                    + " left JOIN re_orders re_o on re_o.or_id = o.id"
                                                                    + " left JOIN re_order_details re_od on re_od.re_or_id = re_o.id"
                                                                    + " JOIN products p on p.id = od.p_id"
                                                                    + " JOIN categories c on c.id = p.cat_id"
                                                                    + " JOIN units u on u.id = p.unit_id"
                                                                    + " WHERE o.inventory = 0"
                                                                    + " AND (o.date BETWEEN '" + from + "' AND '" + to + "')"
                                                                    + " GROUP By"
                                                                    + " p.id"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable getAllOrders()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id,"
                                                                    + " CONVERT( "
                                                                    + " if(u.small_unit = '',  SUM(od.qty) - if(re_od.qty IS NULL, 0, SUM(re_od.qty)), "
                                                                    + " (SUM(od.qty) * u.total_qty + SUM(od.min_qty) ) - "
                                                                    + "if(SUM(re_od.qty) IS NULL, 0 + if(re_od.min_qty IS NULL, 0, SUM(re_od.min_qty)),  "
                                                                    + " SUM(re_od.qty) + if(re_od.min_qty IS NULL, 0, SUM(re_od.min_qty))) "
                                                                    + " ) , DECIMAL(9,2)) AS out_qty , "
                                                                    + " p.qty"
                                                                    + " FROM "
                                                                    + " orders o "
                                                                    + " JOIN order_details od on od.or_id = o.id"
                                                                    + " left JOIN re_orders re_o on re_o.or_id = o.id"
                                                                    + " left JOIN re_order_details re_od on re_od.re_or_id = re_o.id"
                                                                    + " JOIN products p on p.id = od.p_id"
                                                                    + " JOIN categories c on c.id = p.cat_id"
                                                                    + " JOIN units u on u.id = p.unit_id"
                                                                    + " WHERE o.inventory = 0"
                                                                    + " GROUP By"
                                                                    + " p.id"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable getAllEOG(string from, string to)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id,"
                                                                    + " CONVERT( "
                                                                    + " if(u.small_unit = '',  SUM(eog_d.qty), SUM(eog_d.qty) * u.total_qty + SUM(eog_d.min_qty) "
                                                                    + " ) , DECIMAL(9,2)) AS lose_qty "
                                                                    + " FROM "
                                                                    + " eog "
                                                                    + " JOIN eog_details eog_d on eog_d.eog_id = eog.id"
                                                                    + " JOIN products p on p.id = eog_d.p_id"
                                                                    + " JOIN categories c on c.id = p.cat_id"
                                                                    + " JOIN units u on u.id = p.unit_id"
                                                                    + " WHERE eog.inventory = 0"
                                                                    + " AND (eog.date BETWEEN '" + from + "' AND '" + to + "')"
                                                                    + " GROUP By"
                                                                    + " p.id"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable getAllEOG()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id,"
                                                                    + " CONVERT( "
                                                                    + " if(u.small_unit = '',  SUM(eog_d.qty), SUM(eog_d.qty) * u.total_qty + SUM(eog_d.min_qty) "
                                                                    + " ) , DECIMAL(9,2)) AS lose_qty "
                                                                    + " FROM "
                                                                    + " eog "
                                                                    + " JOIN eog_details eog_d on eog_d.eog_id = eog.id"
                                                                    + " JOIN products p on p.id = eog_d.p_id"
                                                                    + " JOIN categories c on c.id = p.cat_id"
                                                                    + " JOIN units u on u.id = p.unit_id"
                                                                    + " WHERE eog.inventory = 0"
                                                                    + " GROUP By"
                                                                    + " p.id"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable Print(int id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT"
                                                                    + " i.id,i.date,i.start_date,i.end_date,i.description, i.type,"
                                                                    + " p.id as 'product_id', p.name as 'product_name',c.name as 'cat_name' ,"
                                                                    + " in_d.in_qty,in_d.out_qty,in_d.lose_qty,in_d.qty, un.unit_name, un.small_unit,un.total_qty,"
                                                                    + " u.full_name"
                                                                    + " FROM "
                                                                    + " inventory i"
                                                                    + " JOIN inventory_details in_d ON i.id = in_d.in_id"
                                                                    + " JOIN products p ON in_d.p_id = p.id"
                                                                    + " JOIN categories c ON p.cat_id = c.id"
                                                                    + " JOIN units un ON p.unit_id = un.id"
                                                                    + " JOIN users u ON i.user_id = u.id"
                                                                    + " WHERE i.id = " + id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }
        
    }
}
