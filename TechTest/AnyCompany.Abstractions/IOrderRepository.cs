using System.Collections.Generic;
using AnyCompany.Domain;

namespace AnyCompany.Abstractions
{
    public interface IOrderRepository
    {
        void Save(Order order);
        void Save(Customer customer);
        IEnumerable<Order> GetAllOrders();
        Customer GetCustomer(int customerId);
        IEnumerable<Customer> GetAllCustomers();
    }
}