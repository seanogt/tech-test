using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyCompany.Service.DAL;
using AnyCompany.Service.DAL.DataManagers;
using Moq;
using NUnit.Framework;

namespace AnyCompany.Tests.Unit.DAL.DataManagers
{
    [TestFixture]
    public class OrdersDbManagerTests
    {
        private Mock<IDatabaseWrapper> _databaseMock;
        private OrdersDbManager _subject;

        private readonly IEnumerable<IDictionary<string, object>> _mockResults = new List<IDictionary<string, object>>
        {
            new Dictionary<string, object> {{"key", "value"}}
        };

        [SetUp]
        public void Setup()
        {
            _databaseMock = new Mock<IDatabaseWrapper>();
            _subject = new OrdersDbManager(_databaseMock.Object);
        }

        [Test]
        public async Task GettingOrdersByCustomer_Calls_TheRightQueryWithArgs()
        {
            _databaseMock.Setup((_) => _.ExecuteSqlFile(
                It.IsAny<string>(),
                It.IsAny<object[]>()
            )).Returns(
                Task.FromResult(_mockResults));

            var result = await _subject.GetOrdersByCustomer("customerId");

            Assert.AreEqual(_databaseMock.Invocations.Count, 1);
            Assert.AreEqual(_databaseMock.Invocations[0].Method.Name, "ExecuteSqlFile");
            var expectedArgs = new object[] {"Orders/get-order-by-customer", new[] {"customerId"}};

            Assert.AreEqual(expectedArgs, _databaseMock.Invocations[0].Arguments.ToArray());
        }
    }
}