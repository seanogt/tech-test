using System.Collections.Generic;
using System.Linq;
using AnyCompany.Abstractions;
using AnyCompany.Domain;
using AnyCompany.Repository;

namespace AnyCompany.Services.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _ordersDbContext;

        public OrderRepository(OrdersDbContext ordersDbContext)
        {
            _ordersDbContext = ordersDbContext;
        }

        public void Save(Order order)
        {
            _ordersDbContext.Orders.Add(order);
            _ordersDbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ordersDbContext.Orders.OrderBy(x => x.CustomersId).ToList();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _ordersDbContext.Customers.ToList();
        }

        public void Save(Customer customer)
        {
            _ordersDbContext.Customers.Add(customer);
            _ordersDbContext.SaveChanges();
        }

        public Customer GetCustomer(int customerId)
        {
            return _ordersDbContext.Customers?.FirstOrDefault(x => x.CustomerId == customerId);
        }
    }
}
