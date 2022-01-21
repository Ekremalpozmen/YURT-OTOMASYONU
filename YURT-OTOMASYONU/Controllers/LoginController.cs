using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YURT_OTOMASYONU.Data;
using YURT_OTOMASYONU.Services.Security;
using YURT_OTOMASYONU.ViewModels.Security;

namespace YURT_OTOMASYONU.Controllers
{
    public class LoginController : Controller
    {
        YurtOtomasyonEntities db = new YurtOtomasyonEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var ogrenciInDb = db.Ogrenci.FirstOrDefault(x => x.Sifre == model.Password && x.KullaniciAdi == model.Username);
            if (ogrenciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(ogrenciInDb.Ad, model.RememberMe);
                return RedirectToAction("Index", "Student");
            }
            else
            {
                ViewBag.kullanıcıyokmesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }
        [HttpPost]
        public ActionResult PersonelLogin(LoginViewModel model)
        {
            var personelInDb = db.Personel.FirstOrDefault(x => x.Sifre == model.Password && x.KullaniciAdi == model.Username);
            if (personelInDb != null)
            {
                FormsAuthentication.SetAuthCookie(personelInDb.Ad, model.RememberMe);
                return RedirectToAction("Index", "Personel");
            }
            else
            {
                ViewBag.kullanıcıyokmesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}