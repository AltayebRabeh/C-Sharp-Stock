using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class Categories
    {
        public static int Save(string name, string desc)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `categories` (`name`, `description`) VALUES ('" + name + "', '" + desc + "')", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable All()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , name as 'إسم الصنف' ,description as 'وصف الصنف' FROM categories WHERE status=1 ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Update(string name, string desc, int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE categories SET name='" + name + "', description='" + desc + "' WHERE id='" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE categories SET status = 0 WHERE id= "+ id +"", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable Search(string search)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , name as 'إسم الصنف' ,description as 'وصف الصنف' FROM categories WHERE status=1 AND id LIKE '%" + search + "%' OR name LIKE '%" + search + "%' OR description LIKE '%" + search + "%' ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }
    }
}
