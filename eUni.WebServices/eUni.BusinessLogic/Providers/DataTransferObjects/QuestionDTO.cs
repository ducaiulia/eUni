using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public int ModuleId { get; set; }
        public int TestId { get; set; }
    }
}
