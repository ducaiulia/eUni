using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
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
        private IFileProvider _fileProvider;

        public CourseController(IUserProvider userProvider, ICourseProvider courseProvider, IFileProvider fileProvider)
        {
            _userProvider = userProvider;
            _courseProvider = courseProvider;
            _fileProvider = fileProvider;
        }

        [Route("GetAllCourses")]
        public async Task<IHttpActionResult> GetAll()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var coursesDTO = _courseProvider.GetAll();
            var coursesViewModels = Mapper.Map<List<CourseViewModel>>(coursesDTO);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Courses "));
            return Content(HttpStatusCode.OK, coursesViewModels);
        }

        [Route("GetAllCoursesByStudId")]
        public async Task<IHttpActionResult> GetAllCoursesByStudId(int studId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var coursesDTO = _courseProvider.GetAllByStudentId(studId);
            var coursesViewModels = Mapper.Map<List<CourseViewModel>>(coursesDTO);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Courses by studId "));
            return Content(HttpStatusCode.OK, coursesViewModels);
        }

        [Route("Add")]
        [@Authorize("teacher")]
        public async Task<IHttpActionResult> Add(CourseViewModel course)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            CourseDTO dtoCourse = Mapper.Map<CourseDTO>(course);
            dtoCourse.Teacher = _userProvider.GetByUserName(TokenHelper.GetFromToken(token, "username"));
            _courseProvider.CreateCourse(dtoCourse);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Course created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("Remove")]
        [@Authorize("teacher")]
        public async Task<IHttpActionResult> Remove(int courseId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _courseProvider.DeleteCourseWithId(courseId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Course deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

        [Route("AssignTeacher")]
        [@Authorize("admin")]
        public async Task<IHttpActionResult> AssignTeacher(string lastName, string firstName, string courseCode)
        {
            CourseDTO course = _courseProvider.GetByCourseCode(courseCode);
            course.Teacher = _userProvider.GetByName(lastName, firstName);
            _courseProvider.UpdateCourse(course);

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Teacher assigned to course"));
            return Content(HttpStatusCode.OK, "Assigned successfully");
        }
    }
}
