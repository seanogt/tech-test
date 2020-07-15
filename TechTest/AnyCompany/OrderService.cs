using AnyCompany.Models;
using AnyCompany.Repositories.OrderRepository;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AnyCompany
{
    public class OrderService
    {

        private IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool PlaceOrder(Order order, int customerId)
        {

            var customerRepo = _unitOfWork.CustomerRepository.GetCustomerDetails(customerId);
            

            if (order.Amount == 0 || customerId < 1)
                return false;
            var temp = Enumerable.Cast<Customer>(customerRepo);
            var cst = new Customer();
            foreach (var item in customerRepo)
            {
                order.Address = item.Address;
                order.CustomerAccountNumber = item.CustomerAccountNumber;
                order.CustomerId = customerId;
                cst.Address.Country = item.Address.Country;
            }


            if (order.Address.Country.ToUpper().Equals("UK"))
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            _unitOfWork.Save(order);

            return true;
        }



        public IEnumerable<Order> GetOrders()
        {
            var ords = _unitOfWork.OrderRepository.GetAllOrders();
            return ords;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var cust = _unitOfWork.CustomerRepository.GetAllCustomers();

            return cust;
        }


        public IEnumerable<CustomOrdersSet> GetCustomersWithOrders()
        {
            var customers = this.GetCustomers();
            var orders = this.GetOrders();
            var result = from c in customers
                         join o in orders
                         on c.CustomerAccountNumber equals o.CustomerAccountNumber
                         orderby c.CustomerAccountNumber
                         select (new CustomOrdersSet
                         {
                             AccountNumber = c.CustomerAccountNumber,
                             Address = c.Address,
                             Amount = o.Amount,
                             EmailAddress = c.EmailAddress,
                             FirstName = c.FirstName,
                             Lastname = c.LastName,
                             OrderNumber = o.OrderId,
                             Vat = o.VAT
                         });

            return result;

        }
    }
}
