using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers
{
    public class UserProvider
    {
        private IUserRepository userRepo = new UserRepository(new ApplicationDbContext()); 

        public List<DomainUserDTO> GetAllUsers()
        {
            IEnumerable<DomainUser> enumerableAllUsers = userRepo.GetAll();
            List<DomainUserDTO> allUsers = new List<DomainUserDTO>();
            foreach(var currentUser in enumerableAllUsers)
            {
                allUsers.Add(new DomainUserDTO()
                {
                    DomainUserId = currentUser.DomainUserId,
                    Email = currentUser.Email,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    MatriculationNumber = currentUser.MatriculationNumber
                });
            }
            return allUsers;
        }
    }
}
