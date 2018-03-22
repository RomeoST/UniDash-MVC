using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile profile);
        Task CreateAsync(ClientProfile profile);
        IEnumerable<ClientProfile> GetAll();
        Task<ClientProfile> FindByName(string name);

        bool HasPermission(string name);
    }
}
