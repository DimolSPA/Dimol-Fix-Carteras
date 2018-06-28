using System.Web;
using System.Web.Mvc;

namespace Dimol.Carteras.Mantenedores
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}