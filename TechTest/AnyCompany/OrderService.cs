using AnyCompany.DAL;
using AnyCompany.Models;
using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool PlaceOrder(Order order)
        {
            return _orderRepository.Save(order);
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return _orderRepository.GetOrdersByCustomerId(customerId);
        }
    }
}
