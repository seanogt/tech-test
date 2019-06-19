using AnyCompany.Context;
using AnyCompany.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyCompany.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CustomerDbContext customerDbContext;

        public UnitOfWork(CustomerDbContext dbContext)
        {
            customerDbContext = dbContext;
        }

        public List<Customer> Customers
        {
            get
            {
                return customerDbContext.Customers.ToList();
            }
        }

        public List<Order> Orders
        {
            get
            {
                return customerDbContext.Orders.ToList();
            }
        }

        public List<VAT> VATs
        {
            get
            {
                return customerDbContext.Vats.ToList();
            }
        }

        public List<CustomerOrder> CustomerOrders
        {
            get
            {
                // Group orders by customer id
                var ord = from o in Orders
                          group o by o.CustomerId into orderGroup
                          select new { CustomerId = orderGroup.Key, Orders = orderGroup.ToList() };

                // Create CustomerOrder mapping object
                var customerOrders = (from c in Customers
                                      join o in ord on c.CustomerId equals o.CustomerId into cog
                                      from p in cog.DefaultIfEmpty()
                                      select new CustomerOrder() { Customer = c, Orders = p == null ? new List<Order>() : p.Orders }).ToList();

                // return final list mapping object
                return customerOrders;
            }
        }

        public async void SaveChangesAsync()
        {
            await customerDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            customerDbContext?.Dispose();
        }

    }
}
