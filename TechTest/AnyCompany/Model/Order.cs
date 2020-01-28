using System;
using AnyCompany.Interface;

namespace AnyCompany
{
    public class Order : IOrder
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
        public Guid CustomerId { get; set; }
    }
}
