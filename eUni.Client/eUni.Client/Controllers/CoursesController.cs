using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EUni_Client.Services;
using Newtonsoft.Json;

namespace EUni_Client.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public async Task<ActionResult> Index()
        {
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Courses = await ApiService.GetAsync<IEnumerable<dynamic>>("/Course/GetAllCourses");
            ViewBag.Courses = Courses;
            return View();
        }
        [HttpPost]
        public async Task<RedirectToRouteResult> CreateCourse(CreateCourseViewModel course)
        {
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<string, CreateCourseViewModel>("/Course/Add", course);
            //var data = new RouteValueDictionary();
            //data.Add("Module", course.Course);
            return RedirectToAction("Index", "Courses");
        }
        public async Task<ActionResult> Create()
        {
            //var ApiService = Session[ServiceNames.ApiService] as ApiService;
            //var Courses = await ApiService.GetAsync<IEnumerable<dynamic>>("/Course/GetAllCourses");
            //ViewBag.Courses = Courses;
            return View();
        }
        public async Task<ActionResult> Course(string c)
        {
            var course = JsonConvert.DeserializeObject(c);
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Modules = await ApiService.GetAsync<IEnumerable<dynamic>, int>("/Module/GetByCourse", "courseId", (int)(((dynamic)course).CourseId));

            if (c != null)
                ViewBag.Course = JsonConvert.DeserializeObject(c);

            ViewBag.Modules = Modules;
            return View();
        }
        public async Task<ActionResult> AssignTeacher()
        {
            ViewBag.Teachers = new List<string>
            {
                "Teacher1", "Teacher2", "Teacher3", "Teacher4", "Teacher5"
            };
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Courses = await ApiService.GetAsync<IEnumerable<dynamic>>("/Course/GetAllCourses");
            ViewBag.Courses = Courses;
            return View();
        }
    }

    public class CreateCourseViewModel
    {
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}