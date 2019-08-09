using System;

namespace AnyCompany.Services.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }
    }
}
