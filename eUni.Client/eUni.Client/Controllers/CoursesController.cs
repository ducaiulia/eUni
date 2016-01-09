using System.Collections.Generic;
using System.Web.Mvc;

namespace EUni_Client.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            ViewBag.Courses = new List<string>
            {
                "Course1", "Course2", "Course3", "Course4", "Course5"
            };
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Course()
        {
            ViewBag.Modules = new List<string>
            {
                "Module1", "Module2", "Module3", "Module4", "Module5"
            };
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