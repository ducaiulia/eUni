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
        public ActionResult Index(string Module, string Course)
        {
            if (Course != null)
                ViewBag.Course = JsonConvert.DeserializeObject(Course);
            if (Module != null)
                ViewBag.Module = JsonConvert.DeserializeObject(Module);
            return View();
        }
        public async Task<ActionResult> Content(string Module, string Course)
        {
            var m = JsonConvert.DeserializeObject(Module);
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Files = await ApiService.GetAsync<IEnumerable<dynamic>, int>("/File/DownloadLink", "moduleId", (int)(((dynamic)m).ModuleId));
            ViewBag.Files = Files;
            ViewBag.Module = Module;
            if (Course != null)
                ViewBag.c = JsonConvert.DeserializeObject(Course);
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