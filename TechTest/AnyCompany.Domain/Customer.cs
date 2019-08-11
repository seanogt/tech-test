using System;
using System.Collections.Generic;

namespace AnyCompany.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
