using AnyCompany.Model;
using AnyCompany.Persistence;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    internal class OrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        private IDataAccessObject _dataAccessObject;
        private DataAccessObjectFactory _dataAccessObjectFactory;

        private IDataAccessObject DataAccessObject
        {
            get
            {
                if (_dataAccessObject == null)
                {
                    _dataAccessObject = DataAccessObjectFactory.CreateDataAccess();
                }

                return _dataAccessObject;
            }
        }

        private DataAccessObjectFactory DataAccessObjectFactory
        {
            get
            {
                if (_dataAccessObjectFactory == null)
                {
                    _dataAccessObjectFactory = new DataAccessObjectFactory(ConnectionString);
                }

                return _dataAccessObjectFactory;
            }
        }

        public void Save(Order order, int customerId)

        {
            using (var connection = DataAccessObject.CreateConnection())
            {
                connection.Open();

                string commandString = "INSERT INTO [Orders] VALUES (@CustomerId, @Amount, @VAT)";

                var command = DataAccessObject.CreateCommand(commandString, System.Data.CommandType.Text, connection);

                command.Parameters.Add(DataParameterHelper.CreateParameter(string.Empty, "@CustomerId", customerId, System.Data.DbType.Int32));
                command.Parameters.Add(DataParameterHelper.CreateParameter(string.Empty, "@Amount", order.Amount, System.Data.DbType.Double));
                command.Parameters.Add(DataParameterHelper.CreateParameter(string.Empty, "@VAT", order.VAT, System.Data.DbType.Double));

                command.ExecuteNonQuery();

                DataAccessObject.CloseConnection(connection);
            }
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            var orders = new List<Order>();

            using (var connection = DataAccessObject.CreateConnection())
            {
                connection.Open();

                string commandString = $"SELECT * FROM [Orders] WHERE CustomerId = {customerId}";

                var command = DataAccessObject.CreateCommand(commandString, System.Data.CommandType.Text, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var order = new Order
                    {
                        Amount = double.Parse(reader["Amount"].ToString()),
                        VAT = double.Parse(reader["VAT"].ToString()),
                        OrderId = int.Parse(reader["OrderId"].ToString())
                    };
                    orders.Add(order);
                }

                DataAccessObject.CloseConnection(connection);
            }

            return orders;
        }
    }
}
