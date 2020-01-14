using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkout;

namespace CheckoutTest
{
    [TestClass]
    public class CheckoutMachineTest
    {
        [TestMethod]
        public void ShouldReturnNormalPriceOfASimpleItem()
        {
            CheckoutMachine checkout = new CheckoutMachine("a: 1");
            checkout.Scan("a");
            Assert.AreEqual(checkout.Total, 1);
        }

        [TestMethod]
        public void ShouldReturnNormalPriceOfASimpleItemScannedTwoTimes()
        {
            CheckoutMachine checkout = new CheckoutMachine("a: 1");
            checkout.Scan("a");
            checkout.Scan("a");
            Assert.AreEqual(checkout.Total, 2);
        }

        [TestMethod]
        public void ShouldReturnSpecialPriceOfItemWithSpecialPrice()
        {
            CheckoutMachine checkout = new CheckoutMachine("a: 1 2 for 1");
            checkout.Scan("a");
            checkout.Scan("a");
            Assert.AreEqual(checkout.Total, 1);
        }

        [TestMethod]
        public void ShouldReturnCorrectPriceForMixedItems()
        {
            CheckoutMachine checkout = new CheckoutMachine(@"a: 2 2 for 1
b: 7
c: 4 3 for 10");
            checkout.Scan("a");
            checkout.Scan("b");
            checkout.Scan("c");
            checkout.Scan("c");
            checkout.Scan("c");
            Assert.AreEqual(checkout.Total, 19);
        }
    }
}
