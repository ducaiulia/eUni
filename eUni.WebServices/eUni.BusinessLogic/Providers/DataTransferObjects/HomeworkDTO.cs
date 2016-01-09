using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class HomeworkDTO
    {
        public int HomeworkId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public ModuleDTO Module { get; set; }
    }
}
