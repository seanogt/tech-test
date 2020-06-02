using System;

namespace AnyCompany.Models
{
    public class CustomerModel
    {
        internal int CustomerId { get; set; } //note, using an int for an id in a system like this where data is distributed among multiple databases is bad. Consider using a GUID instead
        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }
    }
}
