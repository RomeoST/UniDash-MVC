using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.Infrastructure;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    public class UserRepository : RepositoryBase<DutUser>, IUserRepository
    {
        public UserRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public interface IUserRepository : IRepository<DutUser> { }
}
