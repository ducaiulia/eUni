using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class StudentHomeworkViewModel
    {
        public int StudentId { get; set; }
        public string Filename { get; set; }
        public byte[] ContentFile { get; set; }
        public int HomeworkId { get; set; }
        public int Grade { get; set; }
    }
}