using eUni.DataAccess.eUniDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers
{
    public class AbstractProvider
    {
        public static ApplicationDbContext context = new ApplicationDbContext();
    }
}
