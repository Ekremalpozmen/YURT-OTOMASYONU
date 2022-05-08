using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YURT_OTOMASYONU.Data;
using YURT_OTOMASYONU.ViewModels.Student;

namespace YURT_OTOMASYONU.Controllers.Abstract
{
    [Authorize]
    public abstract class BaseController : Controller
    {
         
        protected override void Initialize(System.Web.Routing.RequestContext context)
        {
            base.Initialize(context);
        }

        public StudentViewModel CurrentUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                using (YurtOtomasyonEntities db = new YurtOtomasyonEntities())
                {
                    var contextUserId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
                    var user = db.Ogrenci.FirstOrDefault(x => x.Id == contextUserId);

                    if (user != null)
                    {
                        CurrentUser = new StudentViewModel()
                        {
                            Id = (int)user.Id,
                            Ad = user.Ad,
                            Soyad = user.Soyad,
                        };
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}