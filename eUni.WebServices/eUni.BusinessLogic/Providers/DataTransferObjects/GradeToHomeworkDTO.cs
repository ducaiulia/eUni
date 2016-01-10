using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class GradeToHomeworkDTO
    {
        public int HomeworkId { get; set; }
        public int StudentId { get; set; }
        public int Grade { get; set; }
    }
}
