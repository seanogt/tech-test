using System.Collections.Generic;
using System.Linq;
using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Data.Dapper.Enums;
using AnyCompany.Data.Dapper.Factories;
using AnyCompany.Data.Dapper.Sql;
using AnyCompany.Models;
using Dapper;

namespace AnyCompany.Data.Dapper.Repositories
{
    public class CustomerRepositoryWrapper : ICustomerRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CustomerRepositoryWrapper(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // using this to act as a proxy to the static CustomerRepository - I assumed this was legacy that 
        // we're trying to abstract and eventually replace
        public Customer Load(int customerId)
        {
            return CustomerRepository.Load(customerId);
        }

        public IEnumerable<Customer> GetList()
        {
            using (var connection = _connectionFactory.Create(ConnectionType.CustomerDb))
            {
                connection.Open();
                return connection.Query<Customer>(SqlStatements.GetAllCustomers).ToList();
            }
        }
    }
}
