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
        //Case1= already transfered >= maxlimit returns failure
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
        //Case2 = sender balance doesnt have enough balance to transfer
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
        //case3: sender sends 0 or negative balance
        public void Sender_Sends_Neg_Or_Zero_Balance()
        {
            var test = new BalanceTransaction();

            var result = test.Transfer(10001, 10002, 0);
            var result1 = test.Transfer(10001, 10002, -1000);

            Assert.AreEqual("Failure", result);
            Assert.AreEqual("Failure", result1);
        }
    }
}
