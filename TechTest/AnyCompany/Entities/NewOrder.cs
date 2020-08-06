using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Entities
{
    public class NewOrder
    {
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
    }
}
