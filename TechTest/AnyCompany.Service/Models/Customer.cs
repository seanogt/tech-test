using System;

namespace AnyCompany.Service.Models
{
    public class Customer
    {
        public Customer(string country, DateTime dateOfBirth, string name)
        {
            this.Country = country;
            this.DateOfBirth = dateOfBirth;
            this.Name = name;
        }
        public string Country { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Name { get; private set; }
    }
}
