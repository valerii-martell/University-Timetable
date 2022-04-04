using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversalTimetable.Startup))]
namespace UniversalTimetable
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
