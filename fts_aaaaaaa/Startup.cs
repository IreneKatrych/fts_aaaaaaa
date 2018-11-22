using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fts_aaaaaaa.Startup))]
namespace fts_aaaaaaa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
