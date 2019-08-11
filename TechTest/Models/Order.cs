using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
    }
}
