using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhoonBook.Startup))]
namespace PhoonBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
