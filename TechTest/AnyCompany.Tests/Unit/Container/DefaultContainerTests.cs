using System.Threading.Tasks;
using AnyCompany.Service.Cache;
using AnyCompany.Service.Container;
using NUnit.Framework;

namespace AnyCompany.Tests.Unit.Container
{
    [TestFixture]
    public class DefaultContainerTests 
    {
        private DefaultContainer _subject;

        [SetUp]
        public void SetUp()
        {
            this._subject = new DefaultContainer();
        }

        [Test]
        public void CallingEmptyConstructor_InitialisesContainer_WithDefaultValues() 
        {
            Assert.NotNull(_subject.Cache);
            Assert.NotNull(_subject.Config);
            Assert.NotNull(_subject.Logger);
            Assert.NotNull(_subject.CustomersFacade);
            Assert.NotNull(_subject.DbWrapper);
            Assert.NotNull(_subject.OrdersFacade);
            Assert.NotNull(_subject.TaxesFacade);
        }

    }
}