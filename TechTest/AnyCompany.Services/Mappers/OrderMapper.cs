﻿using AnyCompany.Models;
using AnyCompany.Services.Dtos;

namespace AnyCompany.Services.Mappers
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

        public static OrderDto Map(Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                Amount = order.Amount,
                VAT = order.VAT
            };
        }
    }
}
