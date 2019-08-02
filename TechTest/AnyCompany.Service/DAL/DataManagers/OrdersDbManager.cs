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
            this._database = database;
        }

        public async Task<Order> GetOrderById(string orderId)
        {
            var results = await this._database.ExecuteSqlFile("Orders/get-order-by-id", new [] {orderId});
            if (!results.Any())
            {
                throw new Exception("Not found!");
            }

            var row = results.First();
            return new Order(row["id"].ToString(),
                (double)row["Amount"],
                (double)row["VAT"],
                (int)row["CustomerId"]
                );
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomer(string customerId)
        {
            var results = await this._database.ExecuteSqlFile("Orders/get-order-by-customer", new [] {customerId});
            if (!results.Any())
            {
                throw new Exception("Not found!");
            }

            var orders = results.Select<IDictionary<string, object>,Order>(res => new Order(
                res["orderId"].ToString(),
                (double) res["amount"],
                (double) res["vat"],
                (int) res["CustomerId"]));
            return orders;
        }

        public async Task<string> CreateOrderForCustomer(Order order)
        {
            var results = await this._database.ExecuteSqlFile("Orders/create-order", new object [] {order.OrderId, order.Amount, order.VAT, order.CustomerId});

            if (!results.Any())
            {
                //....
            }

            return (string)results.First()["_insertedIds"];

        }
    }
}