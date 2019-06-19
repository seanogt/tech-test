using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyCompany
{
    /// <summary>
    /// Model built over Customer class to hold the linked orders for customers
    /// </summary>
    public class CustomerOrder : Customer
    {
        public CustomerOrder()
        { }
        
        public CustomerOrder(Customer customer)
        {
            CustomerId = customer.CustomerId;
            Country = customer.Country;
            DateOfBirth = customer.DateOfBirth;
            Name = customer.Name;
        }
        public List<Order> Orders { get; set; }
    }
}
