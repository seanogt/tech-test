using System;
using AnyCompany.Interface;

namespace AnyCompany
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }
    }
}
