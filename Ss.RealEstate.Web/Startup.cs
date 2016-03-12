using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ss.RealEstate.Web.Startup))]
namespace Ss.RealEstate.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
