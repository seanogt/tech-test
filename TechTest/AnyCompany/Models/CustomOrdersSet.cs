using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Models
{
    public class CustomOrdersSet
    {
        public Address Address { get; set; }
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public int OrderNumber { get; set; }
        public double Amount { get; set; }
        public double Vat { get; set; }
    }
}
