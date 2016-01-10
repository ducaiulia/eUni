using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EUni_Client.Services;
using Newtonsoft.Json;

namespace EUni_Client.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Index(string Module)
        {
            if (Module != null)
                ViewBag.Module = JsonConvert.DeserializeObject(Module);
            return View();
        }
        public async Task<ActionResult> Content(string course)
        {
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Modules = await ApiService.GetAsync<IEnumerable<dynamic>, int>("/Module/GetByCourse", "courseId", (int)(((dynamic)course).CourseId));
            ViewBag.Modules = Modules;
            if (course != null)
                ViewBag.c = JsonConvert.DeserializeObject(course);
            return View();
        }

        public ActionResult UploadHomework()
        {
            return View();
        }
        public ActionResult Homework()
        {
            return View();
        }
    }
}