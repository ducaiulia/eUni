using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Repository
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(ApplicationDbContext context) : base(context) { }
    }
}