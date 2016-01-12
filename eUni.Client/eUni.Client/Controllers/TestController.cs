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
    }
}