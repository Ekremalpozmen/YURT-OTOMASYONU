using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YURT_OTOMASYONU.Services.Security;
using YURT_OTOMASYONU.ViewModels.Security;

namespace YURT_OTOMASYONU.Controllers
{
    public class LoginController : Controller
    {
        private readonly SecurityService _securityService;
        public LoginController(SecurityService securityService)
        {
            _securityService = securityService;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = _securityService.Login(model);
                if (loginResult != null)
                {
                    FormsAuthentication.SetAuthCookie(loginResult.Id.ToString(), model.RememberMe);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.kullanıcıyokmesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                    return View();
                }
            }
            return View();
        }
    }
}