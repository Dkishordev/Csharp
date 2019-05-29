using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace testcsharp
{
    
    [TestFixture()]
    public class MainClassTest
    {
        static ClassTest datatest = new ClassTest();
        [Test, TestCaseSource("TestCases")]
        public void Case(int sendfrom, int sendto, int amount, string status)
        {

            var test = new BalanceTransaction();
   
            var result = test.Transfer(sendfrom, sendto, amount);
                   
            Assert.AreEqual(status, result);
        }
    
      public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                datatest.SendAmount_GT_MaxLimit();
                yield return new TestCaseData(10001, 10002, 100000, "Failure").SetName("SendAmount_GT_MaxLimit()");
                datatest.Already_Transfer_LT_MaxLimit();
                yield return new TestCaseData(10001, 10002, 1000, "Success").SetName("Already_Transfer_LT_MaxLimit()");
                datatest.SenderBalance_LT_TransferAmount();
                yield return new TestCaseData(10001, 10002, 35000, "Failure").SetName("SenderBalance_LT_TransferAmount()");
                datatest.Sender_Sends_Neg_Or_Zero_Balance();
                yield return new TestCaseData(10001, 10002, -1000, "Failure").SetName("Sender_Sends_Neg_Or_Zero_Balance()");
                datatest.Successful_Transfer();
                yield return new TestCaseData(10001, 10002, 4000, "Success").SetName("Successful_Transfer()");
                datatest.Balance_LT_MinBalance();
                yield return new TestCaseData(10001, 10002, 6000, "Failure").SetName("Balance_LT_MinBalance()");
            }
        }

    }
}
