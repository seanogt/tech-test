using System;
using NUnit.Framework;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class OrderTests
    {
        readonly Customer NON_UK_CUSTOMER = new Customer() { Name = "John", Country = "Sweden", DateOfBirth = new DateTime(1971, 7, 22) };
        readonly Customer UK_CUSTOMER = new Customer() { Name = "Jane", Country = "UK", DateOfBirth = new DateTime(1981, 4, 13) };
        readonly Order VALID_ORDER = new Order() { OrderId = 1, Amount = 5, VAT = 0.5d };


        [Test()]
        public void GivenNonUKCustomer_SetVAT_ShouldSetVATToZero()
        {
            var order = VALID_ORDER.SetVAT(NON_UK_CUSTOMER);
            Assert.That(order.VAT == 0);
        }

        [Test()]
        public void GivenAUKCustomer_SetVAT_ShouldSetVATTo2Percents()
        {
            var order = VALID_ORDER.SetVAT(UK_CUSTOMER);
            Assert.That(order.VAT == 0.2d);
        }
    }
}
