using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using AutoMapper;
using Dropbox.Api;
using Dropbox.Api.Files;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Helpers;
using eUni.WebServices.Models;

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
            dtoCourse.Teacher = _userProvider.GetByUserName(TokenHelper.GetFromToken(token, "username"));
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

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IHttpActionResult> UploadFile(string filename, [FromBody]byte[] contentFile)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var key = WebConfigurationManager.AppSettings["DropboxToken"];
            var dbx = new DropboxClient(key);

            string username = TokenHelper.GetFromToken(token, "username");
            string role = TokenHelper.GetFromToken(token, "role");

            string path = "/" + role + "/" + username + "/" + filename;
            
            using (var memoryStream = new MemoryStream(contentFile))
            {
                var uploaded = await dbx.Files.UploadAsync(
                    path,
                    WriteMode.Add.Instance,
                    body: memoryStream);
            }

            return null;
        }
    }
}
