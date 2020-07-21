using System;

namespace AnyCompany
{
    public class OrderService : IOrderService
    { 
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {           
            try
            {
                Customer customer = CustomerRepository.Load(customerId);

                if (customer != null)
                {
                    if (order.Amount == 0)
                        return false;

                    if (customer.Country == "UK")
                        order.VAT = 0.2d;
                    else
                        order.VAT = 0;

                    order.Customer = customer;

                    int result = orderRepository.Save(order);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
    }
}
