using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class UserProvider
    {
        private readonly IUserRepository _userRepo = new UserRepository(new ApplicationDbContext()); 

        public List<DomainUserDTO> GetAllUsers()
        {
            IEnumerable<DomainUser> enumerableAllUsers = _userRepo.GetAll();
            var allUsers = Mapper.Map<IEnumerable<DomainUserDTO>>(enumerableAllUsers);

            return allUsers.ToList();
        }
    }
}
