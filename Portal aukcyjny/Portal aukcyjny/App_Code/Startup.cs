using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Portal_aukcyjny.Startup))]
namespace Portal_aukcyjny
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
