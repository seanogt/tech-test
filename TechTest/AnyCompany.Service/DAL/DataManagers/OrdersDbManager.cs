using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyCompany.Service.Facades;
using AnyCompany.Service.Models;

namespace AnyCompany.Service.DAL.DataManagers
{
    public class OrdersDbManager: IOrdersFacade
    {
        private readonly IDatabaseWrapper _database;

        public OrdersDbManager(IDatabaseWrapper database)
        {
            _database = database;
        }

        public async Task<Order> GetOrderById(string orderId)
        {
            var results = await _database.ExecuteSqlFile("Order/get-order-by-id", new Dictionary<string, object> { { "@order_id", orderId } } );

            var row = results.First();
            return new Order(row["id"].ToString(),
                (double)row["Amount"],
                (double)row["VAT"],
                (string)row["CustomerId"]
                );
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomer(string customerId)
        {
            var results = await _database.ExecuteSqlFile("Order/get-order-by-customer", new Dictionary<string, object> { { "@customer_id", customerId } });

            var orders = results.Select(res => new Order(
                res["orderId"].ToString(),
                (double) res["amount"],
                (double) res["vat"],
                (string) res["CustomerId"]));
            return orders;
        }

        public async Task<string> CreateOrderForCustomer(Order order)
        {
            var results = await _database.ExecuteSqlFile("Order/create-order", new Dictionary<string, object>
            {
                { "@order_id", order.OrderId},
                { "@amount", order.Amount},
                { "@vat",  order.VAT },
                { "@customer_id",  order.CustomerId}
            });

            if (!results.Any())
            {
                // How did this happen? Index collision? Investigate
                throw new Exception("Order Creation - inv.");
            }

            return (string)results.First()["_insertedIds"];

        }
    }
}