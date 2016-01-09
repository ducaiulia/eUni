using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class Module
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<File> Files { get; set; }

    }
}
