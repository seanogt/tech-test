using System;
using System.Linq;
using System.Threading.Tasks;
using AnyCompany.Service.Models;
using AnyCompany.Web.Controllers;
using NUnit.Framework;

namespace AnyCompany.Web.Tests.Controllers
{
    [TestFixture]
    public class CustomerControllerTests : ApiTestsBase
    {
        // Mock objects to be exported to a separate file and reused across tests, but for demonstration purposes:
        
        protected Order _mockOrder = new Order("123", 23, 23, "123");
        private CustomersController _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new CustomersController(_container);
        }

        [Test]
        [Ignore("Create mocks before running test")]
        public async Task GettingCustomersOrders_Returns_TheCorrectOrders()
        {
            // This is a direct call to the controller's method.
            // Although it tests the logic, it does not test authentication, authorisation or redirection.
            // Make sure that the other flows are tested by making real API requests.
            var result = await _subject.GetCustomerOrders(_mockOrder.OrderId); // Passing in the ID of a mock object. (For the sake of this task, not creating mock objects, but you get the idea).
            var value = result.Value;
                
            Assert.IsNotNull(value);
            
            // Need to compare values of fields:
            Assert.AreEqual(value.FirstOrDefault(), _mockOrder);
        }

        [Test]
        [Ignore("Create mocks before running test")]
        // Add test that ensures that the API returns a 404 with no body.
        public async Task GettingOrderThatDoNotExist_ThrowsNotFoundException()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _subject.GetCustomerOrders("someNonExistingId");
            });
        }
    }
}