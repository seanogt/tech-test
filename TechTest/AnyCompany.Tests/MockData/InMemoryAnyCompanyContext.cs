
namespace InvestecUnitTestsTests.MockData
{
    using AnyCompany;
    using AnyCompany.DAL;
    using Microsoft.EntityFrameworkCore;
    public class RepoAnyCompanyContext : AnyCompanyContext
    {
        public RepoAnyCompanyContext(DbContextOptions option) : base()
        {

        }
    }
}