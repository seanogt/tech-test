using System;
using System.Data.SqlClient;
using AnyCompany.Models;
using AnyCompany.AnyCompanyContext;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace AnyCompany.Repositories.OrderRepository
{
    internal class OrderRepository : IOrderRepository<Order>
    {

        private CompanyContext _anycompanycontext;

        public OrderRepository()
        {

        }

        public OrderRepository(CompanyContext context)
        {
            _anycompanycontext = context;
        }

        public IEnumerable<Order> GetCustomerOrders(int id)
        {
            var result = _anycompanycontext.Orders.Select(x => new { x.CustomerId, x.Amount, x.OrderId }).Where(x => x.CustomerId == id);

            return (List<Order>)result;
        }

        public IEnumerable<Order> GetAllOrders()
        {

            return _anycompanycontext.Orders.ToList();

        }

        public bool Save(Order order)
        {
            foreach (var item in order.GetType().GetProperties())
            {
                if (item.PropertyType == typeof(string) && item.Equals(""))
                {
                    throw new ArgumentException();
                } 
                else if (item.PropertyType == typeof(int) && item.Equals(0))
                {
                    throw new ArgumentException();
                }
            }

            try
            {
                using (var context = new CompanyContext())
                {
                    context.Add(order);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
