using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using YURT_OTOMASYONU.Data;
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
            if (ModelState.IsValid)
            {
                var ogrenci = db.Ogrenci.FirstOrDefault(x => x.KullaniciAdi == model.Username && x.Sifre == model.Password);
                if (ogrenci != null)
                {
                    FormsAuthentication.SetAuthCookie(ogrenci.Ad, model.RememberMe);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult PersonelLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var personel = db.Personel.FirstOrDefault(x => x.Sifre == model.Password && x.KullaniciAdi == model.Username);
                if (personel != null)
                {
                    FormsAuthentication.SetAuthCookie(personel.Ad, model.RememberMe);
                    return RedirectToAction("Index", "Personel");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}