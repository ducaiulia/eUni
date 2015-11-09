using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface IUserProvider
    {
        DomainUserDTO GetByUserName(string getFromToken);
        List<DomainUserDTO> GetAllUsers();
        DomainUserDTO GetByName(string lastName, string firstName);
    }
}
