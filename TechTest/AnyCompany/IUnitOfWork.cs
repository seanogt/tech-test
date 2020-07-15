using AnyCompany.Models;
using AnyCompany.Repositories.CustomerRepository;
using AnyCompany.Repositories.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public interface IUnitOfWork
    {
        IOrderRepository<Order> OrderRepository { get; }

        ICustomerRepository<Customer> CustomerRepository { get; }

        IEnumerable<Customer> GetAllCustomers();

        IEnumerable<Order> GetAllOrders();

        IEnumerable<Order> GetAllCustomerOrders(int customerid);

        bool Save(Order order);
    }
}
