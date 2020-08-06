using AnyCompany.DataAccessLayer;
using AnyCompany.Entities;
using AnyCompany.Models;
using AnyCompany.RepositoryLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.RepositoryLayer
{
    public class UnitofWork : IUnitofWork
    {
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly ISaveOrderRepository<Order> _saveOrderRepository;
        private readonly CompanyContext _companyContext;
        public UnitofWork(IOrderRepository<Order> orderRepository, CompanyContext companyContext, ISaveOrderRepository<Order> saveOrderRepository)
        {
            _companyContext = companyContext;
            _orderRepository = orderRepository;
            _saveOrderRepository = saveOrderRepository;
        }

        public Customer GetCustomer(int customerId)
        {
            return _companyContext.Customers.Single(c => c.Id == customerId);
        }

        public CustomerOrderSet GetCustomerOrderSets(int cutomerId)
        {
            return _orderRepository.GetCustomerOrders(cutomerId);
        }

        public List<CustomerOrderSet> GetCustomersWithOrders()
        {
            List<CustomerOrderSet> customerOrderSets = new List<CustomerOrderSet>();
            List<Order> orders = new List<Order>();

            var candidateCustomerOrders = from co in _companyContext.CustomerOrders
                                          join o in _companyContext.Orders on
                                          co.OrderId equals o.Id
                                          join c in _companyContext.Customers on 
                                          co.CustomerId equals c.Id
                                          select new
                                          {
                                              customerId = co.Id,
                                              customerName = c.Name,
                                              customerSurname = c.Surname,
                                              customerEmail = c.Email,
                                              oId = o.Id,
                                              oNumber = o.OrderNumber,
                                              oAmnt = o.Amount,
                                              oVat = o.VAT
                                          };
            foreach (var item in candidateCustomerOrders)
            {
                var customerOrderSet = new CustomerOrderSet
                {
                    Name = item.customerName,
                    Surname = item.customerSurname,
                    Email = item.customerEmail,
                    Orders = candidateCustomerOrders.Where(c => c.customerId == item.customerId).Select(x => new Order
                    {
                        Amount = x.oAmnt,
                        OrderNumber = x.oNumber,
                        VAT = x.oVat
                    }).ToList()
                };

                if (!customerOrderSets.Contains(customerOrderSet))
                    customerOrderSets.Add(customerOrderSet);
            };

            return customerOrderSets;
        }

        public bool SaveOrder(NewOrder newOrder)
        {
            return _saveOrderRepository.SaveOrder(newOrder);
        }
    }
}
