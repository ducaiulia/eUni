using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class Homework
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HomeworkId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public virtual Module Module { get; set; }
    }
}
