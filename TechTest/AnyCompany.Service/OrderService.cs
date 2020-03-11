using AnyCompany.Entities;
using AnyCompany.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Service
{
    public static class OrderService
    {
        public static bool PlaceOrder(Order order, out string message)
        {
            message = "";
            if (order.CustomerId <= 0)
            {
                message = "Please select a customer.";
                return false;
            }

            Customer customer = CustomerRepository.Load(order.CustomerId);

            if (customer.CustomerId <= 0)
            {
                message = $"Customer id {order.CustomerId} not found.";
                return false;
            }

            if (order.Amount <= 0)
            {
                message = "Amount can't be zero.";
                return false;
            }

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            OrderRepository.Save(order);

            return true;
        }

        public static List<Order> LoadByCustomer(int customerId, out string message)
        {
            message = "";
            List<Order> orderList = new List<Order>();
            if (customerId <= 0)
            {
                message = "Please select a customer.";
                return null;
            }

            orderList = OrderRepository.LoadByCustomer(customerId);

            return orderList;
        }
    }
}
