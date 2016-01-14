using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class QuestionAnswersViewModel
    {
        public int QuestionId { get; set; }
        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}