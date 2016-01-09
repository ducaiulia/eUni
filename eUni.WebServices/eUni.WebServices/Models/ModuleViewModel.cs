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
        public CourseViewModel Course { get; set; }
    }
}