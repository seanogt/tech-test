using System;

namespace AnyCompany.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public double Amount { get; set; }
        public double Vat { get; set; }
    }
}
