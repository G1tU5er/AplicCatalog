using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AplicCatalog.Startup))]
namespace AplicCatalog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
