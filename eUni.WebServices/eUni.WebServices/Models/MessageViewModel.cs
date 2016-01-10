using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models
{
    public class MessageViewModel
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}