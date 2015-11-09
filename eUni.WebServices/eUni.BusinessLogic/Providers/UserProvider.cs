using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepo;
        private readonly IAspNetUserRepository _aspNetUserRepo;

        public UserProvider(IUserRepository userRepo, IAspNetUserRepository aspNetUserRepo)
        {
            _userRepo = userRepo;
            _aspNetUserRepo = aspNetUserRepo;
        }

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

        public DomainUserDTO GetByName(string lastName,string firstName)
        {
            var user = _userRepo.Get(u => u.FirstName.Trim().Equals(firstName) && u.LastName.Trim().Equals(lastName));
            return Mapper.Map<DomainUserDTO>(user);
        }

    }
}
