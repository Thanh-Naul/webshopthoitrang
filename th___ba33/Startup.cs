using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(th___ba33.Startup))]
namespace th___ba33
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
