using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(todolist.Startup))]
namespace todolist
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
