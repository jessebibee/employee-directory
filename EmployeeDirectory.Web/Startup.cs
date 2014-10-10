using EmployeeDirectory.Web.App_Start;
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
            
            //Test/Dev mode only:
            DbInitialization.InitializeIdentityUsers();
            DbInitialization.InitializeEmployeeRecords(0);
        }
    }
}
