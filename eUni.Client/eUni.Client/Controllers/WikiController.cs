using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public RedirectToRouteResult CreateWiki(string wiki)
        {
            return RedirectToAction("Index");
        }
    }
}