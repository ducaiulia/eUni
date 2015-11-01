using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Repository;

namespace DbTests
{
    public class Test
    {
        public IUserRepository Repo;
        public Test(IUserRepository repo)
        {
            Repo = repo;
        }

    }
}
