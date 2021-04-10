using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Stock.DAL;

namespace Stock.BL
{
    class Customers
    {
        public static int Save(string name, string address, string phone)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `customers` (`name`, `address`, `phone`) VALUES ('" + name + "', '" + address + "', '" + phone + "')", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable All()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , name as 'إسم العميل' , address as 'العنوان' , phone as 'رقم الهاتف' ,created_at as 'التاريخ ' FROM customers  WHERE status =1 ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Update(string name, string address, string phone, int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE customers SET name='" + name + "', address='" + address + "', phone='" + phone + "' WHERE id='" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE customers SET status = 0 WHERE id = '" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable Search(string search)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , name as 'إسم العميل' , address as 'العنوان' , phone as 'رقم الهاتف' ,created_at as 'التاريخ ' FROM customers WHERE status =1 AND id LIKE '%" + search + "%' OR name LIKE '%" + search + "%' OR address LIKE '%" + search + "%' OR phone LIKE '%" + search + "%' ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static object GetLastCustomerId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM customers", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }
    }
}
