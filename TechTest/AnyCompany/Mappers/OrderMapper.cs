using AnyCompany.Dtos;
using AnyCompany.Models;

namespace AnyCompany.Mappers
{
    public static class OrderMapper
    {
        public static Order Map(OrderDto orderDto)
        {
            return new Order
            {
                OrderId = orderDto.OrderId,
                Amount = orderDto.Amount,
                VAT = orderDto.VAT
            };
        }
    }
}
