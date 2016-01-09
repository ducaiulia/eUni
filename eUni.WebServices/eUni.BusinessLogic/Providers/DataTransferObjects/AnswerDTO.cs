using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    class AnswerDTO
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
