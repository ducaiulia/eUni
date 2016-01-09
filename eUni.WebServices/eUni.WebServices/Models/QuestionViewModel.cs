using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public ModuleViewModel Module { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}