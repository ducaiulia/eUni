using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class UploadedHomeworksDTO
    {
        public int StudentId { get; set; }
        public string FullUserName { get; set; }
        public List<FileDTO> Paths { get; set; }
        public int Grade { get; set; }
    }
}
