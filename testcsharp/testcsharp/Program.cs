using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SqlConn;
using MySql.Data.MySqlClient;

namespace testcsharp
{
    class MainClass
    {
        public string transaction_status;
        private const float maxlimit = 100000, minbalance = 5000;
        public float check_balance,rembalance, already_transfered;

        public string Transfer(int from, int to, int amount)
        {
          
            if(amount > maxlimit && amount <= 0) return "Failure";

            Getbalance balance = new Getbalance();
             check_balance = balance.CheckBalance(from);
            already_transfered = balance.GetAlreadyTransfered(from);
            //already_transfered = 110000;
            rembalance = check_balance - amount;

            if (already_transfered>=maxlimit)
                transaction_status= "Failure";
            
            if(already_transfered<maxlimit && rembalance>=minbalance)
            {
                float remaining_transfer_limit = maxlimit - already_transfered;
                if (amount <= remaining_transfer_limit)
                {
                    //update balance of sender and receiver account
                    Updatebalance balanceupdate = new Updatebalance();
                    balanceupdate.Balance_sub(from, to, amount);
                    balanceupdate.Balance_add(from, to, amount);

                    //add transaction in Transfer table
                    balanceupdate.Addtransaction(from, to, amount);
                    transaction_status = "Success"; 
                }
                    
                else
                    transaction_status= "Failure";
            }
            else
                transaction_status = "Failure";
            
            return transaction_status; 
        }


        public static void Main()
        {
            MainClass transfer = new MainClass();
            Console.WriteLine(transfer.Transfer(10002, 10001, 2000));
        }
    }
}
