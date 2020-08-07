using System;
using System.Collections.Generic;
using AnyCompany;
using AnyCompany.BUL.Services;
using AnyCompany.DAL;
using Moq;
using NUnit.Framework;

namespace InvestecUnitTests
{
    [TestFixture]
    public class OrderServiceTests 
    {
        private Mock<IOrderRepository> _orderService = new Mock<IOrderRepository>();
        private Mock<ICustomerRepository> _customerService = new Mock<ICustomerRepository>();
        private OrderService orderService;
        private List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    Name = "Mike America", Country = "US", DateOfBirth = DateTime.Parse("1987-06-01"), CustomerId = 1
                },
                       new Customer
                {
                    Name = "Jonathan Canada", Country = "CN", DateOfBirth = DateTime.Parse("1925-06-01"), CustomerId = 2
                },
                              new Customer
                {
                    Name = "FROM THE UK CHINKU", Country = "UK", DateOfBirth = DateTime.Parse("2001-06-01"), CustomerId = 3
                }
                              ,
                              new Customer
                {
                    Name = "Not in Database Adams", Country = "AG", DateOfBirth = DateTime.Parse("1982-12-12"), CustomerId = 50
                }
            };
        private readonly Order validOrder =  new Order
        {
            Amount = 500,
            CustomerId = 1,
            VAT = 50
        };
        private readonly Order invalidOrder = new Order
        {
            Amount = -89,
            VAT = 50
        };

        [TestCase]
        public void GIVEN_VALIDORDER_RETURNS_TRUE()
        {
            //Arrange
            Customer NonUKCustomer = customers[0];
            validOrder.CustomerId = NonUKCustomer.CustomerId;
            _orderService.Setup(x => x.Save(validOrder)).Returns(true);
            _customerService.Setup(x => x.Get(validOrder.CustomerId)).Returns(NonUKCustomer);

            //Act
            orderService = new OrderService(_orderService.Object,_customerService.Object);
            bool orderResult = orderService.PlaceOrder(validOrder, validOrder.CustomerId);

            //Asset
            Assert.AreEqual(orderResult, true);
        }

        [TestCase]
        public void GIVEN_INVALIDORDERAMOUNT_RETURNS_FALSE()
        {
            //Arrange
            Customer NonUKCustomer = customers[0];
            validOrder.CustomerId = NonUKCustomer.CustomerId;
            _orderService.Setup(x => x.Save(invalidOrder)).Returns(false);

            //Act
            orderService = new OrderService(_orderService.Object, _customerService.Object);
            bool orderResult = orderService.PlaceOrder(validOrder, validOrder.CustomerId);

            //Asset
            Assert.AreEqual(orderResult, false);
        }

        [TestCase]
        public void GIVEN_UKCUSTOMER_APPLIES_CORRECT_VAT_AMOUNT_RETURNS_TRUE()
        {
            //Arrange
            Customer UKcustomer = customers[2];
            validOrder.CustomerId = UKcustomer.CustomerId;
            _orderService.Setup(x => x.Save(validOrder)).Returns(true);
            _customerService.Setup(x => x.Get(validOrder.CustomerId)).Returns(UKcustomer);

            //Act
            orderService = new OrderService(_orderService.Object, _customerService.Object);
            bool orderResult = orderService.PlaceOrder(validOrder, validOrder.CustomerId);

            //Asset
            Assert.AreEqual(orderResult, true);
        }
    }
}
