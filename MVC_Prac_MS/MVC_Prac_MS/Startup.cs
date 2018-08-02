using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Prac_MS.Startup))]
namespace MVC_Prac_MS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
