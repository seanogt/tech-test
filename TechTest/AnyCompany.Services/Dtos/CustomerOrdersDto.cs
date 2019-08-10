using System.Collections.Generic;
using AnyCompany.Models;

namespace AnyCompany.Services.Dtos
{
    public class CustomerOrdersDto
    {
        public CustomerDto Customer { get; set; }

        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
