using System;
using System.Collections.Generic;

namespace AnyCompany.Models
{
    /// <summary>
    /// This is the Customer Model
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }

        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
