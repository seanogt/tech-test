using System;
using System.Collections.Generic;

namespace AnyCompany.Service.DTOs
{
    public class CustomerDto
    {
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }

        public List<OrderDto> Orders { get; set; }
    }
}
