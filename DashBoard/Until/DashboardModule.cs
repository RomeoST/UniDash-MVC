using System;
using Ninject;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;


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
        }
    }
}