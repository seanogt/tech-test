using System.Collections.Generic;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository = new OrderRepository();
        private readonly Utils _utils = new Utils();
        public bool PlaceOrder(Order order, Customer customer)
        {
            bool results = false;
            
            order.VAT = (customer?.Country == "UK") ? 0.2d : 0;

            if (string.IsNullOrEmpty(_utils.ValidateModel(order)))
            {
                _orderRepository.Save(order);
                results = true;
            }
           
            return results;
        }

        public IEnumerable<Order> GetCustomerOrders()
        {
            var ordersList = _orderRepository.GetAllOrders();

            return ordersList;
        }
    }
}
