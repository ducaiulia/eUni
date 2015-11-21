using System;
using System.Net;
using System.Web.Mvc;
using eUni.WebServices.Models;

namespace eUni.WebServices.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return View("Error");
        }
    }
}
