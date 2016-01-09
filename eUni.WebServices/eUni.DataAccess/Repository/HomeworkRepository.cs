using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Repository
{
    public class HomeworkRepository : Repository<Homework>, IHomeworkRepository
    {
        public HomeworkRepository(ApplicationDbContext context) : base(context) { }
    }
}
