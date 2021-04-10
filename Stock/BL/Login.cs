using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL;
using System.Data;

namespace Stock.BL
{
    class Login
    {
        public static DataTable LoginUser(string username, string password)
        {
            DataAccessLayer.Open();
            DataTable dt = DataAccessLayer.ExecuteTable("SELECT * FROM users WHERE status = 1 AND username = '" + username + "' AND password = '" + password + "' LIMIT 1", CommandType.Text);
            DataAccessLayer.Close();

            return dt;
        }
    }
}
