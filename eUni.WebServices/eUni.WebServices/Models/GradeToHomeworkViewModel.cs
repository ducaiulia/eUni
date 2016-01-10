using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class GradeToHomeworkViewModel
    {
        public int HomeworkId { get; set; }
        public int StudentId { get; set; }
        public int Grade { get; set; }
    }
}