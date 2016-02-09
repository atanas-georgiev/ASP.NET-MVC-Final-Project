using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Planex.Web.Startup))]
namespace Planex.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
