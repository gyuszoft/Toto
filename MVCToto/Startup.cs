using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCToto.Startup))]
namespace MVCToto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
