using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Stock.DAL;

namespace Stock.BL
{
    class Users
    {
        public static object GetUserFullname(int id)
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT full_name FROM users where id = " + id + " ", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static int Save(string full_name, string username, string password, string per, int active)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `users` (`full_name`, `username`, `password`, `per`, `active`) VALUES " +
                                                                        "('" + full_name + "', '" + username + "', '" + password + "', '" + per + "', " + active + ")", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int Update(string full_name, string per, int active, int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE `users` SET `full_name` = '" + full_name + "', `per` = '" + per + "', `active` = " + active + " WHERE `id` = " + id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object OldPass(int id)
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT password FROM users where id = " + id + " LIMIT 1", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }

        public static int UpdatePassword(string password, int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE `users` SET `password` = '" + password + "'  WHERE `id` = " + id + "", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable All()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , full_name as 'الاسم الكامل' , username as 'إسم المستخدم', per as 'الصلاحية', if(active = 0, 'غير مفعل', 'مفعل') as 'الحالة' FROM users WHERE status = 1 ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable CheckUsername(string username = null)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT * FROM users WHERE username = '" + username + "' ", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static DataTable Search(string search)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , full_name as 'الاسم الكامل' , username as 'إسم المستخدم', per as 'الصلاحية', if(active = 0, 'غير مفعل', 'مفعل') as 'الحالة' FROM users WHERE status = 1 AND id LIKE '%" + search + "%' OR username LIKE '%" + search + "%' OR full_name LIKE '%" + search + "%' OR per LIKE '%" + search + "%'  ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE users SET status = 0 WHERE id = '" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }
    }
}
