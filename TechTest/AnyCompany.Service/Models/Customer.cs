using System;

namespace AnyCompany.Service.Models
{
    public class Customer
    {
        public Customer(string customerId, string country, DateTime dateOfBirth, string name)
        {
            this.CustomerId = customerId;
            this.Country = country;
            this.DateOfBirth = dateOfBirth;
            this.Name = name;
        }
        
        public string CustomerId { get; private set; }
        public string Country { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Name { get; private set; }
    }
}
