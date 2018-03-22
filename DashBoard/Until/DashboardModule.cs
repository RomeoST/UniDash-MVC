using System;
using System.Web.Mvc;
using DashBoard.Attributes;
using Ninject;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using Ninject.Web.Mvc.FilterBindingSyntax;


namespace DashBoard.Until
{
    using Ninject.Modules;
    public class DashboardModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IApplicantService>().To<ApplicantService>();
            Bind<IUStructService>().To<UStructService>();

            this.BindFilter<PermissionFilter>(FilterScope.Controller, 0).WhenControllerHas<PermissionAttribute>();
        }
    }
}