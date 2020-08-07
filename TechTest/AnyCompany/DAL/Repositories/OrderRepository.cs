using System;
using System.Data.SqlClient;

namespace AnyCompany.DAL
{
    public class OrderRepositoryWrapper : IOrderRepository
    {
        public bool Save(Order order)
        {
            //Use a mapper like AutoMapper here to change Model to user friendly DTO if necessary
            return new OrderRepository().Save(order);
        }

        private class OrderRepository
        {
            internal bool Save(Order order)
            {
                try
                {
                    using (AnyCompanyContext context = new AnyCompanyContext())
                    {
                        context.Orders.Add(order);
                        return context.SaveChanges() > 0;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
        }
    }
}
