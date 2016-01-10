﻿using System;
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
            //get all wikis
            var WikiPages = await ApiService.GetAsync<IEnumerable<dynamic>, int>("/WikiPage/GetAllWikiPagesByModule", "moduleId", (int)(((dynamic)m).ModuleId));
            ViewBag.WikiPages = WikiPages;
            //
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
            var bytes = fileViewModel.Files[0].InputStream.ToByteArray();
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<object, object>("/File/UploadFile", new
            {
                
            });
            return RedirectToAction("Index", "Module", new RouteValueDictionary { {"Module", fileViewModel.Module} });
        }

        public ActionResult Homework(string Module, string Course)
        {
            ViewBag.Module = JsonConvert.DeserializeObject(Module);
            if (Course != null)
                ViewBag.Course = JsonConvert.DeserializeObject(Course);
            return View();
        }
        public async Task<RedirectToRouteResult> CreateCourse(HomeworkCreateViewModel homework)
        {
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<string, HomeworkCreateViewModel>("/Homework/Add", homework);
            //var data = new RouteValueDictionary();
            //data.Add("Module", course.Course);
            return RedirectToAction("Index", "Module");
        }
        public async Task<ActionResult> CreateHomework(string Module)
        {
            ViewBag.Module = JsonConvert.DeserializeObject(Module);
            return View();
        }

        public async Task<RedirectToRouteResult> AddHomework(HomeworkCreateViewModel homeworkCreateViewModel)
        {
            var apiService = Session.GetApiService();
            var moduleId = ((dynamic)JsonConvert.DeserializeObject(homeworkCreateViewModel.Module)).ModuleId;
            var result = await apiService.PostAsyncWithReturn<object, object>("/Homework/Add", new
            {
                ModuleId = moduleId, homeworkCreateViewModel.Score, homeworkCreateViewModel.Text
            });
            return RedirectToAction("Index", "Module", new RouteValueDictionary() { {"Module", homeworkCreateViewModel.Module} });
        }


        public class FileViewModel
        {
            public string Module { get; set; }
            public HttpPostedFileBase[] Files { get; set; }
        }
        public class HomeworkCreateViewModel
        {
            public string Module { get; set; }
            public string Text { get; set; }
            public int Score { get; set; }
        }
    }
}