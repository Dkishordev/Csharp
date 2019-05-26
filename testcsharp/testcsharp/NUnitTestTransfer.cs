using NUnit.Framework;
using System;
namespace testcsharp
{
    [TestFixture()]
    public class MainClassTest
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        TestDataUtils testdata = new TestDataUtils();

        [Test]
        //Case1:  already transfered greater or equal to maxlimit 
        public void SendAmount_GT_MaxLimit()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 20000);
            testdata.InsertData_Account(10002, 10000);
            testdata.InsertData_Transfer(10001, 10002, 100000, today);

            var test = new BalanceTransaction();

            var result = test.Transfer(10001, 10002, 100000);

            Assert.AreEqual("Failure", result);
        }

        [Test]
        //Case2: already transfered less than max limit
        public void Already_Transfer_LT_MaxLimit()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 150000);
            testdata.InsertData_Transfer(10001, 10002, 90000, today);

            var test = new BalanceTransaction();

            var result = test.Transfer(10001, 10002, 6000);
            Assert.AreEqual("Success", result);

        }

        [Test]
        //Case3 = sender balance doesnt have enough balance to transfer
        public void SenderBalance_LT_TransferAmount()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 30000);
            var test = new BalanceTransaction();

            var result = test.Transfer(10001, 10002, 35000);

            Assert.AreEqual("Failure", result);

        }

        [Test]
        //case4: sender sends 0 or negative balance
        public void Sender_Sends_Neg_Or_Zero_Balance()
        {
            var test = new BalanceTransaction();

            var result = test.Transfer(10001, 10002, 0);
            var result1 = test.Transfer(10001, 10002, -1000);

            Assert.AreEqual("Failure", result);
            Assert.AreEqual("Failure", result1);
        }

        [Test]
        //case5: successful transfer
        public void Successful_Transfer()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 50000);
            testdata.InsertData_Account(10002, 30000);
            testdata.InsertData_Account(10003, 20000);
            testdata.InsertData_Transfer(10001, 10002, 10000, today);
            testdata.InsertData_Transfer(10003, 10002, 5000, today);

            var test = new BalanceTransaction();

            var result = test.Transfer(10001, 10002, 15000);
            var result1 = test.Transfer(10003, 10002, 15000);

            Assert.AreEqual("Success", result);
            Assert.AreEqual("Success", result1);
        }

        [Test]
        //case6: Sender balance becomes less than 5000 after transaction
        public void Balance_LT_MinBalance()
        {
            testdata.TruncateTable("Account");
            testdata.TruncateTable("Transfer");
            testdata.InsertData_Account(10001, 10000);

            var test = new BalanceTransaction();

            var result = test.Transfer(10001, 10002, 6000);
            Assert.AreEqual("Failure", result);
            
        }

    }
}
