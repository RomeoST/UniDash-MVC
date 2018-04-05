using DashBoard.DAL.EF;
using DashBoard.DAL.Identity;
using DashBoard.DAL.Infrastructure;
using DashBoard.DAL.Repositories;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Web.Common;

namespace DashBoard.BLL.Infrastructure
{
    using Ninject.Modules;
    /// <summary>
    /// Модуль для создания зависимости UnitOfWork
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IDataBaseFactory>().To<DataBaseFactory>().WithConstructorArgument(_connectionString);
            Bind<IUnitOfWork>().To<IdentityUnitWork>();

            // Init UserManager
            Bind<DutContext>().ToSelf().InRequestScope().WithConstructorArgument(_connectionString);
            Bind(typeof(IUserStore)).To(typeof(DutUserStore)).InRequestScope();
            Bind(typeof(DutUserManager)).ToSelf().InRequestScope();

            Bind<IUserProfileRepository>().To<UserProfileRepository>();
            Bind<IPermissionRepository>().To<PermissionRepository>();
            Bind<IApplicantRepository>().To<ApplicantRepository>();
            Bind<ISubmissionRepository>().To<SubmissionRepository>();
            Bind<IUStructureRepository<Faculty>>().To<UStructureRepository<Faculty>>();
            Bind<IUStructureRepository<Institute>>().To<UStructureRepository<Institute>>();
            Bind<IUStructureRepository<Department>>().To<UStructureRepository<Department>>();
        }
    }
}
