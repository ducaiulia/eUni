using System.Collections.Generic;

namespace eUni.DataAccess.Domain
{
    public class StudentHomework
    {
        public int DomainUserId { get; set; }
        public int HomeworkId { get; set; } 
        public virtual DomainUser DomainUser { get; set; }
        public virtual Homework Homework { get; set; }
        public virtual List<File> Files { get; set; }
        public int Grade { get; set; }
    }
}
