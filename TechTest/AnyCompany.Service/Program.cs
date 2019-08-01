using System;
using AnyCompany.Service.Container;
using AnyCompany.Service.Models;

namespace AnyCompany.Service
{
    public class Program
    {

        public static void Main(string[] args)
        {
            /*
             * THIS IS THE ENTRY POINT TO THE SYSTEM.
             *
             * Using this as a console app for demonstration purposes,
             * as there is no difficulty in adding any kind of web layer, regardless of implementation.
             * All internal logic is DI-ed and injected, so the only thing left to do is consume that in
             * whichever user-facing interface one desires.
             */
            
            // Allowing the container to reset to default values.
            var container = new DefaultContainer();
            
            StartConsoleInterface(container);
        }

        static async void StartConsoleInterface(IContainer container)
        {
            var input = "";
            while (input != "-1")
            {
                Console.WriteLine("Select your options: (1) Create Order, (2) get users orders, (3) get order, (4) get taxes");
                input = Console.ReadLine();
                var option = 0;
                if (!int.TryParse(input, out option))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        await container.OrdersFacade.CreateOrderForCustomer(new Order("1", 2, 3, 1));
                        break;
                    case 2:
                        await container.OrdersFacade.GetOrdersByCustomer("1");
                        break;
                    case 3:
                        await container.OrdersFacade.GetOrderById("1");
                        break;
                    case 4:
                        await container.TaxesFacade.GetTaxByType("vat");
                        break;
                    case -1:
                        return;
                }
            }
        }
    }
}