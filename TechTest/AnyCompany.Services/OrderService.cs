using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Models;
using AnyCompany.Services.Dtos;
using AnyCompany.Services.Exceptions;
using AnyCompany.Services.Helpers;
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
            // load the customer
            Customer customer = _customerRepository.Load(customerId);
            if (customer == null)
                throw new CustomerNotFoundException();

            // do not proceed with order if amount is 0
            if (orderDto.Amount == 0)
                return false;

            // get the appropriate vat for the customers country
            orderDto.VAT = VatCalculator.GetVat(customer.Country);

            // build and save the order
            var order = OrderMapper.Map(orderDto);
            order.CustomerId = customerId;

            _orderRepository.Add(order);

            return true;
        }
    }
}
