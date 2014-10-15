using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DebtGroup.Startup))]
namespace DebtGroup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
