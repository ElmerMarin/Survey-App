using System.Web.Mvc;

namespace WebAppSurvey
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Filters.VerificarSession());
        }
    }
}
