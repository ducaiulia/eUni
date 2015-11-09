using AutoMapper;
using eUni.BusinessLogic.Providers;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using eUni.BusinessLogic.IProviders;

namespace eUni.WebServices.Controllers
{

    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        private IUserProvider _userProvider;
        private ICourseProvider _courseProvider;

        public CourseController(IUserProvider userProvider, ICourseProvider courseProvider)
        {
            _userProvider = userProvider;
            _courseProvider = courseProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(CourseViewModel course)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            CourseDTO dtoCourse = Mapper.Map<CourseDTO>(course);
            dtoCourse.Teacher = _userProvider.GetByUserName(Helpers.TokenHelper.GetFromToken(token, "username"));
            _courseProvider.CreateCourse(dtoCourse);
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("AssignTeacher")]
        public async Task<IHttpActionResult> AssignTeacher(string lastName, string firstName, string courseCode)
        {
            CourseDTO course=_courseProvider.GetByCourseCode(courseCode);
            course.Teacher = _userProvider.GetByName(lastName,firstName);
            _courseProvider.UpdateCourse(course);
            return Content(HttpStatusCode.OK, "Assigned successfully");
        }
    }
}
