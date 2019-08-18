using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektNutrition.Startup))]
namespace ProjektNutrition
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
