using eUni.DataAccess.eUniDbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Domain
{
    public class DomainUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DomainUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [RegularExpression(@"[a-z][a-z]*@[a-z][a-z]*.[a-z][a-z]*.+[a-z]*")]
        public string Email { get; set; }
        public string MatriculationNumber { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
