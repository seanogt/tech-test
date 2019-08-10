using System.Collections.Generic;
using System.Data;
using System.Linq;
using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Data.Dapper.Enums;
using AnyCompany.Data.Dapper.Factories;
using AnyCompany.Models;
using Dapper;

namespace AnyCompany.Data.Dapper.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public OrderRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Add(Order order)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                connection.Execute(@"INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)",
                    new
                    {
                        OrderId = order.OrderId,
                        Amount = order.Amount,
                        VAT = order.VAT,
                        CustomerId = order.CustomerId
                    });
            }
        }

        public IEnumerable<Order> GetList()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Query<Order>("SELECT OrderId, Amount, VAT, CustomerId from dbo.Orders").ToList();
            }
        }

        private IDbConnection CreateConnection()
        {
            return _connectionFactory.Create(ConnectionType.OrderDb);
        }
    }
}
