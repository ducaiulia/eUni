using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EUni_Client.Services;

namespace EUni_Client.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public async Task<ActionResult> Index()
        {
            var apiService = Session.GetApiService();
            var users = await apiService.GetAsync<IEnumerable<dynamic>>("/User/AllUsers");
            ViewBag.Users = users;
            return View();
        }
    }
}