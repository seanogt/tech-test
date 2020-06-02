using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Models
{
    /// <summary>
    /// Response type model to supply a customer with a list of their orders
    /// </summary>
    public class CustomerOrderModel
    {
        public CustomerModel Customer { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
