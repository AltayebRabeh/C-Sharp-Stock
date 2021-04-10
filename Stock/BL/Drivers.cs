using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Stock.DAL;

namespace Stock.BL
{
    class Drivers
    {
        public static int Save(string name, string nat_num, string car_type, string car_name)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `drivers` (`name`, `nat_num`, `car_type`, `car_num`) VALUES ('" + name + "', '" + nat_num + "', '" + car_type + "', '" + car_name + "')", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable All()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , name as 'إسم السائق' , nat_num as 'رقم الهوية' , car_type as 'نوع السيارة', car_num as 'رقم الوحة' ,created_at as 'التاريخ ' FROM drivers WHERE status =1 ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Update(string name, string address, string phone, int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE suppliers SET name='" + name + "', address='" + address + "', phone='" + phone + "' WHERE id='" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE suppliers SET status =0 WHERE id = '" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable Search(string search)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , name as 'إسم المورد' , address as 'العنوان' , phone as 'رقم الهاتف' ,created_at as 'التاريخ ' FROM suppliers WHERE status =1 AND id LIKE '%" + search + "%' OR name LIKE '%" + search + "%' OR address LIKE '%" + search + "%' OR phone LIKE '%" + search + "%' ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static object GetLastDriverId()
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT MAX(id) FROM drivers", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }
    }
}
