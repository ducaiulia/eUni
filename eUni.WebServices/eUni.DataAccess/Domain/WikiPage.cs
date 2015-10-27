using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class WikiPage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WikiPageId { get; set; }
        public string Description { get; set; }
        public virtual Module Module { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Content { get; set; }
    }
}
