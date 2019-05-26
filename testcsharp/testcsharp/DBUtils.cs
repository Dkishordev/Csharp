using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace SqlConn
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            string database = "banktransaction";
            string username = "root";
            string password = "";

            String connString = "Server=" + host + ";Database=" + database
                + ";User Id=" + username + ";password=" + password;
            
            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
