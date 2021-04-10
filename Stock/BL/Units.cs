using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class Units
    {
        public static int Save(string unit_name, string small_unit, int total_qty)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `units` (`unit_name`, `small_unit`, `total_qty`) VALUES ('" + unit_name + "', '" + small_unit + "', '" + total_qty + "')", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int SaveOne(string unit_name)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("INSERT INTO `units` (`unit_name`) VALUES ('" + unit_name + "')", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static DataTable All()
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT id as 'المعرف' , unit_name as 'إسم وحدة القياس' ,small_unit as 'وحدة القياس الصغرى', total_qty as 'عدد الوحدات بالحبة' FROM units WHERE status = 1 ORDER BY id DESC", CommandType.Text);
            DataAccessLayer.Close();
            return dt;
        }

        public static int Update(string unit_name, string small_unit, int total_qty, int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE units SET unit_name='" + unit_name + "', small_unit='" + small_unit + "', total_qty='" + total_qty + "' WHERE id='" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int UpdateOne(string unit_name, int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE units SET unit_name='" + unit_name + "' WHERE id='" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static int Delete(int id)
        {
            DataAccessLayer.Open();
            int i = DataAccessLayer.ExecuteNonQuery("UPDATE units SET status = 0 WHERE id = '" + id + "'", CommandType.Text);
            DataAccessLayer.Close();
            return i;
        }

        public static object small_unit(int id)
        {
            DataAccessLayer.Open();
            object o = DataAccessLayer.ExcuteScalar("SELECT total_qty FROM units WHERE id = " + id + " LIMIT 1", CommandType.Text);
            DataAccessLayer.Close();
            return o;
        }
    }
}
