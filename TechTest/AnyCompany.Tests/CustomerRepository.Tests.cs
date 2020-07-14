using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Models;
using AnyCompany.Repositories.CustomerRepository;
using NUnit.Framework;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class CustomerRepository
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            var customerRepo = new CustomerRepository();
        }

        [Test]
        public void SamplePassTest()
        {
            Assert.Pass();
        }

        [Test]
        public void CheckIfReturnTYpeOfCstomerIsOfCorrectObject()
        { 
            
        }
    }
}
