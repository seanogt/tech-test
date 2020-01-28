using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Interface
{
    public interface IOrder
    {
        int OrderId { get; set; }
        double Amount { get; set; }
        double VAT { get; set; }
        Guid CustomerId { get; set; }
    }
}
