using System;
using System.Web.Mvc;
using eUni.WebServices.Models;

namespace eUni.WebServices.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            Response.StatusCode = 500;
            return View("Error");
        }
    }
}
