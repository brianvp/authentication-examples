using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ModelManagerOAuthIndividual.Startup))]
namespace ModelManagerOAuthIndividual
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
