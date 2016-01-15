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
            var error = TempData["ErrorMessage"];
            if (error != null)
            {
                ViewBag.ErrorMessage = error;
            }
            return View();
        }

        public async Task<RedirectToRouteResult> RegisterUser(RegisterViewModel registerViewModel)
        {
            try
            {
                var response = await LoginService.Register(registerViewModel.Email, registerViewModel.ConfirmPassword, registerViewModel.FirstName, registerViewModel.LastName);
                return RedirectToAction("Index");
            }
            catch (ApiException)
            {
                TempData["ErrorMessage"] = "The user already exists!";
                return RedirectToAction("Register");
            }
        }

        public async Task<RedirectToRouteResult> ManageUser(ChangePasswordViewModel changePasswordViewModel)
        {
            try
            {
                var apiService = Session.GetApiService();
                var response = await apiService.PostAsyncWithReturn("/Account/ChangePassword", changePasswordViewModel);
                return RedirectToAction("Index", "Home");
            }
            catch (ApiException)
            {
                TempData["ErrorMessage"] = "The user already exists!";
                return RedirectToAction("Index", "Mange");
            }
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