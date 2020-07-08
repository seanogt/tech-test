using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnyCompany
{
    public class Customer
    {
        public int Id { get; set; }
        [Required, MinLength(2),MaxLength(10)]
        public string Country { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required,MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        public List<Order> orders { get; set; }
    }
}
