using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repository
{
    public static class Context
    {
        public static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";
        public static void CheckAllTableExist()
        {
            CustomerRepository.CheckTableExist();
            OrderRepository.CheckTableExist();
        }
    }
}
