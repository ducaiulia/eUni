using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Models;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IUserProvider _userProvider;

        public UserController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public string Get(int id)
        {
            return "value";
        }

        [Route("AllUsers")]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            List<DomainUserDTO> users = _userProvider.GetAllUsers();
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction("Get all users");
            return Content(HttpStatusCode.OK, allUsers);
        }
    }
}
