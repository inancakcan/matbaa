using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KendoMatbaa.Startup))]
namespace KendoMatbaa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
