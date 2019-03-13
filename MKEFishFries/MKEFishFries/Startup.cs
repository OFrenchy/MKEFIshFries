using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MKEFishFries.Startup))]
namespace MKEFishFries
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
