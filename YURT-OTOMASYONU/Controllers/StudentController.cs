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
    }
}