using AnyCompany.AnyCompanyContext;
using AnyCompany.Models;
using AnyCompany.Repositories.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using AnyCompany;
using AnyCompany.Repositories.CustomerRepository;

namespace AnyCompany
{
    public class UnitOfWork : IUnitOfWork
    {
        private IOrderRepository<Order> _orderRepository;
        private ICustomerRepository<Customer> _customerRepository;
        private IAnyCompanyContext _anycontext;


        public UnitOfWork(IAnyCompanyContext context, IOrderRepository<Order> orderRepository,ICustomerRepository<Customer> customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _anycontext = context;
        }

        IOrderRepository<Order> IUnitOfWork.OrderRepository => throw new NotImplementedException();

        ICustomerRepository<Customer> IUnitOfWork.CustomerRepository => throw new NotImplementedException();

        public IEnumerable<Customer> GetAllCustomers()
        {
            var results = _customerRepository.GetAllCustomers();
            return results;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var results = _orderRepository.GetAllOrders();
            return new List<Order>();
        }

        public IEnumerable<Order> GetAllCustomerOrders(int customerid)
        {
            var results = _orderRepository.GetCustomerOrders(customerid);
            return results;
        }

        public bool Save(Order order)
        {
            try
            {
                _orderRepository.Save(order);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}