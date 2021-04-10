using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class Requests
    {
        public static int Save(int su_id, int d_id, int u_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `requests` (`su_id`, `dr_id`, `u_id`) VALUES (" + su_id + ", " + d_id + ", " + u_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int SaveDetails(int p_id, decimal qty, decimal min_qty, int re_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `request_details` (`p_id`, `qty`, `min_qty`, `re_id`) VALUES (" + p_id + ", " + qty + ", " + min_qty + ", " + re_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object GetLastRequestId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM requests", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static DataTable AllRequests(int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re.id as 'المعرف', re.date as 'التاريخ',su.name as 'إسم المورد', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM requests re "
                                                            + "JOIN suppliers su ON su.id = re.su_id "
                                                            + "JOIN drivers dr on dr.id = re.dr_id "
                                                            + "JOIN users u on u.id = re.u_id WHERE inventory = " + inventory + " "
                                                            + "ORDER BY re.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
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
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re.id as 'المعرف', re.date as 'التاريخ',su.name as 'إسم المورد', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM requests re "
                                                            + "JOIN suppliers su ON su.id = re.su_id "
                                                            + "JOIN drivers dr on dr.id = re.dr_id "
                                                            + "JOIN users u on u.id = re.u_id WHERE inventory = " + inventory + " "
                                                            + "AND re.id LIKE '%" + search + "%' OR su.name LIKE '%" + search + "%' OR dr.name LIKE '%" + search + "%' OR u.full_name LIKE '%" + search + "%' "
                                                            + "ORDER BY re.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable AllRequestsReback(int reback)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re.id as 'المعرف', re.date as 'التاريخ',su.name as 'إسم المورد', "
                                                            + "dr.name as 'إسم السائق', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM requests re "
                                                            + "JOIN suppliers su ON su.id = re.su_id "
                                                            + "JOIN drivers dr on dr.id = re.dr_id "
                                                            + "JOIN users u on u.id = re.u_id "
                                                            + "WHERE inventory =0 AND reback = " + reback + " ORDER BY re.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable RequestsDetails(int re_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'اسم المنتج', rd.qty as 'الكمية', rd.min_qty as 'الكمية الصغري', if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM request_details rd "
                                                            + "JOIN products p ON p.id = rd.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE rd.re_id = " + re_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable AllRequestDetails(int re_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id, rd.qty, rd.min_qty, if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM request_details rd "
                                                            + "JOIN products p ON p.id = rd.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE rd.re_id = " + re_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("DELETE FROM requests WHERE id = " + id + " ", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable ReRequestsDetails(int re_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id, re_re_d.qty, re_re_d.min_qty, if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM re_request_details re_re_d "
                                                            + "JOIN products p ON p.id = re_re_d.p_id "
                                                            + "JOIN re_requests re_r ON re_r.id = re_re_d.re_re_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE re_re_d.re_re_id = re_r.id"
                                                            + "AND re_r.re_id = " + re_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable RequestReport(int id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re.id, re.date, d.name as 'driver_name', u.full_name as 'username', su.name as 'supplier_name', re.reback,"
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  rd.qty, rd.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM requests re "
                                                               + ", request_details rd "
                                                               + ", products p "
                                                               + ", suppliers su "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE rd.re_id = re.id "
                                                               + "AND p.id = rd.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = re.dr_id "
                                                               + "AND u.id = re.u_id "
                                                               + "AND re.id = " + id + " "
                                                               + "GROUP BY p.id, p.name, c.name, rd.qty, rd.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable RequestsReport()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT re.id, re.date, d.name as 'driver_name', u.full_name as 'username', su.name as 'supplier_name', re.reback,"
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  rd.qty, rd.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM requests re "
                                                               + ", request_details rd "
                                                               + ", products p "
                                                               + ", suppliers su "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", drivers d "
                                                               + ", users u "
                                                               + "WHERE rd.re_id = re.id "
                                                               + "AND p.id = rd.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND d.id = re.dr_id "
                                                               + "AND u.id = re.u_id "
                                                               + "GROUP BY p.id, p.name, c.name, rd.qty, rd.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }
    }
}
