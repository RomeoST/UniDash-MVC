using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    /// <summary>
    /// Клас логіки роботи з детальними даними користувача
    /// </summary>
    public class ClientManager : RepositoryBase<ClientProfile>, IClientManager
    {
        public ClientManager(IDataBaseFactory factory) : base(factory) { }
    }

    public interface IClientManager : IRepository<ClientProfile> { }
}
