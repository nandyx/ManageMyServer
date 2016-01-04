using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyServerAdmin.Startup))]
namespace MyServerAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
