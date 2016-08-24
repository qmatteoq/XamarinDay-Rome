using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TrackSeries.Backend.Startup))]

namespace TrackSeries.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}