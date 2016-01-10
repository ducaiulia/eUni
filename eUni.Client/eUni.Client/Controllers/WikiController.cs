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
    public class WikiController : Controller
    {
        // GET: Wiki
        public ActionResult Index(string Module, string Course)
        {
            if (Course != null)
                ViewBag.Module = JsonConvert.DeserializeObject(Module);
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> CreateWiki(string wiki)
        {
            var wikiLocal = JsonConvert.DeserializeObject<WikiViewModel>(wiki);
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<string, WikiViewModel>("/WikiPage/Add", wikiLocal);
            var data = new RouteValueDictionary {{"Module", wikiLocal.Module}};
            return RedirectToAction("Index", "Module", data);
        }

        public class WikiViewModel
        {
            public int ModuleId { get; set; }
            public string Content { get; set; }
            public string Module { get; set; }
        }
    }
}