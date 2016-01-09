using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    class TestDTO
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public ModuleDTO Module { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }
}
