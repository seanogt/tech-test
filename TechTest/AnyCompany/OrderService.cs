using AnyCompany.Entities;
using AnyCompany.Models;
using AnyCompany.RepositoryLayer;
using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService : IOrderService
    {
        private readonly UnitofWork _unitofWork;

        public OrderService(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public bool PlaceOrder(Order order, int customerId)
        {
            var customer = _unitofWork.GetCustomer(customerId);

            if (order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            var newOrder = _unitofWork.SaveOrder(
                new NewOrder
                {
                    Amount = order.Amount,
                    CustomerId = customerId,
                    OrderNumber = order.OrderNumber,
                    VAT = order.VAT
                });

            return newOrder;
        }

        public CustomerOrderSet GetCustomerOrders(int customerId)
        {
            return _unitofWork.GetCustomerOrderSets(customerId);
        }

        public IEnumerable<CustomerOrderSet> GetCustomersWithOrders()
        {
            return _unitofWork.GetCustomersWithOrders();
        }
    }
}
