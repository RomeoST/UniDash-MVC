using DashBoard.DAL.Infrastructure;
using DashBoard.DAL.Repositories;

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
            Bind<IUnitOfWork>().To<IdentityUnitWork>().WithConstructorArgument(_connectionString);
        }
    }
}
