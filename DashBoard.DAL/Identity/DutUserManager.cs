using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.Infrastructure;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.DAL.Identity
{
    public class DutUserManager : UserManager<DutUser>
    {
        public DutUserManager(IUserStore store): base(store) { }
    }

    public interface IUserStore : IUserStore<DutUser> { }

    public class DutUserStore : UserStore<DutUser>, IUserStore
    {
        public DutUserStore(IDataBaseFactory context) : base(context.Get()) { }
    }
}
