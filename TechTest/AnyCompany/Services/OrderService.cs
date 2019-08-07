using AnyCompany.Repositories;
using AnyCompany.Services;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;

        public OrderService()
        {

        }

        public OrderService(IOrderRepository orderRepo, ICustomerRepository custRepo)
        {
            _orderRepository = orderRepo;
            _customerRepository = custRepo;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = LoadACustomerFromCustomerRepository(customerId);

            if (!order.IsValid())
                return false;

           
            if(!SaveOrderToOrderRepository(order.SetVAT(customer)))
                return false;

            return true;
        }

        protected virtual bool SaveOrderToOrderRepository(Order order)
        {
            return _orderRepository.Save(order);
        }

        protected virtual Customer LoadACustomerFromCustomerRepository(int customerId)
        {
            Customer customer = _customerRepository.Load(customerId);
            return customer;
        }


        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

     
        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            var orders = GetOrders();
            return orders.Where(p => p.CustomerId == customerId);
        }
    }
}
