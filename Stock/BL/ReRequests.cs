using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class ReRequests
    {
        public static int Save(int d_id, int u_id, int re_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `re_requests` (`dr_id`, `u_id`, `re_id`) VALUES (" + d_id + ", " + u_id + ", " + re_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int SaveDetails(int p_id, decimal qty, decimal min_qty, int re_re_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `re_request_details` (`p_id`, `qty`, `min_qty`, `re_re_id`) VALUES (" + p_id + ", " + qty + ", " + min_qty + ", '" + re_re_id + "')", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int ReBack(int re_id, int reback)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE `requests` set `reback` = " + reback + " WHERE id = " + re_id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable AllReRequests(int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_re.id as 'المعرف', re_re.date as 'التاريخ', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "re.id as 'رقم الفاتورة', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM re_requests re_re "
                                                            + "JOIN requests re ON re.id = re_re.re_id "
                                                            + "JOIN drivers dr on dr.id = re_re.dr_id "
                                                            + "JOIN users u on u.id = re_re.u_id  WHERE  re.inventory = " + inventory + " "
                                                            + "ORDER BY re_re.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable Search(string search, int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_re.id as 'المعرف', re_re.date as 'التاريخ', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "re.id as 'رقم الفاتورة', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM re_requests re_re "
                                                            + "JOIN requests re ON re.id = re_re.re_id "
                                                            + "JOIN drivers dr on dr.id = re_re.dr_id "
                                                            + "JOIN users u on u.id = re_re.u_id  WHERE  re.inventory = " + inventory + " "
                                                            + "AND re_re.id LIKE '%" + search + "%' OR re.id LIKE '%" + search + "%' OR dr.name LIKE '%" + search + "%' OR u.full_name LIKE '%" + search + "%' "
                                                            + "ORDER BY re_re.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable ReRequestsDetails(int re_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id, re_re_d.qty, re_re_d.min_qty, if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM re_request_details re_re_d "
                                                            + "JOIN products p ON p.id = re_re_d.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE re_re_d.re_re_id = " + re_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("DELETE FROM re_requests WHERE id = " + id + " ", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object GetLastReRequestId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM re_requests", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static DataTable ReRequestReport(int id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_re.id, re_re.date, re_re.re_id, d.name as 'driver_name', u.full_name as 'username',"
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  re_rd.qty, re_rd.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM re_requests re_re "
                                                               + ", re_request_details re_rd "
                                                               + ", products p "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE re_rd.re_re_id = re_re.id "
                                                               + "AND p.id = re_rd.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = re_re.dr_id "
                                                               + "AND u.id = re_re.u_id "
                                                               + "AND re_re.id = " + id + " "
                                                               + "GROUP BY p.id, p.name, c.name, re_rd.qty, re_rd.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable ReRequestsReport()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re_re.id, re_re.date, re_re.re_id, d.name as 'driver_name', u.full_name as 'username',"
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  re_rd.qty, re_rd.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM re_requests re_re "
                                                               + ", re_request_details re_rd "
                                                               + ", products p "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE re_rd.re_re_id = re_re.id "
                                                               + "AND p.id = re_rd.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = re_re.dr_id "
                                                               + "AND u.id = re_re.u_id "
                                                               + "GROUP BY p.id, p.name, c.name, re_rd.qty, re_rd.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }
    }
}
