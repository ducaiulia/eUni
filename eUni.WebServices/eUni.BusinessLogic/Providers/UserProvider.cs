using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class UserProvider:AbstractProvider
    {
        private readonly IUserRepository _userRepo = new UserRepository(context);
        private readonly IAspNetUserRepository _aspNetUserRepo = new AspNetUserRepository(context);

        public List<DomainUserDTO> GetAllUsers()
        {
            IEnumerable<DomainUser> enumerableAllUsers = _userRepo.GetAll();
            var allUsers = Mapper.Map<IEnumerable<DomainUserDTO>>(enumerableAllUsers);

            return allUsers.ToList();
        }
        public DomainUserDTO GetByUserName(string userName)
        {
            var user = _aspNetUserRepo.Get(u => u.UserName.Trim().Equals(userName));
            return Mapper.Map<DomainUserDTO>(user.DomainUser);
        }
        
    }
}
