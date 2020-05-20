using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
    public class Config
    {
        public string OrdersDbConnectionString { get; set; } = @"Server=(localdb)\mssqllocaldb;Database=Orders;Integrated Security=true";
        public string CustomerDbConnectionString { get; set; } = @"Server=(localdb)\mssqllocaldb;Database=Customers;Integrated Security=true";
    }
}
