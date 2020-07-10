using System;
using AnyCompany.IServices;
using Xunit;

namespace AnyCompany.Tests
{
    public class PlaceOrderTests
    {
        private readonly IOrdersService orderService;
        private readonly ICustomerService customerService;

        public PlaceOrderTests(IOrdersService _orderService, ICustomerService _customerService)
        {
            this.orderService = _orderService;
            this.customerService = _customerService;
        }

        //Amount check
        [Fact]
        public void PlaceOrder_AmountGreaterThanZero_ReturnTrue()
        {
            Order order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                Amount = 10
            };

            var result = orderService.PlaceOrder(order, order.CustomerId);
            Assert.True(result);
        }

        [Fact]
        public void PlaceOrder_AmountZero_ReturnFalse() {
            Order order = new Order {
                OrderId = 1,
                CustomerId = 1,
                Amount = 0
            };

            var result = orderService.PlaceOrder(order, order.CustomerId);
            Assert.False(result, "Amount should be greater than zero");
        }

        [Fact]
        public void PlaceOrder_AmountNegative_ReturnFalse()
        {
            Order order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                Amount = -10
            };

            var result = orderService.PlaceOrder(order, order.CustomerId);
            Assert.False(result, "Amount should be greater than zero");
        }


        //Vat check
        [Fact]
        public void PlaceOrder_CountryNotUk_ReturnTrue() {
            decimal VAT = 0.0m;
            Customer customer = new Customer {
                Name = "XAEA-12",
                CustomerId = 1,
                Country = "South Africa",
                DateOfBirth = DateTime.Now.AddYears(-25),             
            };

            customerService.SaveCustomer(customer);

            Order order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                Amount = 10
            };

            orderService.PlaceOrder(order, order.CustomerId);
            var _order = orderService.GetOrder(order.OrderId);

            var result = _order.VAT == VAT; 

            Assert.True(result);
        }

        [Fact]
        public void PlaceOrder_CountryNotUk_ReturnFalse()
        {
            decimal VAT = 0.0m;
            Customer customer = new Customer
            {
                Name = "XAEA-12",
                CustomerId = 1,
                Country = "South Africa",
                DateOfBirth = DateTime.Now.AddYears(-25),
            };

            customerService.SaveCustomer(customer);

            Order order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                Amount = 10
            };

            orderService.PlaceOrder(order, order.CustomerId);
            var _order = orderService.GetOrder(order.OrderId);

            var result = _order.VAT != VAT;

            Assert.False(result, "If country is not UK, VAT must be set to 0");
        }

        [Fact]
        public void PlaceOrder_CountryUk_ReturnTrue()
        {
            decimal VAT = 0.2m;
            Customer customer = new Customer
            {
                Name = "XAEA-12",
                CustomerId = 1,
                Country = "UK",
                DateOfBirth = DateTime.Now.AddYears(-25),
            };

            customerService.SaveCustomer(customer);

            Order order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                Amount = 10
            };

            orderService.PlaceOrder(order, order.CustomerId);
            var _order = orderService.GetOrder(order.OrderId);

            var result = _order.VAT == VAT;

            Assert.True(result);
        }
    }
}
