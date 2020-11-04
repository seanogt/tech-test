using AnyCompany.Models;
using System;
using System.Collections.Generic;

namespace AnyCompany.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public Customer(Country country)
            : this()
        {
            Country = country;
        }

        public int CustomerId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Order> Orders { get; set; }     
    }
}
