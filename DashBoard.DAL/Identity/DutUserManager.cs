using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;

namespace DashBoard.DAL.Identity
{
    public class DutUserManager : UserManager<DutUser>
    {
        public DutUserManager(IUserStore<DutUser> store): base(store) { }
    }
}
