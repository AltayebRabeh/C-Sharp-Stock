using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class Products
    {
        public static int Save(string name, string desc, int active, int unit_id, int cat_id, int user_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `products` (`name`, `description`, `active`, `unit_id`, `cat_id`, `user_id`)"
                        + "VALUES ('" + name + "', '" + desc + "', '" + active + "', " + unit_id + ", " + cat_id + ", " + user_id + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable All()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'إسم المنتج',c.name as 'إسم الصنف', p.description as 'الوصف', "
                                                            + "if(un.small_unit = '', CONCAT_WS(un.unit_name, CONVERT(p.qty, VARCHAR(10)), ' '), "
                                                            + "CONCAT_WS(un.unit_name, CONVERT(CONVERT(p.qty / un.total_qty, INTEGER), VARCHAR(10)) , CONCAT_WS(un.small_unit , CONVERT(p.qty % un.total_qty, VARCHAR(10)), ''))) as 'الكمية',"
                                                            + "if(p.active = 1, 'مفعل', 'غير مفعل') as 'الحالة', u.full_name as 'اسم المستخدم', units.unit_name as 'وحدة القياس', p.created_at as 'تاريخ الاضافة' "
                                                            +"FROM products p "
                                                            + "JOIN users u on u.id = p.user_id "
                                                            + "JOIN units un on un.id = p.unit_id "
                                                            +" JOIN categories c ON c.id = p.cat_id "
                                                            + "JOIN units on units.id = p.unit_id  WHERE p.status = 1 ORDER BY p.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Update(int id, string name, string desc, int active, int unit_id, int cat_id, int user_id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE `products` SET `name` = '" + name + "', `description` = '" + desc + "', `active` = '" + active + "', "
                                                    + "`unit_id` = " + unit_id + ", `cat_id` = " + cat_id + ", `user_id` = " + user_id + " WHERE id = " + id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE products SET status = 0 WHERE id= " + id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable Search(string search)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'إسم المنتج',c.name as 'إسم الصنف', p.description as 'الوصف', p.qty as 'الكمية', "
                                                            + "if(p.active = 1, 'مفعل', 'غير مفعل') as 'الحالة', u.full_name as 'اسم المستخدم', units.unit_name as 'وحدة القياس', p.created_at as 'تاريخ الاضافة' "
                                                            + "FROM products p "
                                                            + "JOIN users u on u.id = p.user_id "
                                                            + "JOIN categories c ON c.id = p.cat_id "
                                                            + "JOIN units on units.id = p.unit_id "
                                                            + "WHERE p.status=1 AND p.id LIKE '%" + search + "%' OR "
                                                            + "p.name LIKE '%" + search + "%' OR "
                                                            + "c.name LIKE '%" + search + "%' OR "
                                                            + "p.description LIKE '%" + search + "%' OR "
                                                            + "u.full_name LIKE '%" + search + "%' OR "
                                                            + "units.unit_name LIKE '%" + search + "%' ORDER BY p.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }


        public static DataTable SearchActivation(string search)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'إسم المنتج',c.name as 'إسم الصنف', p.description as 'الوصف', p.qty as 'الكمية', "
                                                            + "if(p.active = 1, 'مفعل', 'غير مفعل') as 'الحالة', u.full_name as 'اسم المستخدم', units.unit_name as 'وحدة القياس', p.created_at as 'تاريخ الاضافة' "
                                                            + "FROM products p "
                                                            + "JOIN users u on u.id = p.user_id "
                                                            + "JOIN categories c ON c.id = p.cat_id "
                                                            + "JOIN units on units.id = p.unit_id "
                                                            + "WHERE p.status=1 AND p.active LIKE '%" + search + "%' ORDER BY p.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int AddQty(int p_id, decimal qty)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE `products` set `qty` = `qty` + " + qty + " WHERE id = " + p_id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int RemoveQty(int p_id, decimal qty)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE `products` set `qty` = `qty` - " + qty + " WHERE id = " + p_id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable AllProductsActive()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'إسم المنتج',c.name as 'إسم الصنف', "
                                                            + "units.unit_name as 'وحدة القياس', "
                                                            + "units.small_unit as 'وحدة القياس الصغرى' "
                                                            + "FROM products p "
                                                            + "JOIN categories c ON c.id = p.cat_id "
                                                            + "JOIN units on units.id = p.unit_id "
                                                            + "WHERE p.active = 1 "
                                                            + "ORDER BY p.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable SearchProductsActive(string search)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT p.id as 'المعرف', p.name as 'إسم المنتج',c.name as 'إسم الصنف', "
                                                            + "units.unit_name as 'وحدة القياس', "
                                                            + "units.small_unit as 'وحدة القياس الصغرى' "
                                                            + "FROM products p "
                                                            + "JOIN categories c ON c.id = p.cat_id "
                                                            + "JOIN units on units.id = p.unit_id "
                                                            + "WHERE p.active = 1 and status = 1 AND "
                                                            + "p.id LIKE '%" + search + "%' "
                                                            + "order p.name LIKE '%" + search + "%' "
                                                            + "ORDER BY p.id DESC"
                                                            , CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static object qty(int id)    
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT qty FROM products where id = "+id+" LIMIT 1", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static object unit_id(int id)
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT unit_id FROM products WHERE id = " + id + " LIMIT 1", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }
    }
}
