using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Models;
using AnyCompany.Services.Dtos;
using AnyCompany.Services.Mappers;
using AnyCompany.Services.Services;

namespace AnyCompany.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public bool PlaceOrder(OrderDto orderDto, int customerId)
        {
            Customer customer = _customerRepository.Load(customerId);

            if (orderDto.Amount == 0)
                return false;

            if (customer.Country == "UK")
                orderDto.VAT = 0.2d;
            else
                orderDto.VAT = 0;

            var order = OrderMapper.Map(orderDto);
            order.CustomerId = customerId;

            _orderRepository.Add(order);

            return true;
        }
    }
}
