using System;
using System.Data.SqlClient;
using AnyCompany.Models;
using AnyCompany.AnyCompanyContext;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace AnyCompany.Repositories.CustomerRepository
{
    public static class CustomerRepository
    {
        private static CompanyContext _context = new CompanyContext();

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            var customerObject = _context.Customers.Where(x=>x.CustomerId == customerId).Select(x=>new { 
                x.CustomerId,
                x.FirstName,
                x.LastName,
                x.EmailAddress,
                x.DateOfBirth,
                x.CustomerAccountNumber,
                x.ContactNumber,
                x.Address
            });

            return (Customer)customerObject;
        }

    }
}
