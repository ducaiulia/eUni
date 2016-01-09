using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.DataAccess.Repository
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(ApplicationDbContext context) : base(context) { }
    }
}
