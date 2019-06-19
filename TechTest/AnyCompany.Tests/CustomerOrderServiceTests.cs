using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AnyCompany;
using Moq;
using AnyCompany.Entities;

namespace AnyCompany.Tests
{
    public class CustomerOrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<ICustomerRepositoryService> _mockCustomerRepository;
        private readonly List<Order> _orders;
        private readonly List<Customer> _customers;
        public CustomerOrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepositoryService>();

            _orders = new List<Order> { new Order { OrderId = 1, Amount = 100, CustomerId = 1 }, new Order { OrderId = 2, Amount = 200, CustomerId = 1 } };
            _customers = new List<Customer> { new Customer { CustomerId = 1, Country = "UK" } };

        }

        [Fact]
        public void PlaceOrder_Success()
        {
            _mockOrderRepository.Setup(x => x.Save(It.IsAny<Order>())).Verifiable();
            _mockCustomerRepository.Setup(x => x.LoadCustomer(It.IsAny<int>())).Returns(new Customer { CustomerId = 1, Country = "UK" });

            var orderService = new CustomerOrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object, new VatService());

            var result = orderService.PlaceOrder(new Order { Amount = 100, OrderId = 1 }, 1);

            Assert.True(result);
        }

        [Fact]
        public void PlaceOrder_Reject()
        {
            _mockOrderRepository.Setup(x => x.Save(It.IsAny<Order>())).Verifiable();

            var orderService = new CustomerOrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object, new VatService());

            var result = orderService.PlaceOrder(new Order { Amount = 0, OrderId = 1 }, 1);

            Assert.False(result);
        }

        [Fact]
        public void PlaceOrder_Exception()
        {
            _mockOrderRepository.Setup(x => x.Save(It.IsAny<Order>())).Throws(new ApplicationException());
            _mockCustomerRepository.Setup(x => x.LoadCustomer(It.IsAny<int>())).Returns(new Customer { CustomerId = 1, Country = "UK" });

            var orderService = new CustomerOrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object, new VatService());
            var result = orderService.PlaceOrder(new Order { Amount = 100, OrderId = 1 }, 1);

            Assert.False(result);
        }



        [Fact]
        public void LoadCustomersTest()
        {
            _mockOrderRepository.Setup(x => x.LoadAll()).Returns(_orders);
            _mockCustomerRepository.Setup(x => x.LoadAllCustomers()).Returns(_customers);

            var orderService = new CustomerOrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object, new VatService());
            var result = orderService.LoadAllCustomers();

            Assert.Equal(2, result[0].Orders.Count);
        }

        [Fact]
        public void LoadCustomersTest_Exception()
        {
            _mockCustomerRepository.Setup(x => x.LoadAllCustomers()).Throws(new ApplicationException());

            var orderService = new CustomerOrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object, new VatService());
            var result = orderService.LoadAllCustomers();

            Assert.Null(result);
        }

        [Fact]
        public void LoadOrdersTest_Success()
        {
            _mockOrderRepository.Setup(x => x.LoadAll()).Returns(_orders);

            var orderService = new CustomerOrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object, new VatService());
            var result = orderService.LoadOrders();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void LoadOrdersTest_Exception()
        {
            _mockOrderRepository.Setup(x => x.LoadAll()).Throws(new ApplicationException());

            var orderService = new CustomerOrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object, new VatService());
            var result = orderService.LoadOrders();

            Assert.Null(result);
        }
    }
}
