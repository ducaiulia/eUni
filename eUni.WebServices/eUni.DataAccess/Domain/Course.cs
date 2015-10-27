using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate  { get; set; }
        public virtual User Teacher { get; set; }
        public virtual ICollection<Module> Modules { get; set; }

    }
}
