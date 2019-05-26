using System;
using MySql.Data.MySqlClient;
using SqlConn;
namespace testcsharp
{
    public class TestDataUtils
    {
        public MySqlParameterCollection Parameters { get; }
        public MySqlDataReader reader = null;

        public void TruncateTable(String tablename)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string query = "TRUNCATE TABLE " + tablename;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

       public void InsertData_Transfer(int from, int to, float amount, string date)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(
                "insert into Transfer (SendFrom, SendTo, Amount, Date) values (@sendfrom,@sendto,@amount,@date);", conn);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@sendto", to);
            cmd.Parameters.AddWithValue("@sendfrom", from);
            cmd.Parameters.AddWithValue("@date", date );
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        public void InsertData_Account(int accountid, float amount)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(
                "insert into Account (AccountID, Balance) values (@accountid,@balance);", conn);
            cmd.Parameters.AddWithValue("@accountid", accountid);
            cmd.Parameters.AddWithValue("@balance", amount);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

    }
}
