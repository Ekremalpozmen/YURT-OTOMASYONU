using System.Web;
using System.Web.Mvc;

namespace YURT_OTOMASYONU
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
