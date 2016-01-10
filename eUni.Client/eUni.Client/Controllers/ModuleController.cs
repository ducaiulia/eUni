using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EUni_Client.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Index(object c)
        {
            ViewBag.c = c;
            return View();
        }
        public ActionResult Content(object c)
        {
            ViewBag.c = c;
            return View();
        }

        public ActionResult UploadHomework()
        {
            return View();
        }
        public ActionResult Homework()
        {
            return View();
        }
    }
}