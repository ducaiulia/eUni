using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class HomeworkViewModel
    {
        public int HomeworkId { get; set; }
        public int ModuleId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }
}