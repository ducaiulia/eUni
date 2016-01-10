using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EUni_Client.Controllers
{
    public class WikiController : Controller
    {
        // GET: Wiki
        public ActionResult Index()
        {
            return View();
        }

        public RedirectToRouteResult CreateWiki(object o)
        {
            return RedirectToAction("Index");
        }
    }
}