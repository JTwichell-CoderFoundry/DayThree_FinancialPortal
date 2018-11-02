using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DayThree_FinancialPortal.Startup))]
namespace DayThree_FinancialPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
