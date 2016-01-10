using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class ModuleViewModel
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public List<FileViewModel> DownloadLinks { get; set; }
        public List<WikiPageViewModel> WikiPages { get; set; }
    }
}