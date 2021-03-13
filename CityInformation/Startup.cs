using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CityInformation.Startup))]
namespace CityInformation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
