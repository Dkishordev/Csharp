using System;
using SqlConn;
using System.Data;
using MySql.Data.MySqlClient;
namespace testcsharp
{
    public class Balance
    {
        public float balance, already_transfered;
        public MySqlParameterCollection Parameters { get; }
        public MySqlDataReader reader = null;

        //returns balance of the sender account
        public float GetBalance(int from)
        {
            
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

        //update balance of receiver account
        public void Balance_add(int from, int to, float amount)
        {
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "update Account set Balance = Balance + @amount where AccountID =@sendto;", conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@sendto", to);
                reader = cmd.ExecuteReader();
                conn.Close();

            }
            catch
            {
                Console.WriteLine("Error");
            }


        }

        //update balance of sender account
        public void Balance_sub(int from, int to, float amount)
        {
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "update Account set Balance = Balance - @amount where AccountID =@sendfrom;", conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@sendfrom", from);
                reader = cmd.ExecuteReader();
                conn.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        //insert transaction into transfer table
        public void Addtransaction(int from, int to, float amount)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "insert into Transfer (SendFrom, SendTo, Amount, Date) values (@sendfrom,@sendto,@amount,@today);", conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@sendto", to);
                cmd.Parameters.AddWithValue("@sendfrom", from);
                cmd.Parameters.AddWithValue("@today", today);
                reader = cmd.ExecuteReader();
                conn.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }


        }
    }
}
