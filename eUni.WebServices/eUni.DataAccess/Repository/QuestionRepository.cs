using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Repository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context) { }
    }
}