using System.Web.Mvc;

namespace EUni_Client.Controllers
{
    public class ForumsController : Controller
    {
        // GET: Forums
        public RedirectResult Index()
        {
            return Redirect("http://forums.euni.com");
        }
    }
}