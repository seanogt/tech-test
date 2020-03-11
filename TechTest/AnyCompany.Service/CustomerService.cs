using AnyCompany.Entities;
using AnyCompany.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Service
{
    
    public static class CustomerService
    {        
        public static bool Save(Customer customer, out string message)
        {
            message = "";
            if ((customer.Country??"") == "")
            {
                message = "Please select a Country.";
                return false;
            }
            if (customer.DateOfBirth.Year <= 1900 ||
                customer.DateOfBirth.Year > DateTime.Now.Year)
            {
                message = "Please select a valid Date Of Birth.";
                return false;
            }
            if ((customer.Name ?? "") == "")
            {
                message = "Please enter a name.";
                return false;
            }
            CustomerRepository.Save(customer);
            return true;
        }
        public static Customer Load(int customerId, out string message)
        {
            message = "";
            if (customerId <= 0)
            {
                message = "Please select a customer id.";
                return null;
            }
            Customer customer = CustomerRepository.Load(customerId);
            if (customer.CustomerId <= 0)
            {
                message = $"Customer id {customerId} not found.";
                return null;
            }
            return customer;
        }
        public static List<Customer> LoadAll()
        {
            return CustomerRepository.LoadAll();
        }

    }
}
