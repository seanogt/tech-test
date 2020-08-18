using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
