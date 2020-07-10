using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.IServices;
using Xunit;

namespace AnyCompany.Tests
{
    public class GetCustomersUnitTests
    {
        private ICustomerService customerService;

        public GetCustomersUnitTests(ICustomerService _customerService)
        {
            this.customerService = _customerService;
        }

        [Fact]
        public void GetCustomer_CheckReturnType_ReturnTrue()
        {
            var customers = customerService.GetCustomersWithOrders();
            var result = customers is List<Customer>;
            Assert.True(result);
        }
    }
}
