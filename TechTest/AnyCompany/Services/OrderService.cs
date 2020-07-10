using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnyCompany.IServices;

namespace AnyCompany
{
    public class OrderService : IOrdersService
    {
        public Order GetOrder(int orderId)
        {
            var order = new Order();
            try
            {
                order = OrderRepository.GetOrder(orderId);
            }
            catch (SqlException ex)
            {
                //{code to log error}
            }
            catch (Exception ex)
            {
                //{code to log error}
            }
            return order;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            int result = 0;
            try
            {
                Customer customer = CustomerRepository.Load(customerId);

                if (order.Amount <= 0)
                    return false;

                if (customer.Country == "UK")
                    order.VAT = 0.2m;
                else
                    order.VAT = 0;

                result = OrderRepository.Save(order);
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

            if (result == 0) { return false; }

            return true;
        }
    }
}
