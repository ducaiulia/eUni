using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DomainUserDTO Teacher { get; set; }
        public List<ModuleDTO> Modules { get; set; }
    }
}
