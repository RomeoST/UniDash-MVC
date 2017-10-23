using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.DAL.Identity
{
    public class DutRoleManager : RoleManager<DutRole>
    {
        public DutRoleManager(RoleStore<DutRole> store) : base(store) { }
    }
}
