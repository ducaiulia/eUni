using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Module Module { get; set; }  


    }
}
