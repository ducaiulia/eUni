using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            
            return View(tests);
        }

        public ActionResult CreateTest(int moduleId)
        {
            return View(moduleId);
        }

        //public async Task<ActionResult> CreateTest(string testName, int moduleId)
        //{
        //    var apiService = Session.GetApiService();
        //    var result = await apiService.PostAsyncWithReturn<string, object>("/Test/Add", new { Name = testName, CourseCode = moduleId });
        //    return null;
        //}
    }
}