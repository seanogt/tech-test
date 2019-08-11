using System.Collections.Generic;
using AnyCompany.Abstractions;
using AnyCompany.Domain;
using AnyCompany.Services.Repositories;

namespace AnyCompany.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private int _customerId;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order PlaceOrder(Order order, int customerId)
        {
            _customerId = customerId;

            if (Customer == null || order.Amount == 0d)
            {
                return null;
            }

            order.Customer = _orderRepository.GetCustomer(_customerId);

            order.VAT = Customer.Country.ToLower() == "uk" ? 0.2d : 0;

           _orderRepository.Save(order);

            return order;
        }

        public Customer Customer => CustomerRepository.Load(_customerId);

        public IEnumerable<Order> GetAlOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public IEnumerable<Customer> GetAlCustomers()
        {
            return _orderRepository.GetAllCustomers();
        }
    }
}
