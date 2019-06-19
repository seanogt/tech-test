using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepositoryService _customerRepositoryService;
        private readonly IVatService _vatService;

        public CustomerOrderService(IOrderRepository orderRepository
            , ICustomerRepositoryService customerRepositoryService
            , IVatService vatService)
        {
            _orderRepository = orderRepository;
            _customerRepositoryService = customerRepositoryService;
            _vatService = vatService;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            try
            {
                if (order.Amount == 0)
                    return false;

                var customer = _customerRepositoryService.LoadCustomer(customerId);

                order.VAT = _vatService.GetVatAmount(customer.Country);
                _orderRepository.Save(order);

                return true;
            }
            catch (Exception ex)
            {
                //log exception
                return false;
            }
        }
        
        public List<CustomerOrder> LoadAllCustomers()
        {
            try
            {
                var customers = _customerRepositoryService.LoadAllCustomers();

                var customerOrders = new List<CustomerOrder>();
                customers?.ForEach(a => customerOrders.Add(new CustomerOrder(a)));

                var orders = LoadOrders();
                customerOrders.ForEach(a =>
                {
                    a.Orders = new List<Order>();
                    a.Orders.AddRange(orders?.Where(b => b.CustomerId == a.CustomerId));
                });

                return customerOrders;
            }
            catch (Exception ex)
            {
                //log exception
                return null;
            }
        }

        public List<Order> LoadOrders()
        {
            try
            {
                return _orderRepository.LoadAll();
            }
            catch (Exception ex)
            {
                //log exception
                return null;
            }
        }
    }
}
