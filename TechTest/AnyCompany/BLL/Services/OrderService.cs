using AnyCompany.BLL.Helpers;
using AnyCompany.DAL.Interfaces;
using AnyCompany.DAL.Models;
using System;

namespace AnyCompany.BLL.Services
{
    //Creating DataSource not responsibilty of Order Service
    public class OrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        //Business Rules moved to central location                                               
        public bool PlaceOrder(Order order, int customerId)
        {
            if (!BusinessRulesHelper.IsValidOrderAmount(order.Amount))
            {
                //Some DI logger here
                Console.WriteLine("Order Amount should be greater than 0.");
                return false;
            }

            Customer customer = _customerRepository.Get(customerId);
            if (_customerRepository.Get(customerId) == null)
            {
                //Some DI logger here
                Console.WriteLine("No Customer found.");
                return false;
            }
            else
            {
                order.VAT = BusinessRulesHelper.DetermineVat(customer.Country);
            }

            return _orderRepository.Save(order);
        }
    }
}
