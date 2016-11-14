using System.Web;
using System.Web.Mvc;

namespace CECS_475___Lab_Assignment_09
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
