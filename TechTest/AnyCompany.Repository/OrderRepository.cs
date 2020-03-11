using AnyCompany.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repository
{
    public static class OrderRepository
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
                                                    AND TABLE_NAME='Orders') 
                                        Begin
	                                        CREATE TABLE [dbo].[Orders](
		                                        OrderId bigint IDENTITY(1,1) NOT NULL,
		                                        CustomerId bigint NULL,
		                                        VAT float NULL,
		                                        Amount  float NULL,		
	                                            CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
	                                        (
		                                        [OrderId] ASC
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
        public static void Save(Order order)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString))
            {
                connection.Open();

                if (order.OrderId <= 0)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Orders(CustomerId, Amount, VAT) output INSERTED.OrderId VALUES (@CustomerId, @Amount, @VAT)  ", connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Amount", order.Amount));
                        command.Parameters.Add(new SqlParameter("@VAT", order.VAT));
                        command.Parameters.Add(new SqlParameter("@CustomerId", order.CustomerId));
                        object OrderIdValue = command.ExecuteScalar();
                        if (OrderIdValue != null)
                            order.OrderId = int.Parse(OrderIdValue.ToString());
                    }
                }
                else
                {
                    using (SqlCommand command = new SqlCommand("Update Orders Set CustomerId = @CustomerId,  Amount = @Amount, VAT = @VAT) Where OrderId = @OrderId ", connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Amount", order.Amount));
                        command.Parameters.Add(new SqlParameter("@VAT", order.VAT));
                        command.Parameters.Add(new SqlParameter("@CustomerId", order.CustomerId));
                        command.Parameters.Add(new SqlParameter("@OrderId", order.OrderId));
                        command.ExecuteNonQuery();                        
                    }
                }
                connection.Close();
            }
        }
        public static List<Order> LoadByCustomer(int customerId)
        {
            List<Order> orderList = new List<Order>();

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM Orders WHERE CustomerId = @CustomerIdParam", connection))
                {
                    command.Parameters.Add(new SqlParameter($"@CustomerIdParam", customerId));

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        orderList.Add(new Order()
                        {
                            Amount = double.Parse(reader["Amount"].ToString()),
                            CustomerId = int.Parse(reader["CustomerId"].ToString()),
                            OrderId = int.Parse(reader["OrderId"].ToString()),
                            VAT = double.Parse(reader["VAT"].ToString()),
                        });
                    }
                }
                connection.Close();
            }
            return orderList;
        }
    }
}
