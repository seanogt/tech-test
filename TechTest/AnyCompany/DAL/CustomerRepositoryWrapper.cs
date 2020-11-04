using AnyCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany.DAL
{
    public class CustomerRepositoryWrapper : RepositoryBase, ICustomerRepository
    {
        public CustomerRepositoryWrapper(string connectionString)
            : base(connectionString)
        {

        }

        public Customer GetCustomerById(int customerId)
        {
            return CustomerRepository.Load(customerId, Connection);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Customer");
            var reader = ExecuteQuery(command);

            List<Customer> customers = new List<Customer>();

            //TODO: Implement Mapper
            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    Name = reader["Name"].ToString(),
                    DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                    CountryId = Convert.ToInt32(reader["CountryId"])
                });
            }

            return customers;
        }
    }
}
