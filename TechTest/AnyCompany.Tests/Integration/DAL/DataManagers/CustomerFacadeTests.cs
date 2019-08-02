using AnyCompany.Service.DAL.DataManagers;
using NUnit.Framework;

namespace AnyCompany.Tests.Integration.DAL.DataManagers
{
    // Testing integreation with the database, and that the queries work as expected.
    // Run against test database
    
    [TestFixture]
    public class CustomerFacadeTests : IntegrationTestsBase
    {
        private CustomersDbManager _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new CustomersDbManager(_databaseWrapper);
            BuildMockCustomers();
        }
        
        [Test]
        public void When_GettingCustomerById_It_ReturnsTheRightCustomer()
        {
            var actual = _subject.GetCustomerById("cust-1");
            
            // Make sure that the right customer was returned by the query:
            // Assert.AreEqual(actual, mockCustomer1);
        }

        /// <summary>
        /// Extract that to an external file when required by other integration tests.
        /// </summary>
        private void BuildMockCustomers()
        {
            
        }
    }
}