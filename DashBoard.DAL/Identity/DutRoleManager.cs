using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.DAL.Identity
{
    public class DutRoleManager : RoleManager<DutRole>
    {
        public DutContext DataBase { get; set; }

        public DutRoleManager(DutContext context) : base(new RoleStore<DutRole>(context)) => DataBase = context;

        /// <summary>
        /// Получить все роли у которых есть доступ к контроллеру и методу
        /// </summary>
        /// <param name="controller">Контроллер</param>
        /// <param name="action">Метод</param>
        /// <returns></returns>
        public IQueryable<DutRole> GetPermissionRoles(string controller, string action)
        {
            return DataBase.PermissionRoleses.Where(p => p.Controller.Name == controller && p.Action.Name == action)
                .Select(p => p.DutRole);
        }
    }
}
