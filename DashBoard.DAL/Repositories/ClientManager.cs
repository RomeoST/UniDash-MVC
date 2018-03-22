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
    public class ClientManager : IClientManager
    {
        public DutContext DataBase { get; set; }

        public ClientManager(DutContext db)
        {
            DataBase = db;
        }

        public async Task CreateAsync(ClientProfile profile)
        {
            DataBase.ClientProfiles.Add(profile);
            await DataBase.SaveChangesAsync();
        }

        public void Create(ClientProfile profile)
        {
            DataBase.ClientProfiles.Add(profile);
            DataBase.SaveChanges();
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            return DataBase.ClientProfiles.ToList();
        }

        public async Task<ClientProfile> FindByName(string name)
        {
            return await DataBase.ClientProfiles.FirstOrDefaultAsync(p => p.DutUser.UserName == name);
        }

        public bool HasPermission(string name)
        {
            return true;
            //DataBase.Roles.
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
