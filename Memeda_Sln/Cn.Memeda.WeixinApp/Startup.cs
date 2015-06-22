using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cn.Memeda.WeixinApp.Startup))]
namespace Cn.Memeda.WeixinApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
