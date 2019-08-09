using System.Collections.Generic;
using System.Linq;
using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Models;
using AnyCompany.Services.Services;
using Moq;
using NUnit.Framework;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class CustomerOrderServiceTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IOrderRepository> _orderRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Test]
        public void GetCustomersWithOrders_ShouldReturnAllOrders()
        {
            // Arrange.
            const int CustomerOneId = 3;
            const int CustomerTwoId = 4;

            var customers = GetCustomers(CustomerOneId, CustomerTwoId);
            _customerRepositoryMock.Setup(r => r.GetList()).Returns(customers);

            var orders = GetOrders(CustomerOneId, CustomerTwoId);
            _orderRepositoryMock.Setup(r => r.GetList()).Returns(orders);

            var customerOrderService = GetService();

            // Act.
            var result = customerOrderService.GetAllCustomerWithOrders();

            // Assert.
            Assert.AreEqual(customers.Count(), result.Count());
            Assert.AreEqual(customers.First().CustomerId, result.First().Customer.CustomerId);
            Assert.AreEqual(customers.First().Country, result.First().Customer.Country);
            Assert.AreEqual(customers.First().DateOfBirth, result.First().Customer.DateOfBirth);
            Assert.AreEqual(customers.First().Name, result.First().Customer.Name);
            Assert.AreEqual(orders.Count(o => o.CustomerId == customers.First().CustomerId), result.First().Orders.Count());
            Assert.AreEqual(orders.First(o => o.CustomerId == customers.First().CustomerId).Amount, result.First().Orders.First().Amount);
            Assert.AreEqual(orders.First(o => o.CustomerId == customers.First().CustomerId).VAT, result.First().Orders.First().VAT);
            Assert.AreEqual(orders.First(o => o.CustomerId == customers.First().CustomerId).OrderId, result.First().Orders.First().OrderId);
        }

        private CustomerOrderService GetService()
        {
            return new CustomerOrderService(
                _customerRepositoryMock.Object,
                _orderRepositoryMock.Object);
        }

        private IEnumerable<Customer> GetCustomers(int idOne, int idTwo)
        {
            return new List<Customer>
            {
                GetCustomer(idOne),
                GetCustomer(idTwo)
            };
        }

        private IEnumerable<Order> GetOrders(int idOne, int idTwo)
        {
            return new List<Order>
            {
                GetOrder(33, idOne),
                GetOrder(43, idTwo),
                GetOrder(43, idOne),
            };
        }

        private Customer GetCustomer(int id)
        {
            return new Customer
            {
                CustomerId = id,
                Name = "John Smith",
                Country = "UK"
            };
        }

        private Order GetOrder(int id, int customerId)
        {
            return new Order
            {
                OrderId = id,
                Amount = 20.99,
                CustomerId = customerId,
                VAT = 0.2d
            };
        }
    }
}
