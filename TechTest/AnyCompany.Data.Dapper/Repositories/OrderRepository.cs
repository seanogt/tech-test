using System.Collections.Generic;
using System.Data;
using System.Linq;
using AnyCompany.Data.Contract.Repositories;
using AnyCompany.Data.Dapper.Enums;
using AnyCompany.Data.Dapper.Factories;
using AnyCompany.Data.Dapper.Sql;
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
                connection.Execute(SqlStatements.InsertOrder,
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
                return connection.Query<Order>(SqlStatements.GetAllOrders).ToList();
            }
        }

        private IDbConnection CreateConnection()
        {
            return _connectionFactory.Create(ConnectionType.OrderDb);
        }
    }
}
