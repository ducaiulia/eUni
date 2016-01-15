using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EUni_Client.Models;
using EUni_Client.Services;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace EUni_Client.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public async Task<ActionResult> Index(int moduleId)
        {
            var apiService = Session.GetApiService();
            var tests = await apiService.GetAsync<IList<TestViewModel>, int>("/Test/GetAllTestsByModule", "moduleId", moduleId);

            foreach (var test in tests)
            {
                var g = await apiService.GetAsync<int?, object>("/Test/GetGradeForStudent", new Dictionary<string, object> {{"studentId", apiService.UserId}, {"testId", test.TestId}});
                test.Grade = g.Value;
            }

            ViewBag.ModuleId = moduleId;
            return View(tests);
        }

        [HttpPost]
        public async Task<ActionResult> Create(int moduleId, string name)
        {
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<string, object>("/Test/Add", new { Name = name, ModuleId = moduleId });

            return RedirectToAction("Index", "Test", new RouteValueDictionary { { "moduleId", moduleId } });
        }


        public async Task<ActionResult> TakeTest(int testId)
        {
            var apiService = Session.GetApiService();

            var studentId = (await apiService.GetAsync<dynamic, string>("/User/GetByUsername", "username", apiService.Username)).DomainUserId;
            ViewBag.StudentId = studentId;
            ViewBag.TestId = testId;
                        
            var test = await apiService.GetAsync<TestViewModel, int>("/Test/GetAllQuestionsByTestId", "testId", testId);
            int totalScore = 0;
            test.Questions.ForEach(q => q.Answers.ForEach(a =>
            {
                totalScore = a.IsCorrect ? totalScore += q.Score : totalScore += 0;
            }));
            ViewBag.TotalScore = totalScore;

            return View(test);
        }

        public async Task<RedirectToRouteResult> SubmitTest(IEnumerable<object> testData)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task SubmitFinalGrade(int studentId, int finalScore, int testId)
        {
            var apiService = Session.GetApiService();

            var result = await apiService.PostAsyncWithReturn<string, object>("/Test/UpdateGrade", new { StudentId = studentId, TestId = testId, Grade = finalScore });
        }

        public RedirectToRouteResult Redirect(int moduleId)
        {
            return RedirectToAction("Index", "Test", new RouteValueDictionary() {{"moduleId", moduleId}});
        }
    }
}