using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace Stock
{
    static class Program
    {
        public static SQLiteConnection con = new SQLiteConnection(@"Data Source=C:\Users\Public\Documents\stock_serial;Version=3;Password=stock_serial_num");
        public static int UserID;
        public static string UserPermission;
        public static string Serial;
        private static int Activated;
        private static int Count;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists("stock_serial"))
            {
                DialogResult d = MessageBox.Show("يوجد ملفات ناقصة الرجاء إعادة تثبيت البرنامج", null,MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (DialogResult.OK == d || DialogResult.Cancel == d)
                {
                    Application.Exit();
                }
            }

            if (!File.Exists(@"C:\Users\Public\Documents\stock_serial"))
            {
                File.Copy("stock_serial", @"C:\Users\Public\Documents\stock_serial");
            }

            SerialMethod();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Count > 30 && Activated == 0)
            {
                Application.Run(new PL.Activation());
            }
            else
            {
                Application.Run(new PL.Login());
            }
            
        }

        public static void activation()
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE serial SET activated = 1", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        static void SerialMethod()
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT activated FROM serial LIMIT 1", con);
            object activated = cmd.ExecuteScalar();
            Activated = Convert.ToInt32(activated);
            if (Activated == 0)
            {
                SQLiteCommand cmd2 = new SQLiteCommand("SELECT serial_number FROM serial LIMIT 1", con);
                object serial = cmd2.ExecuteScalar();
                Serial = Convert.ToString(serial);

                SQLiteCommand cmd3 = new SQLiteCommand("SELECT date FROM serial LIMIT 1", con);
                object date = cmd3.ExecuteScalar();

                SQLiteCommand cmd5 = new SQLiteCommand("SELECT count FROM serial LIMIT 1", con);
                object count = cmd5.ExecuteScalar();
                Count = Convert.ToInt32(count);

                if (Convert.ToString(date) == string.Empty)
                {
                    SQLiteCommand cmd4 = new SQLiteCommand("UPDATE serial SET date = '" + DateTime.Now.ToShortDateString() + "', count = 1", con);
                    cmd4.ExecuteNonQuery();
                }
                else if ((DateTime.Now - Convert.ToDateTime(date)).Days != 0)
                {
                    SQLiteCommand cmd6 = new SQLiteCommand("UPDATE serial SET date = '" + DateTime.Now.ToShortDateString() + "', count = count+1", con);
                    cmd6.ExecuteNonQuery();
                }

                con.Close();
            }
        }
    }
}
