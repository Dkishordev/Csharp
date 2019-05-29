using System;
using MySql.Data.MySqlClient;
using SqlConn;
namespace testcsharp
{
    public class TestDataUtils
    {
        public MySqlParameterCollection Parameters { get; }

        public void TruncateTable(String tablename)
        {
            using (MySqlConnection connection = DBUtils.GetDBConnection())
            {
                try
                {
                    string query = "TRUNCATE TABLE " + tablename;
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
        }

       public void InsertData_Transfer(int from, int to, float amount, string date)
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
                    command.Parameters.AddWithValue("@today", date);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }

        public void InsertData_Account(int accountid, float amount)
        {
            using (MySqlConnection connection = DBUtils.GetDBConnection())
            {
                try
                {
                    string query = "insert into Account (AccountID, Balance) values (@accountid,@balance);";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@accountid", accountid);
                    command.Parameters.AddWithValue("@balance", amount);
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
