using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Weather.Website.Startup))]
namespace Weather.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
