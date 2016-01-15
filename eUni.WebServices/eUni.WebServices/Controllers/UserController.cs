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
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

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

        [Route("GetByUsername")]
        public async Task<IHttpActionResult> GetByUsername(string username)
        {
            var uname = JsonConvert.DeserializeObject<string>(username);
            return Ok(Mapper.Map<UserViewModel>(_userProvider.GetByUserName(uname)));
        }

        [Route("AllUsers")]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            List<DomainUserDTO> users = _userProvider.GetAllUsers();
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all users"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllUsersWithPagination")]
        public async Task<IHttpActionResult> GetAllUsersWithPagination(int? pageNumber, int? pageSize)
        {
            var filter = new PaginationFilter()
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();


            List<DomainUserDTO> users = _userProvider.GetAllUsersWithPagination(filter);
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all users"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllStudentsWithPagination")]
        public async Task<IHttpActionResult> GetAllStudentsWithPagination(int? pageNumber, int? pageSize)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var filter = new PaginationFilter()
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            List<DomainUserDTO> users = _userProvider.GetAllStudentsWithPagination(filter);
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all students"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllStudents")]
        public async Task<IHttpActionResult> GetAllStudents()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            
            List<DomainUserDTO> users = _userProvider.GetAllStudents();
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all students"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllTeachers")]
        public async Task<IHttpActionResult> GetAllTeachers()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            List<DomainUserDTO> users = _userProvider.GetAllTeachers();
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all teachers"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllTeachersWithPagination")]
        public async Task<IHttpActionResult> GetAllTeachersWithPagination(int? pageNumber, int? pageSize)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var filter = new PaginationFilter()
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            List<DomainUserDTO> users = _userProvider.GetAllTeachersWithPagination(filter);
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all teachers"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllAdmins")]
        public async Task<IHttpActionResult> GetAllAdmins()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            List<DomainUserDTO> users = _userProvider.GetAllAdmins();
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all admins"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllAdminsWithPagination")]
        public async Task<IHttpActionResult> GetAllAdminsWithPagination(int? pageNumber, int? pageSize)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var filter = new PaginationFilter()
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            List<DomainUserDTO> users = _userProvider.GetAllAdminsWithPagination(filter);
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all admins"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllStudentsByCourse")]
        public async Task<IHttpActionResult> GetAllStudentsByCourseId(int courseId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            List<DomainUserDTO> users = _userProvider.GetAllStudentsByCourseId(courseId);
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all admins"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("AllStudentsByCourseWithPagination")]
        public async Task<IHttpActionResult> GetAllStudentsByCourseWithPagination(int courseId, int? pageNumber, int? pageSize)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var filter = new PaginationFilter()
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            List<DomainUserDTO> users = _userProvider.GetAllStudentsByCourseIdPagination(courseId, filter);
            var allUsers = Mapper.Map<IEnumerable<UserViewModel>>(users);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get all admins"));
            return Content(HttpStatusCode.OK, allUsers);
        }

        [Route("EnrollUserToCourse")]
        [HttpGet]
        public async Task<IHttpActionResult> EnrollUserToCourse(int courseId, int userId)
        {
            var result = _userProvider.EnrollUserToCourse(courseId, userId);

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Enroll user to course"));

            return Content(HttpStatusCode.OK, new ResultViewModel() { Succeeded = result.Succeeded, ErrorMessage = result.ErrorMessage });
        }


    }
}
