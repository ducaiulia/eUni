using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EUni_Client.Models;
using EUni_Client.Services;

namespace EUni_Client.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var error = TempData["ErrorMessage"];
            if (error != null)
            {
                ViewBag.ErrorMessage = error;
            }
            return View();
        }

        public async Task<RedirectToRouteResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var apiService = await LoginService.Login(loginViewModel.Email, loginViewModel.Password);
                Session[ServiceNames.ApiService] = apiService;
                return RedirectToAction("Index", "Home");
            }
            catch (ApiException)
            {
                TempData["ErrorMessage"] = "Invalid username/password";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public async Task<RedirectToRouteResult> RegisterUser(RegisterViewModel registerViewModel)
        {
            var response = await LoginService.Register(registerViewModel.Email, registerViewModel.ConfirmPassword);
            return RedirectToAction("Index");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public RedirectToRouteResult Logout()
        {
            Session[ServiceNames.ApiService] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}