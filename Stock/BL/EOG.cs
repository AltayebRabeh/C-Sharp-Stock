using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class EOG
    {
        public static int Save(string description, int u_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `eog` (`description`, `u_id`) VALUES ('" + description + "', " + u_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int SaveDetails(int p_id, decimal qty, decimal min_qty, int eog)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `eog_details` (`p_id`, `qty`, `min_qty`, `eog_id`) VALUES (" + p_id + ", " + qty + ", " + min_qty + ", " + eog + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object GetLastEOGId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM eog", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }


        public static DataTable AllEOG(int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT eog.id as 'المعرف', eog.date as 'التاريخ', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM eog "
                                                            + "JOIN users u on u.id = eog.u_id  WHERE inventory = " + inventory + " "
                                                            + "ORDER BY eog.id DESC "
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable Search(string search, int inventory = 0)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT eog.id as 'المعرف', eog.date as 'التاريخ', "
                                                            + "u.full_name as 'إسم المستخدم' "
                                                            + "FROM eog "
                                                            + "JOIN users u on u.id = eog.u_id  WHERE inventory = " + inventory + " "
                                                            + "AND eog.id LIKE '%" + search + "%' OR u.full_name LIKE '%" + search + "%' "
                                                            + "ORDER BY eog.id DESC "
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable EOGDetails(int EOG_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'اسم المنتج', eog_d.qty as 'الكمية', eog_d.min_qty as 'الكمية الصغري', if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM eog_details eog_d "
                                                            + "JOIN products p ON p.id = eog_d.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE eog_d.eog_id = " + EOG_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable AllOrderDetails(int eog_id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id, eog_d.qty, eog_d.min_qty, if(u.small_unit = '', '', u.total_qty) as '' "
                                                            + "FROM eog_d "
                                                            + "JOIN products p ON p.id = eog_d.p_id "
                                                            + "JOIN units u ON u.id = p.unit_id "
                                                            + "WHERE eog_d.eog_id = " + eog_id + ""
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("DELETE FROM eog WHERE id = " + id + " ", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable EOGReport(int id)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT eog.id, eog.date, u.full_name as 'username', "
                                                               + "p.name as 'product_name', p.id as 'product_id', c.name as 'category_name',  eog_d.qty, eog_d.min_qty, un.unit_name, un.small_unit, un.total_qty "
                                                               + "FROM eog "
                                                               + ", eog_details eog_d "
                                                               + ", products p "
                                                               + ", categories c "
                                                               + ", units un "
                                                               + ", users u "
                                                               + "WHERE eog_d.eog_id = eog.id "
                                                               + "AND p.id = eog_d.p_id "
                                                               + "AND p.cat_id = c.id "
                                                               + "AND un.id = p.unit_id "
                                                               + "AND u.id = eog.u_id "
                                                               + "AND eog.id = " + id + " "
                                                               + "GROUP BY p.id, p.name, c.name, eog_d.qty, eog_d.min_qty, un.unit_name, un.small_unit, un.total_qty"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }


    }
}
