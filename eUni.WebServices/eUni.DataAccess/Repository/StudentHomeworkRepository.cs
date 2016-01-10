using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Repository
{
    public class StudentHomeworkRepository: Repository<StudentHomework>, IStudentHomeworkRepository
    {
        public StudentHomeworkRepository(ApplicationDbContext context) : base(context) { }
    }
}
