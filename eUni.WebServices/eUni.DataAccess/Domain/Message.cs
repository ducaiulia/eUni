using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace eUni.DataAccess.Domain
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual DomainUser From { get; set; }
        public virtual DomainUser To { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }  
    }
}
