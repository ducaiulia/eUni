using eUni.DataAccess.eUniDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Repository
{
    public class AspNetUserRepository: Repository<ApplicationUser>, IAspNetUserRepository
    {
        public AspNetUserRepository(ApplicationDbContext context) : base(context) { }
    }
}
