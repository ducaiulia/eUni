using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class StudentHomeworkDTO
    {
        public int StudentId { get; set; }
        public int HomeworkId { get; set; }
        public int Grade { get; set; }
        public DomainUserDTO DomainUser { get; set; }
        public HomeworkDTO Homework { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
