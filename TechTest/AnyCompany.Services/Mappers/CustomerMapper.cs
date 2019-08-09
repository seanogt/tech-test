using AnyCompany.Models;
using AnyCompany.Services.Dtos;

namespace AnyCompany.Services.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto Map(Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Country = customer.Country,
                DateOfBirth = customer.DateOfBirth,
                Name = customer.Name
            };
        }
    }
}