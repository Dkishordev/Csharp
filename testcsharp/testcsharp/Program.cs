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
    class BalanceTransaction
    {
        private const float maxlimit = 100000, minbalance = 5000;

        public string Transfer(int from, int to, int amount)
        {
          
            if(amount > maxlimit || amount <= 0) 
                return "Failure";

            Balance balance = new Balance();
            float getbalance = balance.GetBalance(from);

            //get already transfered balance on present day
            float already_transfered = balance.GetAlreadyTransfered(from);

            //remaining balance of sender after sending amount.
            float rembalance = getbalance - amount;

            if (already_transfered>=maxlimit)
                return "Failure";
            
            if(already_transfered<maxlimit && rembalance>=minbalance)
            {
                float remaining_transfer_limit = maxlimit - already_transfered;
                if (amount <= remaining_transfer_limit)
                {
                    //update balance of sender and receiver account
                    balance.Balance_sub(from, to, amount);
                    balance.Balance_add(from, to, amount);

                    //add transaction in Transfer table
                    balance.Addtransaction(from, to, amount);
                    return "Success"; 
                }
                    
                else
                    return "Failure";
            }
            else
                return "Failure";
            
        }


        public static void Main()
        {
            BalanceTransaction transfer = new BalanceTransaction();
            Console.WriteLine(transfer.Transfer(10002, 10001, 1000));
        }
    }
}
