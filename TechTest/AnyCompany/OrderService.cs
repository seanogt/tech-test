using AnyCompany.Interfaces;
using AnyCompany.Models;
using AnyCompany.Repository;
using System;
using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService: IOrderService
    {
        public Customer GetCustomer(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }

        public List<Order> GetOrders()
        {
            using (OrderRepository orderRepository = new OrderRepository())
            {
                return orderRepository.GetOrders();
            }
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            try
            {
                using (OrderRepository orderRepository = new OrderRepository())
                {
                    Customer customer = GetCustomer(customerId);

                    if (order.Amount == 0)
                        return false;

                    if (customer.Country == "UK")
                        order.VAT = 0.2d;
                    else
                        order.VAT = 0;

                    orderRepository.Save(order);

                    return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
