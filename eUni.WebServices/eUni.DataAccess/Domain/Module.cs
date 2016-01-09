using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUni.DataAccess.Domain
{
    public class Module
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public virtual Course Course { get; set; }
        public virtual List<Content> Contents { get; set; }
        public virtual List<File> Files { get; set; }
        public virtual List<WikiPage> WikiPages { get; set; }

      
    }
}
