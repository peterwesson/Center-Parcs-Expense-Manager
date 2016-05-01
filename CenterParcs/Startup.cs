using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(CenterParcs.Startup))]
namespace CenterParcs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
