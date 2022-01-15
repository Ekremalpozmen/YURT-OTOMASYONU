using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YURT_OTOMASYONU.ViewModels.Security;

namespace YURT_OTOMASYONU.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var loginResult = _securityService.Login(model);
            //    if (loginResult != null)
            //    {
            //        FormsAuthentication.SetAuthCookie(loginResult.Id.ToString(), model.RememberMe);

            //        return RedirectToAction("Index", "Dashboard");
            //    }
            //    else
            //    {
            //        ViewBag.kullanıcıyokmesaj = "Geçersiz Kullanıcı Adı veya Şifre";
            //        return View();
            //    }
            //}
            return View();
        }
    }
}