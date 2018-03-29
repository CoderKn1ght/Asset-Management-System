using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asset_Management_System.Startup))]
namespace Asset_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
