using System.Collections.Generic;
using System.Linq;
using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Services.Dtos;
using AnyCompany.Services.Mappers;

namespace AnyCompany.Services.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerOrderService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public IEnumerable<CustomerOrdersDto> GetAllCustomerWithOrders()
        {
            var customerOrderDtos = new List<CustomerOrdersDto>();

            // load data
            var customers = _customerRepository.GetList();
            var orders = _orderRepository.GetList();

            // map orders to the appropriate customer and return a dto
            foreach (var customer in customers)
            {
                var customerOrderDto = new CustomerOrdersDto
                {
                    Customer = CustomerMapper.Map(customer),
                    Orders = orders.Where(o => o.CustomerId == customer.CustomerId).Select(OrderMapper.Map)
                };

                customerOrderDtos.Add(customerOrderDto);
            }

            return customerOrderDtos;
        }
    }
}
