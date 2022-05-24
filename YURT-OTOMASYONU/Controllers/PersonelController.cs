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

        public ActionResult AddFood()
        {
            return View();
        }
    }
}