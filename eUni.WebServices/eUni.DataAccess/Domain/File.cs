using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Enums;

namespace eUni.DataAccess.Domain
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public FileType FileType { get; set; }
        public virtual Module Module { get; set; }
    }
}
