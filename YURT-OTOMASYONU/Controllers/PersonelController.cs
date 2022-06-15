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
                Ilce = model.Ilce,
                DogumYeri = model.DogumYeri,
                DogumTarihi = model.DogumTarihi,
                AnneAdi = model.AnneAdi,
                BabaAdi = model.BabaAdi,
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
        public ActionResult EditStudent(int ogrenciId)
        {
            var model = db.Ogrenci.FirstOrDefault(x => x.Id == ogrenciId);
            return PartialView("~/Views/Personel/_EditStudent.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditStudent(Ogrenci ogrenci)
        {

            var editStudent = db.Ogrenci.FirstOrDefault(x => x.Id == ogrenci.Id);
            editStudent.Ad = ogrenci.Ad;
            editStudent.Soyad = ogrenci.Soyad;
            editStudent.TCKimlikNo = ogrenci.TCKimlikNo;
            editStudent.KanGrubu = ogrenci.KanGrubu;
            editStudent.Il = ogrenci.Il;
            editStudent.Ilce = ogrenci.Ilce;
            editStudent.DogumYeri = ogrenci.DogumYeri;
            editStudent.DogumTarihi = ogrenci.DogumTarihi;
            editStudent.AnneAdi = ogrenci.AnneAdi;
            editStudent.BabaAdi = ogrenci.BabaAdi;
            editStudent.TelefonNo = ogrenci.TelefonNo;
            editStudent.Adres = ogrenci.Adres;
            editStudent.Okul = ogrenci.Okul;
            editStudent.Bölüm = ogrenci.Bölüm;
            editStudent.Fakülte = ogrenci.Fakülte;
            editStudent.GirisTarihi = ogrenci.GirisTarihi;
            editStudent.Email = ogrenci.Email;
            editStudent.KullaniciAdi = ogrenci.KullaniciAdi;
            editStudent.Sifre = ogrenci.Sifre;
            editStudent.OdaNo = ogrenci.OdaNo;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult PermissionDetail(int izinId)
        {
            //izin detayı
            var model = db.Izinler.FirstOrDefault(x => x.Id == izinId);
            return PartialView("~/Views/Personel/_PermissionDetail.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteStudent(int id)
        {
            var deleteStudent = db.Ogrenci.FirstOrDefault(x => x.Id == id);
            db.Ogrenci.Remove(deleteStudent);
            db.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult DeletePersonel(int id)
        {
            var deletePersonel = db.Personel.FirstOrDefault(x => x.Id == id);
            db.Personel.Remove(deletePersonel);
            db.SaveChanges();
            return View();
        }


        public ActionResult AddFood()
        {
            return PartialView("~/Views/Personel/_AddFood.cshtml");
        }

        [HttpPost]
        public ActionResult AddFood(YemekListesi yemeklistesi)
        {
            var yemek = new YemekListesi()
            {
                Yemek1 = yemeklistesi.Yemek1,
                Yemek2 = yemeklistesi.Yemek2,
                Yemek3 = yemeklistesi.Yemek3,
                Yemek4 = yemeklistesi.Yemek4,
                Tarih = DateTime.Now
            };
            db.YemekListesi.Add(yemek);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelList()
        {
            var model = db.Personel.ToList();
            return View(model);
        }

        public ActionResult AddPersonel()
        {
            return PartialView("~/Views/Personel/_AddPersonel.cshtml");
        }

        [HttpPost]
        public JsonResult SearchPersonel(string personelName)
        {

            var personel = db.Personel.Where(x => x.Ad == personelName).ToList();
            return Json(personel);
        }

    }
}