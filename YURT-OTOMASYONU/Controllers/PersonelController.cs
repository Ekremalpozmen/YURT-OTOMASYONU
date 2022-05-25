using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YURT_OTOMASYONU.Data;

namespace YURT_OTOMASYONU.Controllers
{
    public class PersonelController : Controller
    {
        YurtOtomasyonEntities db = new YurtOtomasyonEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CallHelp()
        {
            var model = db.Yardımlar.ToList();
            return PartialView("~/Views/Personel/_CallHelp.cshtml",model);
        }
        public ActionResult PermissionList()
        {
           var model= db.Izinler.ToList();
            return PartialView("~/Views/Personel/_Permission.cshtml",model);
        }
<<<<<<< HEAD

        public ActionResult Payment()
        {
            //ödeme yapanlar
            var model = db.Odemeler.Where(m => m.OdemeDurumu == true).ToList();
            return PartialView("~/Views/Personel/_Payment.cshtml", model);
        }

=======
        public ActionResult Payment()
        {
            return PartialView("~/Views/Personel/_Payment.cshtml");
        }
>>>>>>> 34a90b3b36e2a6271f179ca0f3e566b6c87e05f8
        public ActionResult AddFood()
        {
            return View();
        }
    }
}