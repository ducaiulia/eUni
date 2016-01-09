using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class WikiPageDTO
    {
        public int WikiPageId { get; set; }

        public string Description { get; set; }

        public ModuleDTO Module { get; set; }

        public string Content { get; set; }
    }
}
