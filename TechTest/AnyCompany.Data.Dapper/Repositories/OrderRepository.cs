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
            using (var connection = _connectionFactory.Create(ConnectionType.OrderDb))
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
    }
}
