using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    public static class DbTestHelper
    {
        public static DbSet<T> ToDbSet<T>(this IEnumerable<T> data) where T : class
        {
            var fakeData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());

            return mockSet.Object;
        }

        public static Mock<DbSet<T>> ToDbMock<T>(this IEnumerable<T> data) where T : class
        {
            var fakeData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(fakeData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());

            return mockSet;
        }
    }
}
