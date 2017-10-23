using DashBoard;
using DashBoard.App_Start;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using DashBoard.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi;
using Owin;

[assembly: OwinStartup(typeof(DashBoard.Startup))]

namespace DashBoard
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        { 
            app.CreatePerOwinContext(CreateUserService);
            app.CreatePerOwinContext(CreateRoleService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        private IUserService CreateUserService() => serviceCreator.CreateUserService("UniversityContext");
        private IRoleService CreateRoleService() => serviceCreator.CreateRoleService("UniversityContext");
    }
}