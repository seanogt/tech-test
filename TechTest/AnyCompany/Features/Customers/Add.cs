using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Repositories;

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
        public Validator(DataContext context)
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .Must(name => NameValidator.ValidateName(name, Collection.AllowedNameCharacters))
                .WithMessage(Collection.AllowedNameCharactersMessage)
                .Must(name => !context.Collections.Any(c => c.Name == name))
                .WithMessage(ValidationMessages.Collection.AlreadyExists);
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        //todo: add tests
        public void Handle(Command message)
        {
            var customer = new Customer
            {
                Name = message.Name,
                Country = message.Country,
                DateOfBirth = message.DateOfBirth
            };

            CustomerRepository.Add(customer);
        }
    }
}
