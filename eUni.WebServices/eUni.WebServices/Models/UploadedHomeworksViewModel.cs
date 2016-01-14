using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class UploadedHomeworksViewModel
    {
        public int StudentId { get; set; }
        public string FullUserName { get; set; }
        public List<string> Paths { get; set; }
        public int Grade { get; set; }
    }
}