
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class StudentTest
    {
        public int DomainUserId { get; set; }
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        public virtual DomainUser DomainUser { get; set; }
        public int Grade { get; set; }  

    }
}
