using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class WikiPageViewModel
    {
        public int WikiPageId { get; set; }
        public string Description { get; set; }
        public int ModuleId { get; set; }
        public string Content { get; set; }
    }
}