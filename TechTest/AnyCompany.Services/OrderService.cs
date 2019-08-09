using AnyCompany.Models;
using AnyCompany.Services.Dtos;
using AnyCompany.Services.Mappers;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(OrderDto orderDto, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            if (orderDto.Amount == 0)
                return false;

            if (customer.Country == "UK")
                orderDto.VAT = 0.2d;
            else
                orderDto.VAT = 0;

            orderRepository.Save(OrderMapper.Map(orderDto));

            return true;
        }
    }
}
