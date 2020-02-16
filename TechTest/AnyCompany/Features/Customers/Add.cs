using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Repositories;
using FluentValidation;
using MediatR;
using System.Threading;

namespace AnyCompany.Features.Customers
{
    public class AddCommand : IRequest
    {
        public string Country { get; }

        public DateTime DateOfBirth { get; }

        public string Name { get; }

        public AddCommand(string country, DateTime dateOfBirth, string name)
        {
            Country = country;
            DateOfBirth = dateOfBirth;
            Name = name;
        }
    }

    public class Validator : AbstractValidator<AddCommand>
    {
        public Validator()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .Matches(@"")
                .Length(50);
            //todo: add `Must not exists` validation
        }
    }

    public class Handler : IRequestHandler<AddCommand>
    {
        //todo: add tests
        public Task<Unit> Handle(AddCommand message, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = message.Name,
                Country = message.Country,
                DateOfBirth = message.DateOfBirth
            };

            CustomerRepository.Add(customer);

            return Unit.Task;
        }
    }
}
