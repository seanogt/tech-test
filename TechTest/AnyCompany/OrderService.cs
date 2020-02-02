using AnyCompany.Model;
using System;
using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public List<Customer> LoadAllCustomersAndOrders()
        {
            try
            {
                List<Customer> customers = CustomerRepository.LoadAllCustomers();

                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load customers.", ex);
            }
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            if (order == null)
                throw new ArgumentNullException();

            try
            {
                Customer customer = CustomerRepository.Load(customerId);

                if (order.Amount == 0)
                    return false;

                if (customer == null)
                    return false;

                switch (customer.Country)
                {
                    case "UK":
                        order.VAT = 0.2d;
                        break;
                    default:
                        order.VAT = 0;
                        break;
                }

                orderRepository.Save(order, customerId);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save order for customer with customer ID {customerId}", ex);
            }
        }
    }
}
