using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public ModuleViewModel Module { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}