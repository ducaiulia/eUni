using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            ViewBag.Module = JsonConvert.DeserializeObject(Module);
            if (Course != null)
                ViewBag.Course = JsonConvert.DeserializeObject(Course);
            return View();
        }

        public ActionResult UploadHomework()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> UploadFile(FileViewModel fileViewModel)
        {
            dynamic module = JsonConvert.DeserializeObject(fileViewModel.Module);
            var file = fileViewModel.Files[0];
            var bytes = file.InputStream.ToByteArray();
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<object, object>("/File/UploadFile", new
            {
                Filename = file.FileName,
                ContentFile = bytes, module.ModuleId
            });
            return RedirectToAction("Index", "Module", new RouteValueDictionary { { "Module", fileViewModel.Module } });
        }


        //public string Path { get; set; }
        //public string Filename { get; set; }
        //public byte[] ContentFile { get; set; }
        //public int ModuleId { get; set; }

        public ActionResult Homework(string Module, string Course)
        {
            ViewBag.Module = JsonConvert.DeserializeObject(Module);
            if (Course != null)
                ViewBag.Course = JsonConvert.DeserializeObject(Course);
            return View();
        }

        public class FileViewModel
        {
            public string Module { get; set; }
            public HttpPostedFileBase[] Files { get; set; }
        }
    }
}