using AnyCompany.Data;
using AnyCompany.Model;
using AnyCompany.Service;
using AnyCompany.Service.Requests;
using Autofac.Extras.Moq;
using Xunit;
using Moq;

namespace AnyCompany.Tests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public void PlaceOrder_Gets_Customer()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Customer>>().Setup(x => x.Load(It.IsAny<int>())).Returns(new Customer());
                var service = mock.Create<OrderService>();

                var request = new PlaceOrderRequest{Amount = 1};
                service.PlaceOrder(request);

                mock.Mock<IRepository<Customer>>().Verify(x => x.Load(It.IsAny<int>()), Times.Once);

            }
        }
    }
}
