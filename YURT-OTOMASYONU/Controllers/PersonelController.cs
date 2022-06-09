using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YURT_OTOMASYONU.Data;
using YURT_OTOMASYONU.ViewModels.Personel;

namespace YURT_OTOMASYONU.Controllers
{
    public class PersonelController : Controller
    {
        YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        public ActionResult Index()
        {
            var ogrenciler = db.Ogrenci.ToList();
            return View(ogrenciler);
        }

        public ActionResult CallHelp()
        {
            var model = db.Yardımlar.ToList();
            return PartialView("~/Views/Personel/_CallHelp.cshtml", model);
        }
        public ActionResult PermissionList()
        {
            var model = db.Izinler.ToList();
            return PartialView("~/Views/Personel/_Permission.cshtml", model);
        }


        public ActionResult Payment()
        {
            //ödeme yapanlar
            var model = db.Odemeler.Where(m => m.OdemeDurumu == true).ToList();
            return PartialView("~/Views/Personel/_Payment.cshtml", model);
        }

        public ActionResult NoPayment()
        {
            //ödeme yapmayanlar
            var model = db.Odemeler.Where(m => m.OdemeDurumu == false).ToList();
            return PartialView("~/Views/Personel/_NoPayment.cshtml", model);
        }
        public ActionResult AddStudent()
        {
            return PartialView("~/Views/Personel/_AddStudent.cshtml");
        }
        [HttpPost]
        public ActionResult AddStudent(StudentAddViewModel model)
        {
            var student = new Ogrenci
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                TCKimlikNo = model.TCKimlikNo,
                KanGrubu = model.KanGrubu,
                Il = model.Il,
                Ilce=model.Ilce,
                DogumYeri=model.DogumYeri,
                DogumTarihi = model.DogumTarihi,
                AnneAdi=model.AnneAdi,
                BabaAdi=model.BabaAdi,
                TelefonNo = model.TelefonNo,
                Adres = model.Adres,
                Okul = model.Okul,
                Bölüm = model.Bölüm,
                Fakülte = model.Fakülte,
                GirisTarihi = model.GirisTarihi,
                Email = model.Email,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                OdaNo = model.OdaNo,
            };
            db.Ogrenci.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PermissionDetail(int izinId)
        {
            //izin detayı
            var model = db.Izinler.FirstOrDefault(x => x.Id == izinId);
            return PartialView("~/Views/Personel/_PermissionDetail.cshtml",model);
        }

        public ActionResult AddFood()
        {
            return View();
        }

    }
}