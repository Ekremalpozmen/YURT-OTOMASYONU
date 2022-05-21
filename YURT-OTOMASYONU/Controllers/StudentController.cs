using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YURT_OTOMASYONU.Controllers.Abstract;
using YURT_OTOMASYONU.Data;
using YURT_OTOMASYONU.ViewModels.Student;

namespace YURT_OTOMASYONU.Controllers
{
    public class StudentController : BaseController
    {

        YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        // GET: Student
        public ActionResult Index()
        {
            ViewBag.OgrenciId = CurrentUser.Id;
            return View();
        }

        public ActionResult Permission()
        {
            return PartialView("~/Views/Student/_Permission.cshtml");
        }

        [HttpPost]
        public ActionResult Permission(PermissionViewModel model)
        {
            var izin = new Izinler
            {
                OgrenciId = CurrentUser.Id,
                BaslangicTarihi = model.BaslangicTarihi,
                BitisTarihi = model.BitisTarihi,
                Aciklama = model.Aciklama,
                İletisimNumarasi = model.İletisimNumarasi,
                KabulDurumu = false
            };

            db.Izinler.Add(izin);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Payment()
        {
            return PartialView("~/Views/Student/_Payment.cshtml");
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {
            var ödeme = new Odemeler
            {
                OgrenciId = CurrentUser.Id,
                OdemeTarihi = DateTime.Now,
                Tutar = model.Tutar
            };
            db.Odemeler.Add(ödeme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Profile(int ogrenciId)
        {
            var student = db.Ogrenci.FirstOrDefault(x => x.Id == ogrenciId);
            var profile = new StudentViewModel()
            {
                Id = student.Id,
                Ad = student.Ad,
                Soyad = student.Soyad,
                Okul = student.Okul,
                TCKimlikNo = student.TCKimlikNo,
                KanGrubu = student.KanGrubu,
                Il = student.Il,
                DogumYeri = student.DogumYeri,
                DogumTarihi = student.DogumTarihi,
                TelefonNo = student.TelefonNo,
                Adres = student.Adres,
                Bölüm = student.Bölüm,
                Email = student.Email,
                KullaniciAdi = student.KullaniciAdi,
            };
            return View(profile);
        }

        public ActionResult CallHelp()
        {
            return PartialView("~/Views/Student/_CallHelp.cshtml");
        }

        [HttpPost]
        public ActionResult CallHelp(CallHelpViewModel model, int envanterNo)
        {
            var yardım = new Yardımlar()
            {
                EnvanterNo = envanterNo,
                Aciklama = model.Aciklama,
                OgrenciId = CurrentUser.Id,
                KabulDurumu = false,
            };
            db.Yardımlar.Add(yardım);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditPassword()
        {
            return PartialView("~/Views/Student/_EditPassword.cshtml");
        }

        [HttpPost]
        public ActionResult EditPassword(EditPasswordViewModel model)
        {
            var currentUser = db.Ogrenci.FirstOrDefault(x => x.Id == CurrentUser.Id);

            var userPassword = currentUser.Sifre;

            if (userPassword == model.OldPassword)
            {
                if (model.NewPassword == model.NewPasswordAgain)
                {
                    currentUser.Sifre = model.NewPassword;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Mesaj = ("Şifreler Uyuşmuyor");
                    return ViewBag.Mesaj;
                }
            }
            else
            {
                ViewBag.Mesaj = ("Eski Şifre Yanlış");
                return ViewBag.Mesaj;
            }

            return RedirectToAction("Index");
        }


    }
}