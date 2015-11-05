using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using eUni.BusinessLogic.Providers;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Models;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private UserProvider userProvider = new UserProvider();

        public string Get(int id)
        {
            return "value";
        }

        [Route("AllUsers")]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            List<DomainUserDTO> users = userProvider.GetAllUsers();
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            return Content(HttpStatusCode.OK, allUsers);
        }
    }
}
