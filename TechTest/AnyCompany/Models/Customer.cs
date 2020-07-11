using System;

namespace AnyCompany.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }
    }
}
