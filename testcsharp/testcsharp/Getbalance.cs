using System;
using SqlConn;
using System.Data;
using MySql.Data.MySqlClient;
namespace testcsharp
{
    public class Getbalance
    {
        public float balance, already_transfered;
        public MySqlParameterCollection Parameters { get; }

        //returns balance of the sender account
        public float CheckBalance(int from)
        {
            MySqlDataReader reader = null;
            try{
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "select Balance from Account where AccountID=@sendfrom;", conn);
                cmd.Parameters.AddWithValue("@sendfrom", from);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    balance = Convert.ToInt32(reader["Balance"]);
                }
                conn.Close();
                return balance;
                
            }
            catch{
                return 0;
            }
        }

        //returns per day transfered amount of sender account 
        public float GetAlreadyTransfered(int from)
        {
            MySqlDataReader reader = null;
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            try{
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                        "select sum(Amount) as Total from Transfer where SendFrom = @sendfrom and Date =@today;", conn);
                cmd.Parameters.AddWithValue("@sendfrom", from);
                cmd.Parameters.AddWithValue("@today", today);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    already_transfered = Convert.ToInt32(reader["Total"]);
                }
                conn.Close();
                return already_transfered;
                
            }
            catch{
                return 0;
            }
           
        }
    }
}
