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
        }
    }
}