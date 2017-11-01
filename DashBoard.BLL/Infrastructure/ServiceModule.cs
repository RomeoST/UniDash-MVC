using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.Interfaces;
using DashBoard.DAL.Repositories;
using Ninject.Modules;

namespace DashBoard.BLL.Infrastructure
{
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
