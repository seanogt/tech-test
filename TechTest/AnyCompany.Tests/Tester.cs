using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    public class Tester
    {
        public static void Main()
        {
            OrderService service = new OrderService();
            try
            {
           
            Customer steve = new Customer { Name = "Steve", Country = "UK", DateOfBirth = new DateTime(2001, 2, 13) };
            Order orderFridge = new Order { Amount = 1229.76, CustomerId = 1, VAT = 0 };
            //

            Order den = new Order();
            Customer customer = new Customer();
            den.Amount = 139.50;
            den. CustomerId = 2;
            den. VAT = 0;
            customer.Name = "Den";
            customer. Country = "SA";
            customer.DateOfBirth = new DateTime(2001, 2, 13);
            den.Customer = customer;
                //
                CustomerRepository.Save(steve);
                var cust = CustomerRepository.Load(1);
                CustomerRepository.Save(den.Customer);
                //
                Order denny = new Order();
            denny.Customer = cust;
            var result = service.PlaceOrder(orderFridge, cust);
            var result1 = service.PlaceOrder(den, den.Customer);
            var result2 = service.PlaceOrder(denny, denny.Customer);

                var results3 = service.GetCustomerOrders();

                foreach (var custOrder in results3)
                {
                    Console.WriteLine($"Name: {custOrder.Customer.Name} -Country: {custOrder.Customer.Country} - Amount: ${custOrder.Amount} VAT: {custOrder.VAT} ");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
