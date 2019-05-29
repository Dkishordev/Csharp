using System;
using SqlConn;
using System.Data;
using MySql.Data.MySqlClient;
namespace testcsharp
{
    public class Balance
    {
        public string today = DateTime.Now.ToString("yyyy-MM-dd");
        public MySqlParameterCollection Parameters { get; }

        //returns balance of the sender account
        public float GetBalance(int from)
        {
            float balance= 0;
            object a = new object();




            using (a) {
                
            }
            using (MySqlConnection connection = DBUtils.GetDBConnection())
            {
                try
                {
                    string query = "select Balance from Account where AccountID=@sendfrom;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@sendfrom", from);
                    balance = (float)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return balance;
        }

        //returns per day transfered amount of sender account 
        public double GetAlreadyTransfered(int from)
        {
            double already_transfered = 0;
            using (MySqlConnection connection = DBUtils.GetDBConnection())
            {
                try
                {
                    string query = "select sum(Amount), SendFrom from Transfer where SendFrom =@sendfrom and Date = @today group by SendFrom;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@sendfrom", from);
                    command.Parameters.AddWithValue("@today", today);
                    already_transfered = (double)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return already_transfered;  
        }


        //update balance of receiver account
        public void Balance_add(int from, int to, float amount)
        {
            using (MySqlConnection connection = DBUtils.GetDBConnection())
            { 
                try{
                    string query = "update Account set Balance = Balance + @amount where AccountID =@sendto;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@sendto", to);
                    command.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


        }

        //update balance of sender account
        public void Balance_sub(int from, int to, float amount)
        {
            using (MySqlConnection connection = DBUtils.GetDBConnection())
            {
                try
                {
                    string query = "update Account set Balance = Balance - @amount where AccountID =@sendfrom;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@sendfrom", from);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        //insert transaction into transfer table
        public void Addtransaction(int from, int to, float amount)
        {
            using (MySqlConnection connection = DBUtils.GetDBConnection())
            {
                try
                {
                    string query = "insert into Transfer (SendFrom, SendTo, Amount, Date) values (@sendfrom,@sendto,@amount,@today);";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@sendto", to);
                    command.Parameters.AddWithValue("@sendfrom", from);
                    command.Parameters.AddWithValue("@today", today);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


        }
    }
}
