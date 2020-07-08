using System;
using System.Data.SqlClient;
using System.Linq;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static readonly Utils _utils = new Utils();
        private static readonly CustomerOrdersContext _CustomerOrderscontext = new CustomerOrdersContext();
        public static Customer Load(int customerId)
        {
            if (customerId == 0)
            {
                return null;
            }

            Customer customer = new Customer();
            try
            {                
                customer = _CustomerOrderscontext.Customers.Where(x => x.Id == customerId).FirstOrDefault();                
            }
            catch (Exception ex)
            { //log exception in the global filter
                throw ex;
            }
                       
            return customer;
        }
        public static void Save(Customer customer)
        {         
            if (!string.IsNullOrEmpty(_utils.ValidateModel(customer)))
            {
                throw new Exception("Invalid Model");
            }

            try
            {
                _CustomerOrderscontext.Customers.Add(customer);
                _CustomerOrderscontext.SaveChanges();               
            }
            catch (Exception ex)
            {
                //log exception in the global filter
                throw ex;
            }            
        }

    }
}
