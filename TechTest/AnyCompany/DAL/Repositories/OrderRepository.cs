using System;
using System.Data.SqlClient;

namespace AnyCompany.DAL.Repositories
{
    internal class OrderRepository
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
