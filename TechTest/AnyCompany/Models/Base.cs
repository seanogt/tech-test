using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Models
{
    public class Base
    {
        public string CustomerAccountNumber { get; set; }
        public int CustomerId { get; set; }
        public Address Address { get; set; }
    }
}
