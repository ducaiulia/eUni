
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class StudentTest
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public int Grade { get; set; }  

    }
}
