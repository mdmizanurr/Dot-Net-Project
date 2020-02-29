using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToyDemoProj.Startup))]
namespace ToyDemoProj
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
