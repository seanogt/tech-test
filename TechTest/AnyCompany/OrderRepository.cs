using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace AnyCompany
{
    internal class OrderRepository
    {       
        private readonly CustomerOrdersContext _CustomerOrderscontext;
        public OrderRepository() : this(new CustomerOrdersContext())
        {
        }

        private OrderRepository(CustomerOrdersContext context)
        {
            _CustomerOrderscontext = context;

        }
       
        public void Save(Order order)
        {            
            try
            {
                _CustomerOrderscontext.Orders.Add(order);
                _CustomerOrderscontext.SaveChanges();                
            }
            catch (Exception ex)
            {
                //log exception in the global filter
                throw ex;
            }            
        }
        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> ordersList = new List<Order>();
            
            try
            {
                _CustomerOrderscontext.Configuration.LazyLoadingEnabled = false;
                ordersList = _CustomerOrderscontext.Orders.Include("Customer").ToList();
                
                return ordersList;
            }
            catch (Exception ex)
            {
                //log exception in the global filter
                throw ex;
            }        

        }
    }
}
