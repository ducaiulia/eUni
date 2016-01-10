using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models.OutputModels
{
    public class WikiPageOutModel
    {
        public int WikiPageId { get; set; }
        public string Description { get; set; }
        public NamedEntityOutModel Module { get; set; }
        public string Content { get; set; }
    }
}