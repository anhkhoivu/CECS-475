using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CECS_475___Lab_Assignment_09.Startup))]
namespace CECS_475___Lab_Assignment_09
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
