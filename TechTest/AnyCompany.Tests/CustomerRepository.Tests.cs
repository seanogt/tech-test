using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Models;
using AnyCompany.Repositories.CustomerRepository;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class CustomerRepositoryTest
    {


        [Test]
        public static void Given_The_Correct_CustomerId_Is_Provided_Return_Correct_Customer_details ()
        {
            var customerRepo = Substitute.For<ICustomerRepository<Customer>>();

            var results = CustomerRepository.Load(1);

            Assert.IsNotNull(results);

        }

        [Test]
        public static void Given_The_InCorrect_CustomerId_Is_Provided_Return_NoDetails()
        {
            var customerRepo = Substitute.For<ICustomerRepository<Customer>>();

            var results = CustomerRepository.Load(1);

            Assert.IsNull(results);
        }
    }
}
