using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Stock.DAL
{
    class DataAccessLayer
    {
        public static string connStr = "server=localhost;user=root;database=stock;port=3306;password=";
        public static MySqlConnection conn = new MySqlConnection(connStr);
        

        public static void Open()
        {
            if (ConnectionState.Closed == conn.State)
            {
                conn.Open();
            }
        }

        public static void Close()
        {
            if (ConnectionState.Open == conn.State)
            {
                conn.Close();
            }
        }

        public static object ExcuteScalar(string query,CommandType type,params MySqlParameter[]arr)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddRange(arr);
            cmd.CommandType = type;
            object o = cmd.ExecuteScalar();
            return o;
        }

        public static int ExecuteNonQuery(string query, CommandType type, params MySqlParameter[] arr)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.CommandType = type;
            cmd.Parameters.AddRange(arr);
            int n = cmd.ExecuteNonQuery();
            return n;
        }

        public static DataTable ExecuteTable(string query,CommandType type,params MySqlParameter[]arr)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.CommandType = type;
            cmd.Parameters.AddRange(arr);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            
            da.Fill(dt);
            return dt;
        }
    }
}
