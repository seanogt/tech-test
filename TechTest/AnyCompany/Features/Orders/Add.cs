using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnyCompany.Models;
using AnyCompany.Repositories;
using AnyCompany.Services;
using FluentValidation;
using MediatR;

namespace AnyCompany.Features.Orders
{
    public class AddCommand : IRequest
    {
        public int CustomerId { get; }
        public double Amount { get; }
    }

    public class Validator : AbstractValidator<AddCommand>
    {
        public Validator()
        {
            //todo
            //validate user id - must exists
            //validate product id - must exits
        }
    }

    public class Handler : IRequestHandler<AddCommand>
    {
        private readonly OrderService _OrderService;

        public Handler(OrderService orderService)
        {
            _OrderService = orderService;
        }

        //todo: add tests
        public Task<Unit> Handle(AddCommand message, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                Amount = message.Amount
            };

            _OrderService.PlaceOrder(order, message.CustomerId);

            return Unit.Task;
        }
    }
}
