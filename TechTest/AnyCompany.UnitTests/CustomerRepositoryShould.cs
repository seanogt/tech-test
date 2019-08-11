using AnyCompany.Services.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyCompany.UnitTests
{
    /// <summary>
    /// can't really test static class/method
    /// </summary>
    [TestClass]
    public class CustomerRepositoryShould
    {
        [TestMethod]
        public void ShouldLoadCustomerOk()
        {
            var customer = CustomerRepository.Load(1);

            customer.CustomerId.Should().Be(1);
        }
    }
}
