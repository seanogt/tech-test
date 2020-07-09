using System;
using System.Data.SqlClient;

namespace AnyCompany
{
    public class OrderService
    {
        public bool PlaceOrder(Order order, int customerId)
        {
            try
            {
                Customer customer = CustomerRepository.Load(customerId);

                if (order.Amount == 0)
                    return false;

                if (customer.Country == "UK")
                    order.VAT = 0.2m;
                else
                    order.VAT = 0;

                OrderRepository.Save(order);
            }
            catch (SqlException ex)
            {
                //{code to log error}

                return false;
            }
            catch (Exception ex)
            {
                //{code to log error}

                return false;
            }

            return true;
        }
    }
}
