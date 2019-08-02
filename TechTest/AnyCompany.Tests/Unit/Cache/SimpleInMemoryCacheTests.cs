using System.Threading.Tasks;
using AnyCompany.Service.Cache;
using NUnit.Framework;

namespace AnyCompany.Tests.Unit.Cache
{
    [TestFixture]
    public class SimpleInMemoryCacheTests
    {
        private SimpleInMemoryCache _subject;
        private readonly string _key = "zim";
        private readonly string _value = "zoom";

        [SetUp]
        public void SetUp()
        {
            this._subject = new SimpleInMemoryCache();
        }

        [Test]
        public async Task AddingItemToCache_SavesItems_InCache()
        {
            // Polluted test as it assumes that the 'Set' method works properly.
            // If it actually doesn't, the test will fail, which is incorrect.
            // Solvable by injecting an initial map of key-value to the cache wrapper, though not ideal.
            await this._subject.Set(_key, _value);

            var actualValue = await _subject.Get(_key);
            
            Assert.AreEqual(_value, actualValue);
        }

    }
}