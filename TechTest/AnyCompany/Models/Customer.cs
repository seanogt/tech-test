using System;

namespace AnyCompany.Models
{
    public class Customer : Base
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public int ContactNumber { get; set; }

    }
}
