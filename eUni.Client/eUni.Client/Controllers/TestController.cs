using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace EUni_Client.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(string m)
        {
            ViewBag.m = JsonConvert.DeserializeObject(m);
            return View();
        }
    }
}