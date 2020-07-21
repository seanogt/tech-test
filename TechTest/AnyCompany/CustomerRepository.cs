using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=AnyCompany;User Id=admin;Password=password1;";

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("select * FROM Customers WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
                
            }
            connection.Close();

            return customer;
        }

        public static Customer GetCustomerOrders(int customerId)
        {
            Customer customer = new Customer();
            List<Order> order = new List<Order>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("select * FROM Customers C INNER JOIN Orders O on O.CustomerId = C.CustomerId WHERE C.CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
                Order c = new Order
                {
                    OrderId = Convert.ToInt32(reader["OrderId"]),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    VAT = Convert.ToDouble(reader["VAT"]),
                    CustomerId = Convert.ToInt32(reader["CustomerId"])
                };
                order.Add(c);

            }
            connection.Close();

            customer.Orders = order;

            return customer;
        }
        public static int Save(Customer customer)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Customers VALUES (@CustomerId, @Country, @DateOfBirth, @Name)", connection);

                command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                command.Parameters.AddWithValue("@Country", customer.Country);
                command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                command.Parameters.AddWithValue("@Name", customer.Name);

                int result = command.ExecuteNonQuery();

                connection.Close();

                return result;
            }
            catch (Exception ex)
            {
                ex.ToString();

                return -1;             
            }
        }
    }
}
