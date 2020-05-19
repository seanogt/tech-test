using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer;
        }

        public static Customer Save(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                SqlCommand command = new SqlCommand("INSERT INTO Customer (Name, DateOfBirth, Country) OUTPUT INSERTED.CustomerID VALUES (@Name, @DateOfBirth, @Country) ", connection);

                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                command.Parameters.AddWithValue("@Country", customer.Country);

                connection.Open();

                customer.CustomerID = (int)command.ExecuteScalar();

            }

            return customer;
        }

        public static List<CustomerOrder> LoadCustomers()
        {
            List<CustomerOrder> customers = new List<CustomerOrder>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer LEFT JOIN Order ON Customer.CustomerID = Order.CustomerID", connection);

                connection.Open();

                var reader = command.ExecuteReader();

                int customerId = 0;

                while (reader.Read())
                {
                    int readerCustId = Convert.ToInt32(reader["CustomerID"]);

                    if (readerCustId == customerId)
                    {
                        Customer customer = new Customer()
                        {
                            CustomerID = readerCustId,
                            Name = reader["Name"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            Country = reader["Country"].ToString()
                        };

                        Order order = new Order
                        {
                            CustomerID = readerCustId,
                            OrderId = Convert.ToInt32(reader["OrderId"]),
                            Amount = Convert.ToDouble(reader["Amount"]),
                            VAT = Convert.ToDouble(reader["VAT"])
                        };


                        customers.Add(new CustomerOrder
                        {
                            Customer = customer,
                            Orders = new List<Order> { order }
                        });
                    }
                    else
                    {
                        customers.Where(c => c.Customer.CustomerID == readerCustId).FirstOrDefault().Orders.Add(new Order
                        {
                            CustomerID = readerCustId,
                            OrderId = Convert.ToInt32(reader["OrderId"]),
                            Amount = Convert.ToDouble(reader["Amount"]),
                            VAT = Convert.ToDouble(reader["VAT"])
                        });
                    }
                }

                connection.Close();
            }

            return customers;
        }
    }
}
