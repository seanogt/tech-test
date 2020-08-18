﻿using AnyCompany.Interfaces;
using AnyCompany.Models;
using AnyCompany.Repository;
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
            throw new System.NotImplementedException();
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            using (OrderRepository orderRepository = new OrderRepository())
            {
                Customer customer = CustomerRepository.Load(customerId);

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
    }
}
