using AnyCompany.DataAccessLayer;
using AnyCompany.Models;
using AnyCompany.RepositoryLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.RepositoryLayer
{
    public static class CustomerRepository 
    {
        private static readonly CompanyContext companyContext = new CompanyContext();
        public static Customer Load(int customerId)
        {
            return companyContext.Customers.FirstOrDefault(c => c.Id == customerId);
        }
    }
}
