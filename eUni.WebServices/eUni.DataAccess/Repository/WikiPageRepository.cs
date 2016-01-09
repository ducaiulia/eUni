using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Repository
{
    public class WikiPageRepository : Repository<WikiPage>, IWikiPageRepository
    {
        public WikiPageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
