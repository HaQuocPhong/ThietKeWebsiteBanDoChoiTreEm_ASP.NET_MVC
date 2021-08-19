using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoChoiTreEmWeb.Startup))]
namespace DoChoiTreEmWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
