using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YURT_OTOMASYONU.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CallHelp()
        {
            return PartialView("~/Views/Personel/_CallHelp.cshtml");
        }

        public ActionResult AddFood()
        {
            return View();
        }
    }
}