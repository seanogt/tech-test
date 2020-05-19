using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class CustomerOrder
    {
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
    }
}
