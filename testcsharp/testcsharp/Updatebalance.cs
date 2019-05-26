using System;
using SqlConn;
using System.Data;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace testcsharp
{
    public class Updatebalance
    {
        public float balance;
        public MySqlParameterCollection Parameters { get; }

        //update balance of receiver account
        public void Balance_add(int from, int to, float amount)
        {
            MySqlDataReader reader = null;
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
            MySqlDataReader reader = null;
            try{
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "update Account set Balance = Balance - @amount where AccountID =@sendfrom;", conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@sendfrom", from);
                reader = cmd.ExecuteReader();
                conn.Close();
            }
            catch{ 
                Console.WriteLine("Error");
            }
         
        }

        //insert transaction into transfer table
        public void Addtransaction(int from, int to, float amount)
        {
            MySqlDataReader reader = null;
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            try{
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
            catch{
                Console.WriteLine("Error");
            }
        
            
        }

    }
}
