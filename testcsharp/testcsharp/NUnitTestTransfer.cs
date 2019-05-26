using NUnit.Framework;
using System;
namespace testcsharp
{
    [TestFixture()]
    public class MainClassTest
    {
        [Test]
        //Case1= already transfered >= maxlimit returns failure
        public void Case1()
        {
            //Arrange
            var obj = new MainClass();
            //Act
            var result = obj.Transfer(10001, 10002, 2000);
            //Assert
            Assert.AreEqual("Failure", result);
        }

        [Test]
        //Case2= already transfered <= maxlimit and remainingbalance >= minbalance returns success
        public void Case2()
        {
            //Arrange
            var obj = new MainClass();
            //Act
            var result = obj.Transfer(10001, 10002, 2000);
            //Assert
            Assert.AreEqual("Success", result);
        }

        [Test]
        //Case3 = (already transfered <= maxlimit and remainingbalance >= minbalance returns success) 
        // and amount <=remainingtransfer limit
        public void Case3()
        {
            var obj = new MainClass();
            var result = obj.Transfer(10001, 10002, 2000);
            Assert.AreEqual("Success", result);
                     
        }
    }
}
