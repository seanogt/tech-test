using System;
using System.Collections.Generic;

namespace AnyCompany.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
