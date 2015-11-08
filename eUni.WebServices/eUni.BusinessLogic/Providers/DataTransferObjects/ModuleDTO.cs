using System.Collections.Generic;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class ModuleDTO
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public CourseDTO Course { get; set; }
        public List<ContentDTO> Contents { get; set; }
    }
}