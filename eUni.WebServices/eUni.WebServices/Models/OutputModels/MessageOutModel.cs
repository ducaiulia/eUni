using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Models.OutputModels
{
    public class MessageOutModel
    {
        public int MessageId { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}