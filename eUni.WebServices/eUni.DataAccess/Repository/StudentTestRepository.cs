using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Repository
{
    public class StudentTestRepository : Repository<StudentTest>, IStudentTestRepository
    {
        public StudentTestRepository(ApplicationDbContext context) : base(context) { }
    }
}