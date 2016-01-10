using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using EUni_Client.Services;

namespace EUni_Client.Controllers
{
    public class LogsController : Controller
    {
        // GET: Logs
        public async Task<ActionResult> Index()
        {
            var apiService = Session.GetApiService();
            var logs = await apiService.GetAsync<IEnumerable<dynamic>>("/Log/Get");
            ViewBag.Logs = logs;
            return View();
        }
    }
}