using System;
using AnyCompany.DataRepositories;
using AnyCompany.Models;

namespace AnyCompany.Services
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        /// <summary>
        /// Places an order for the specified customer
        /// </summary>
        /// <param name="order">Order to be placed</param>
        /// <param name="customerId">Customer to place the order for</param>
        /// <returns>Bool based on the success of the insert</returns>
        public bool PlaceOrder(OrderModel order, int customerId)
        {
            try
            {
                CustomerModel customer = CustomerRepository.Load(customerId);

                if (customer == null)
                {
                    throw new Exception("No customer found for the supplied id");
                }

                if (Math.Abs(order.Amount) < 1) // using abs avoid issues with rounding
                    throw new Exception("Order amount may not be 0");

                order.VAT = GetVatForCustomer(customer);
                order.CustomerId = customerId;

                orderRepository.Save(order);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Order service error placing order: {e.Message}");
                return false;
            }            
        }

        #region helper

        /// <summary>
        /// Gets the vat based on the customer's country.
        /// </summary>
        /// <param name="customer">Customer containing a country code</param>
        /// <returns>Vat amount for customer's country</returns>
        private double GetVatForCustomer(CustomerModel customer)
        {
            //note. This can be expanded later to use a proper service to retrieve the VAT as well as validate the country code

            switch (customer.Country.Trim())
            {
                case "UK":
                    return 0.2d;                    
                default:
                    return 0;                                            
            }
        }

        #endregion
    }
}
