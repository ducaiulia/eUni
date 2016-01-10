using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public DomainUserDTO From { get; set; }
        public DomainUserDTO To { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
