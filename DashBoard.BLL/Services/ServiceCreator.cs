using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Repositories;

namespace DashBoard.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitWork(connection));
        }

        public IRoleService CreateRoleService(string connection)
        {
            return new RoleService(new IdentityUnitWork(connection));
        }
    }
}
