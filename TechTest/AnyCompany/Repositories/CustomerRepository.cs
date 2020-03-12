using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AnyCompany.Models;

namespace AnyCompany.Repositories
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
                connection))
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
                    }

                    connection.Close();
                }

                return customer;
            }
        }

        public static List<CustomerOrder> LoadAllCustomers()
        {
            List<CustomerOrder> list = new List<CustomerOrder>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Customer LEFT JOIN Order ON Customer.CustomerId = Order.CustomerId",
                connection))
                {
                    var reader = command.ExecuteReader();

                    int customerId = 0;
                    while (reader.Read())
                    {
                        int readerCustomerId = Convert.ToInt32(reader["CustomerId"]);
                        if (readerCustomerId == customerId)
                        {
                            Customer customer = new Customer
                            {
                                CustomerId = readerCustomerId,
                                Name = reader["Name"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                Country = reader["Country"].ToString(),
                            };
                            Order order = new Order
                            {
                                CustomerId = readerCustomerId,
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                Amount = Convert.ToDouble(reader["Amount"]),
                                VAT = Convert.ToDouble(reader["VAT"])
                            };

                            list.Add(new CustomerOrder
                            {
                                Customer = customer,
                                Orders = new List<Order> { order }
                            });
                        }
                        else
                        {
                            list.Where(x => x.Customer.CustomerId == readerCustomerId).SingleOrDefault().Orders.Add(new Order
                            {
                                CustomerId = readerCustomerId,
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                Amount = Convert.ToDouble(reader["Amount"]),
                                VAT = Convert.ToDouble(reader["VAT"])
                            });
                        }
                    }

                    connection.Close();
                }

                return list;
            }
        }
    }
}
