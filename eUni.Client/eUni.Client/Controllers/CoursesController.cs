using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        public ActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> Course(string c)
        {
            var course = JsonConvert.DeserializeObject(c);
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Modules = await ApiService.GetAsync<IEnumerable<dynamic>, int>("/Module/GetByCourse", "courseId", (int)(((dynamic)course).CourseId));

            if (c != null)
                ViewBag.c = JsonConvert.DeserializeObject(c);

            ViewBag.Modules = Modules;
            return View();
        }
        public ActionResult AssignTeacher()
        {
            ViewBag.Teachers = new List<string>
            {
                "Teacher1", "Teacher2", "Teacher3", "Teacher4", "Teacher5"
            };
            ViewBag.Courses = new List<string>
            {
                "Course1", "Course2", "Course3", "Course4", "Course5"
            };
            return View();
        }
    }
}