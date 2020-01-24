using System;
using System.Data.SqlClient;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=DESKTOP-21TVJEC;Initial Catalog=AnyCompany;Integrated Security=True;Persist Security Info=False;Enlist=False;Pooling=True;Min Pool Size=1;Max Pool Size=100;Connect Timeout=15;User Instance=False";//ConfigurationSettings.AppSettings["AnyCompany"].ToString();//@"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

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
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

            return customer;
        }
      
    }
}
