using System;
using System.Web.Mvc;
using DashBoard.Attributes;
using Ninject;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using DashBoard.DAL.EF;
using DashBoard.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Web.Common;
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
            Bind<ISubmissionService>().To<SubmissionService>();

            // TODO: В плане архитектуры хз
            //Bind<DutContext>().ToSelf().InRequestScope();
            //Bind(typeof(UserManager<>)).ToSelf();
            //Bind(typeof(UserStore<>)).ToSelf();

            this.BindFilter<PermissionFilter>(FilterScope.Controller, 0).WhenControllerHas<PermissionAttribute>();
        }
    }
}