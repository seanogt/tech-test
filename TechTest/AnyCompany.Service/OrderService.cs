using AnyCompany.Data;
using AnyCompany.Model;
using AnyCompany.Service.Requests;

namespace AnyCompany.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Customer> _customerRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<Customer> customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public bool PlaceOrder(PlaceOrderRequest request)
        {
            if (request.Amount == 0)
            {
                return false;
            }

            var customer = _customerRepository.Load(request.CustomerId);

            if (customer == null)
            {
                return false;
            }

            var vat = customer.Country == "UK" ? 0.2d : 0;

            var order = new Order
            {
                Amount = request.Amount,
                CustomerId = request.CustomerId,
                VAT = vat
            };

            _orderRepository.Add(order);

            return true;
        }
    }
}
