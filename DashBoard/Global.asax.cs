using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using DashBoard.Mapping;
using DashBoard.Until;
using Ninject;


namespace DashBoard
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfiguration.Configure();

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}
