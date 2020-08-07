using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace AnyCompany.DAL.Repositories
{
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        public Customer Get(int CustomerId)
        {
            //Use a mapper like AutoMapper here to change Model to user friendly DTO if necessary
            return CustomerRepository.Get(CustomerId);
        }

        public IEnumerable<Customer> GetAll(bool includeOrders = true)
        {
            //Use a mapper like AutoMapper here to change Model to user friendly DTO if necessary
            return CustomerRepository.GetAll(includeOrders);
        }

        public static class CustomerRepository
        {
            public static Customer Get(int CustomerId)
            {
                try
                {
                    using (AnyCompanyContext anyCompanyContext = new AnyCompanyContext())
                    {
                        return anyCompanyContext.Customers.SingleOrDefault(c => c.CustomerId == CustomerId);
                    }
                }
                catch (SqlException ex)
                {
                    //Logger here
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    //Logger here
                    Console.WriteLine(ex.Message);
                }
                return null;
            }

            public static IEnumerable<Customer> GetAll(bool includeOrders = true)
            {
                try
                {
                    using (AnyCompanyContext anyCompanyContext = new AnyCompanyContext())
                    {
                        return includeOrders ? anyCompanyContext.Customers.Include(c => c.Orders).ToList() : anyCompanyContext.Customers.ToList();
                    }                              
                }
                catch (SqlException ex)
                {
                    //Injected Logger
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    //Injected Logger
                    Console.WriteLine(ex.Message);
                }
                return null;
            }
        }
    }
}
