using AnyCompany.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repository
{
    public static class CustomerRepository
    {

        public static void CheckTableExist()
        {

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString))
            {
                connection.Open();

                string sqlScript = $@"
                                        IF Not EXISTS (SELECT * 
                                                   FROM INFORMATION_SCHEMA.TABLES 
                                                   WHERE TABLE_TYPE='BASE TABLE' 
                                                   AND TABLE_NAME='Customer') 
                                        Begin
	                                        CREATE TABLE [dbo].[Customer](
		                                        CustomerId bigint IDENTITY(1,1) NOT NULL,
		                                        Name nvarchar(255) NULL,
		                                        DateOfBirth  Date NULL,
		                                        Country nvarchar(255) NULL,
	                                         CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
	                                        (
		                                        [CustomerId] ASC
	                                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	                                        ) ON [PRIMARY]

                                        End";

                using (SqlCommand command = new SqlCommand(sqlScript, connection))
                {                    
                    command.ExecuteNonQuery();                   
                }
                connection.Close();
            }
 
        }
        public static void Save(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString))
            {
                connection.Open();

                if (customer.CustomerId <= 0)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Customer(Name, DateOfBirth,Country) output INSERTED.[CustomerId] VALUES (@Name, @DateOfBirth, @Country);  ", connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", customer.Name));
                        command.Parameters.Add(new SqlParameter("@DateOfBirth", customer.DateOfBirth));
                        command.Parameters.Add(new SqlParameter("@Country", customer.Country));
                        object customerIdValue = command.ExecuteScalar();
                        if (customerIdValue != null)
                            customer.CustomerId = int.Parse(customerIdValue.ToString());
                    }
                }
                else
                {
                    using (SqlCommand command = new SqlCommand("Update Orders Customer Name = @Name,  DateOfBirth = @DateOfBirth, Country = @Country) Where CustomerId = @CustomerId ", connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", customer.Name));
                        command.Parameters.Add(new SqlParameter("@DateOfBirth", customer.DateOfBirth));
                        command.Parameters.Add(new SqlParameter("@Country", customer.Country));
                        command.Parameters.Add(new SqlParameter("@CustomerId", customer.CustomerId));
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }
        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM Customer WHERE CustomerId = @CustomerIdParam", connection))
                {
                    command.Parameters.Add(new SqlParameter($"@CustomerIdParam", customerId));

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        customer.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        customer.Name = reader["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                        customer.Country = reader["Country"].ToString();
                    }
                }
                connection.Close();
            }
            return customer;
        }
        public static List<Customer> LoadAll()
        {
            List<Customer> customerList = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM Customer", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        customerList.Add(new Customer()
                        {
                            CustomerId = int.Parse(reader["CustomerId"].ToString()),
                            Name = reader["Name"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            Country = reader["Country"].ToString(),
                        });
                    }
                }
                connection.Close();
            }
            return customerList;
        }
    }
}
