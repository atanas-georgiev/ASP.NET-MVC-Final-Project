using Microsoft.Owin;

[assembly: OwinStartup(typeof(Planex.Web.Startup))]

namespace Planex.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}