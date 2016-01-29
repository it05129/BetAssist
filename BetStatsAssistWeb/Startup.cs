using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BetStatsAssistWeb.Startup))]
namespace BetStatsAssistWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
