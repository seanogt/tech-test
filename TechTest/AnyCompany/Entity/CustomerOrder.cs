using System.Collections.Generic;

namespace AnyCompany.Entity
{
    public class CustomerOrder
    {
        public Customer Customer { get; set; }

        public List<Order> Orders { get; set; }
    }
}
