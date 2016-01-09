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
using eUni.WebServices.Helpers;
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

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all users"));
            return Content(HttpStatusCode.OK, allUsers);
        }


        [Route("EnrollUserToCourse")]
        public async Task<IHttpActionResult> EnrollUserToCourse(CourseViewModel course, UserViewModel user)
        {
            var result = _userProvider.EnrollUserToCourse(course.CourseCode, user.DomainUserId);

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Enroll user to course"));

            return Content(HttpStatusCode.OK, new ResultViewModel() { Succeeded = result.Succeeded, ErrorMessage = result.ErrorMessage });
        }
    }
}
