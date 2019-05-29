using NUnit.Framework;
using System;
namespace testcsharp
{
    public class ClassTest
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        TestDataUtils testdata = new TestDataUtils();

        public void SendAmount_GT_MaxLimit()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 20000);
            testdata.InsertData_Account(10002, 10000);
            testdata.InsertData_Transfer(10001, 10002, 100000, today);
        }

        public void Already_Transfer_LT_MaxLimit()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 150000);
            testdata.InsertData_Transfer(10001, 10002, 90000, today);
        }

    
        public void SenderBalance_LT_TransferAmount()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 30000);
        }

        public void Sender_Sends_Neg_Or_Zero_Balance()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 20000);
            testdata.InsertData_Account(10002, 10000);
        }

        public void Successful_Transfer()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 50000);
            testdata.InsertData_Account(10002, 30000);
            testdata.InsertData_Account(10003, 20000);
            testdata.InsertData_Transfer(10001, 10002, 10000, today);
            testdata.InsertData_Transfer(10003, 10002, 5000, today);
        }

        public void Balance_LT_MinBalance()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 10000);
        }

    }
}
