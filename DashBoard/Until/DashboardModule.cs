using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using Ninject.Modules;

namespace DashBoard.Until
{
    public class DashboardModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IApplicantService>().To<ApplicantService>();
        }
    }
}