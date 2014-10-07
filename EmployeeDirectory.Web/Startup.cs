using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeDirectory.Web.Startup))]
namespace EmployeeDirectory.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
