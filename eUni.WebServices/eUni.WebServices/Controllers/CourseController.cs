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

namespace eUni.WebServices.Controllers
{

    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {

        private UserProvider userProvider = new UserProvider();
        private CourseProvider courseProvider = new CourseProvider();

        [Route("Add")]
        public async Task<IHttpActionResult> Add(CourseViewModel course)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            CourseDTO dtoCourse = Mapper.Map<CourseDTO>(course);
            dtoCourse.Teacher = userProvider.GetByUserName(Helpers.TokenHelper.GetFromToken(token, "username"));
            courseProvider.CreateCourse(dtoCourse);
            return Content(HttpStatusCode.OK, "Created successfully");
        }
    }
}
