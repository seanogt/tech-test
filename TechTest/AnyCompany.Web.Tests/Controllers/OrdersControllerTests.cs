using System.Threading.Tasks;
using AnyCompany.Web.Controllers;
using NUnit.Framework;

namespace AnyCompany.Web.Tests.Controllers
{
    [TestFixture]
    public class OrdersControllerTests : ApiTestsBase
    {
        private OrdersController _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new OrdersController(_container);
        }

        // Add tests for what happens on primary key and unique columns collision
        [Test]
        [Ignore("Create mocks before running test")]
        public async Task CreatingACustomer_Really_CreatesAndSavesTheCustomer()
        {
            var result = await _subject.CreateOrder("Json Order Data Here");
            var createdId = result.Value;

            var orderFromDb = await _container.OrdersFacade.GetOrderById(createdId);
            
            Assert.NotNull(orderFromDb);
            
            // TODO: Compare fields to sent JSON, make sure they match
        }
    }
}