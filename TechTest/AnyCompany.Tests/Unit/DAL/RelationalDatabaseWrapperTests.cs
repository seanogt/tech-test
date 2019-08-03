using System.IO;
using System.Threading.Tasks;
using AnyCompany.Service.DAL;
using NUnit.Framework;

namespace AnyCompany.Tests.Unit.DAL
{
    [TestFixture]
    public class RelationalDatabaseWrapperTests
    {
        private RelationalDatabaseWrapper _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new RelationalDatabaseWrapper("", null, "WeirdFolder", null);
        }
        
        [Test]
        public void ExecutingSqlFile_ThatDoesNotExist_ThrowsAnError()
        {
            Assert.ThrowsAsync<FileNotFoundException>(async () =>
            {
                await _subject.ExecuteSqlFile("FileThatDoesNotExist", null);
            });
            
        }
    }
}