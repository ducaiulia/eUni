using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EUni_Client.Models;
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
            var Files =
                await
                    ApiService.GetAsync<IEnumerable<dynamic>, int>("/File/DownloadLink", "moduleId",
                        (int) (((dynamic) m).ModuleId));
            ViewBag.Files = Files;
            //get all wikis
            var WikiPages =
                await
                    ApiService.GetAsync<IEnumerable<dynamic>, int>("/WikiPage/GetAllWikiPagesByModule", "moduleId",
                        (int) (((dynamic) m).ModuleId));
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


        public async Task<ActionResult> Homework(string Module, string Course)
        {
            dynamic module = ViewBag.Module = JsonConvert.DeserializeObject(Module);
            if (Course != null)
                ViewBag.Course = JsonConvert.DeserializeObject(Course);

            var apiService = Session.GetApiService();
            var moduleId = (int)module.ModuleId;
            var homeworks = await apiService.GetAsync<IEnumerable<object>, int>("/Homework/AllHomeworksByModuleId", "moduleId", moduleId);
            return View(homeworks);
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

        public ActionResult CreateModule(string course)
        {
            var courseCode = ((dynamic)JsonConvert.DeserializeObject(course)).CourseCode;
            ViewBag.CourseCode = courseCode;
            ViewBag.Course = course;
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



        [HttpPost]
        public async Task<ActionResult> Create(ModuleViewModel vm)
        {
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<string, object>("/Module/Add", new {Name = vm.Name, CourseCode = vm.CourseCode});
            return RedirectToAction("Course", "Courses", new RouteValueDictionary() { {"c", vm.Course} });
        }


        public async Task<ActionResult> CreateResource(string Module)
        {
            var m = JsonConvert.DeserializeObject(Module);
            var apiService = Session.GetApiService();
            var files =
                await
                    apiService.GetAsync<IEnumerable<dynamic>, int>("/File/DownloadLink", "moduleId",
                        (int)(((dynamic)m).ModuleId));
            ViewBag.Files = files;
            ViewBag.Module = Module;
            return View();
        }


        [HttpPost]
        public async Task<RedirectToRouteResult> UploadModuleResource(FileViewModel fileViewModel)
        {
            dynamic module = JsonConvert.DeserializeObject(fileViewModel.Module);
            var file = fileViewModel.Files[0];
            var bytes = file.InputStream.ToByteArray();
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<object, object>("/File/UploadFile", new
            {
                Filename = file.FileName,
                ContentFile = bytes,
                module.ModuleId
            });
            return RedirectToAction("CreateResource", "Module", new RouteValueDictionary { { "Module", fileViewModel.Module } });
        }


        public async Task<ActionResult> CreateQuestions(int moduleId)
        {
            var apiService = Session.GetApiService();
            var moduleQuestions = await apiService.GetAsync<IList<QuestionViewModel>, int>("/Question/GetAllByModuleId", "moduleId", moduleId);
            ViewBag.ModuleId = moduleId;

            return View(moduleQuestions);
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion(string text, int score, int moduleId, IList<string> answers, IList<string> isCorrect)
        {
            var apiService = Session.GetApiService();
            var questionId = await apiService.PostAsyncWithReturn<int, object>("/Question/Add", new { Text = text, Score = score, ModuleId = moduleId });

            var answersVm = new List<AnswerViewModel>();

            for (int i = 0; i < answers.Count(); i++)
            {
                answersVm.Add(new AnswerViewModel {QuestionId = questionId, Text = answers[i], IsCorrect = bool.Parse(isCorrect[i])});
            }

            await apiService.PostAsync("/Answer/CreateAnswersForQuestion", new { QuestionId = questionId, Answers = answersVm});

            return RedirectToAction("CreateQuestions", "Module", new RouteValueDictionary() { { "moduleId", moduleId } });
        }
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
