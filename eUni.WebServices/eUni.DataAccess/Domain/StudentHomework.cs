using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class StudentHomework
    {
        public int UserId { get; set; }
        public int HomeworkId { get; set; } 
        public virtual User User { get; set; }
        public virtual Homework Homework { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public int Grade { get; set; }
    }
}
