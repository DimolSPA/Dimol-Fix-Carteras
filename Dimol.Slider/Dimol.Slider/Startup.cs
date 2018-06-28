using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dimol.Slider.Startup))]
namespace Dimol.Slider
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
