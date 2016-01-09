using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EUni_Client.Startup))]
namespace EUni_Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
