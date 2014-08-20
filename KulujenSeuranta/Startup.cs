using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KulujenSeuranta.Startup))]
namespace KulujenSeuranta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
