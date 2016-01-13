using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EUni_Client.Models
{
    public class QuestionResultsViewModel
    {
        public int TestId { get; set; }
        public IList<QuestionViewModel> TestQuestions { get; set; }
        public IList<QuestionViewModel> ModuleQuestions { get; set; }
    }
}