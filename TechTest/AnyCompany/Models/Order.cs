using AnyCompany.Models;
using System;

namespace AnyCompany.Models
{
    public class Order
    {
        private double _amount;
        public int OrderId { get; set; }
        public double VAT { get; set; }

        public Order()
        {

        }

        public Order(double amount, Customer customer)
        {
            Amount = amount;
            Customer = customer;
            VAT = customer.Country.VATRate;
        }

        public double Amount
        {
            get { return _amount; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentException("Amount should be greater than 0");

                _amount = value; 
            }
        }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
