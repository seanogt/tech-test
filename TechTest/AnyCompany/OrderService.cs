using System.Collections.Generic;
using System.Linq;
using AnyCompany.DAL;
using AnyCompany.Domain;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();
        public bool PlaceOrder(Order order)
        {
            Customer customer = CustomerRepository.Load(order.CustomerId);
            if (order.Amount == 0)
                return false;
            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;
            orderRepository.Save(order);
            return true;
        }

        public IEnumerable<Customer> GetCustomerOrders()
        {
            List<Order> orders = orderRepository.SelectAll().ToList();
            List<Customer> customers = CustomerRepository.LoadAll().ToList();
            foreach (Customer customer in customers)
            {
                customer.Orders = orders.Where(o => o.CustomerId == customer.CustomerId).ToList();
            }
            return customers;
        }

        public Customer GetCustomerOrders(int customerId)
        {
            List<Order> orders = orderRepository.SelectAll().ToList();
            Customer customer = CustomerRepository.Load(customerId);
            customer.Orders = orders.Where(o => o.CustomerId == customer.CustomerId).ToList();
            return customer;
        }
    }

}