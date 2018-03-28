using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DashBoard.Attributes;
using DashBoard.BLL.Interfaces;
using DashBoard.Model.Models;
using DashBoard.Models;

namespace DashBoard.Controllers
{
    [Authorize, Permission]
    public class PermissionController : Controller
    {
        private IUserService UserService;
        private IRoleService RoleService;

        public PermissionController(IUserService user, IRoleService role)
        {
            UserService = user;
            RoleService = role;
        }

        public async Task<ActionResult> Index()
        {
            var users = await UserService.GetAll();
            var model = users.Select(Mapper.Map<ClientProfile, AccessEditFormModel>).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> AccessGetRoles(string id)
        {
            var user = await UserService.FindByName(id);
            if (user == null)
                return Json(new { model = "failed", message = "Не вдалося відобразити дані" }, JsonRequestBehavior.AllowGet);
            var roles = RoleService.GetAll();

            var listRoles = new List<BaseFormModel>();
            foreach (var identityUserRole in user.ClientProfile.DutUser.Roles)
            {
                var firstOrDefault = roles.FirstOrDefault(p => p.Id == identityUserRole.RoleId);
                if (firstOrDefault != null)
                    listRoles.Add(new BaseFormModel { Id = identityUserRole.RoleId, Name = firstOrDefault.Name });
            }

            ViewBag.id = "listAccess";

            return PartialView("_PartialDefaultTable", listRoles);
        }
    }
}