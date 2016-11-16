using Microsoft.Owin;
using Owin;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Security;

[assembly: OwinStartupAttribute(typeof(MvcMovie.Startup))]
namespace MvcMovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "Hello",
                 url: "{controller}/{action}/{name}/{id}"
             );
        }
    }
}
