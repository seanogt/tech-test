using AnyCompany.Model;
using AnyCompany.Persistence;
using System;
using System.Collections.Generic;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        private static OrderRepository _orderRepository;
        private static IDataAccessObject _dataAccessObject;
        private static DataAccessObjectFactory _dataAccessObjectFactory;

        private static OrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository();
                }

                return _orderRepository;
            }
        }

        private static IDataAccessObject DataAccessObject
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

        private static DataAccessObjectFactory DataAccessObjectFactory
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

        public static Customer Load(int customerId)
        {
            Customer customer = new Customer();

            using (var connection = DataAccessObject.CreateConnection())
            {
                connection.Open();

                string commandString = $"SELECT* FROM Customer WHERE CustomerId = {customerId}";

                var command = DataAccessObject.CreateCommand(commandString, System.Data.CommandType.Text, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())

                {
                    customer.CustomerId = int.Parse(reader["CustomerId"].ToString());
                    customer.Name = reader["Name"].ToString();
                    customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                    customer.Country = reader["Country"].ToString();
                }

                DataAccessObject.CloseConnection(connection);
            }            

            return customer;
        }

        public static Customer LoadCustomerAndOrders(int customerId) 
        {
            Customer customer = Load(customerId);

            if (!string.IsNullOrEmpty(customer.Name))
            {
                LoadCustomerOrders(customer);
            }
            else { return null; }

            return customer;
        }

        private static void LoadCustomerOrders(Customer customer)
        {
            customer.Orders = OrderRepository.GetCustomerOrders(customer.CustomerId);
        }

        public static void Save(Customer customer)
        {
            using (var connection = DataAccessObject.CreateConnection())
            {
                connection.Open();

                string commandString = "INSERT INTO Customer VALUES (@Name, @DateOfBirth, @Country)";

                var command = DataAccessObject.CreateCommand(commandString, System.Data.CommandType.Text, connection);

                command.Parameters.Add(DataParameterHelper.CreateParameter(string.Empty, "@Name", 150, customer.Name, System.Data.DbType.String));
                command.Parameters.Add(DataParameterHelper.CreateParameter(string.Empty, "@DateOfBirth", customer.DateOfBirth, System.Data.DbType.Date));
                command.Parameters.Add(DataParameterHelper.CreateParameter(string.Empty, "@Country", 150, customer.Country, System.Data.DbType.String));

                command.ExecuteNonQuery();

                DataAccessObject.CloseConnection(connection);
            }
        }

        public static List<Customer> LoadAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (var connection = DataAccessObject.CreateConnection())
            {
                connection.Open();

                string commandString = $"SELECT * FROM Customer";

                var command = DataAccessObject.CreateCommand(commandString, System.Data.CommandType.Text, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())

                {
                    var customer = new Customer
                    {
                        CustomerId = int.Parse(reader["CustomerId"].ToString()),
                        Name = reader["Name"].ToString(),
                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                        Country = reader["Country"].ToString()
                    };

                    customers.Add(customer);
                }

                DataAccessObject.CloseConnection(connection);
            }

            foreach (var customer in customers)
            {
                LoadCustomerOrders(customer);
            }

            return customers;
        }
    }
}
