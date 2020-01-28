using System;
using System.Data.SqlClient;
using AnyCompany.Interface;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        public static Customer Load(Guid customerId)
        {
            var customer = new Customer();

            try
            {
                var query = "SELECT " +
                            "c.Country, " +
                            "c.DateOfBirth, " +
                            "c.Name," +
                            "OrderId," +
                            "Amount," +
                            "VAT" +
                            "FROM " +
                            "Customer AS c" +
                            "INNER JOIN" +
                            "Orders.dbo.Orders AS o" +
                            "ON" +
                            "c.CustomerId = o.CustomerId" +
                            "WHERE " +
                            " c.CustomerId = " + customerId;

                using (var conn = new SqlConnection(Properties.Settings.Default.connCustomers))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            customer.Name = reader["Name"].ToString();
                            customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            customer.Country = reader["Country"].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return customer;
        }
    }
}
