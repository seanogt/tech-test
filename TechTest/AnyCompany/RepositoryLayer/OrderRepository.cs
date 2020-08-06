using AnyCompany.DataAccessLayer;
using AnyCompany.Entities;
using AnyCompany.Models;
using AnyCompany.RepositoryLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.RepositoryLayer
{
    internal class OrderRepository : IOrderRepository<Order>, ISaveOrderRepository<Order>
    {

        private readonly CompanyContext _companyContext;

        public OrderRepository(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }
        public CustomerOrderSet GetCustomerOrders(int customerId)
        {
            List<Order> orders = new List<Order>();
            var customer = _companyContext.Customers.Single(c => c.Id == customerId);
            var candidateCustomerOrders = from c in _companyContext.CustomerOrders
                                          join o in _companyContext.Orders on
                                          c.OrderId equals o.Id
                                          select new { 
                                              customerId = c.Id, 
                                              oId = o.Id, 
                                              oNumber = o.OrderNumber, 
                                              oAmnt = o.Amount, 
                                              oVat = o.VAT };

            foreach (var item in candidateCustomerOrders)
            {
                if (item.customerId == customerId)
                    orders.Add(new Order
                    {
                        Id = item.oId,
                        OrderNumber = item.oNumber,
                        Amount = item.oAmnt,
                        VAT = item.oVat
                    });
            }
            

            return new CustomerOrderSet
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                Orders = orders
            };
        }

        public IEnumerable<Order> GetOrders()
        {
            return _companyContext.Orders.AsEnumerable();
        }

        public bool SaveOrder(NewOrder newOrder)
        {
            var orderSaved = false;
            var order = new Order
            {
                Amount = newOrder.Amount,
                OrderNumber = newOrder.OrderNumber,
                VAT = newOrder.VAT
            };

            try
            {
                _companyContext.Orders.Add(order);
                _companyContext.SaveChanges();
            }
            catch
            {
                orderSaved = false;
            }

            var latestOrder = _companyContext.Orders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber).Id;

            var customerOrder = new CustomerOrder
            {
                OrderId = latestOrder,
                CustomerId = newOrder.CustomerId,
                Completed = false
            };

            try
            {
                _companyContext.CustomerOrders.Add(customerOrder);
                _companyContext.SaveChanges();
                orderSaved = true;
            }
            catch
            {
                orderSaved = false;
            }

            return orderSaved;
        }
    }
}
