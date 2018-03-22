using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    /// <summary>
    /// Клас логіки роботи з паравами доступу до методів
    /// </summary>
    public class PermissionManager : RepositoryBase<PermissionRoles>, IPermission
    {
        public PermissionManager(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public class ControllerManager : RepositoryBase<TController>, IController
    {
        public ControllerManager(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public class ActionManager : RepositoryBase<TAction>, IAction
    {
        public ActionManager(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    #region Interfaces
    public interface IPermission : IRepository<PermissionRoles> { }
    public interface IController : IRepository<TController> { }
    public interface IAction : IRepository<TAction> { }
    #endregion

}
