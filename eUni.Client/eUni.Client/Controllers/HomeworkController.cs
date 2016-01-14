using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EUni_Client.Services;
using Newtonsoft.Json;

namespace EUni_Client.Controllers
{
    public class HomeworkController : Controller
    {
        // GET: Homework
        public async Task<ActionResult> Index(string hw, string module)
        {
            ViewBag.Homework = JsonConvert.DeserializeObject(hw);
            ViewBag.Module = JsonConvert.DeserializeObject(module);
            int homeworkId = ViewBag.Homework.HomeworkId;
            var apiService = Session.GetApiService();
            object homeworks = await apiService.GetAsync<IEnumerable<dynamic>, int>("/Homework/UploadedHomeworksByHomeworkId", "hwId", homeworkId);
            ViewBag.Homeworks = homeworks;
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> UploadHomework(HomweorkFileViewModel fileViewModel)
        {
            dynamic homework = JsonConvert.DeserializeObject(fileViewModel.Homework);
            var file = fileViewModel.Files[0];
            var bytes = file.InputStream.ToByteArray();
            var apiService = Session.GetApiService();
            var studentId = (await apiService.GetAsync<dynamic, string>("/User/GetByUsername", "username", apiService.Username)).DomainUserId;
            var result = await apiService.PostAsyncWithReturn<object, object>("/Homework/UploadHomeworkAnswer", new
            {
                StudentId = studentId,
                Filename = file.FileName,
                ContentFile = bytes,
                homeworkId = homework.HomeworkId
            });
            return RedirectToAction("Homework", "Module", new RouteValueDictionary { { "Module", fileViewModel.Module } });
        }

        public class HomweorkFileViewModel
        {
            public string Homework { get; set; }
            public string Module { get; set; }
            public HttpPostedFileBase[] Files { get; set; }
        }
    }
}