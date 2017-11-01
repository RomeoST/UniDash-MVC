using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DashBoard.Attributes;
using DashBoard.BLL.Interfaces;
using DashBoard.Model.Models;
using DashBoard.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace DashBoard.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserService UserService;
        private IRoleService RoleService;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserService userService, IRoleService roleService)
        {
            UserService = userService;
            RoleService = roleService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Производит аторизацию на сайте. Вход в систему
        /// </summary>
        /// <param name="model">Модель параметров с сайта</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAjax]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginFormModel model)
        {
            var claim = await Task.Run(()=> UserService.Authenticate(model.UserName, model.Password));

            if (claim == null)
                return Json(new {model = "failed", modelList = new[] {"Невірний логін або пароль"}},
                    JsonRequestBehavior.AllowGet);

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties{IsPersistent = true}, claim);

            return Json(new { model = "confirmed", href = "/dashboard" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAjax]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterFormModel model)
        {
            var userDto = Mapper.Map<RegisterFormModel, DutUser>(model);
            var details = await UserService.Create(userDto, model.Password);
            if (details.Successed)
            {
                var claim = await Task.Run(() => UserService.Authenticate(model.UserName, model.Password));
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);

                return Json(new { model = "confirmed", href = "/dashboard/welcome" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { model = "failed", modelList = new[]{details.Message} }, JsonRequestBehavior.AllowGet);
            }
        }

		[HttpGet]
        public ActionResult Logout()
		{
            AuthenticationManager.SignOut();
			return Redirect("Login");
		}

        [HttpPost]
        [ValidateAjax]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(EditUserFormModel model)
        {
            var newData = Mapper.Map<EditUserFormModel, DutUser>(model);
            var result = await Task.Run((() => UserService.EditProfile(newData)));

            return Json(new {model = result.Successed?"confirmed":"failed", message=result.Message}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> AccessGetRoles(string id)
        {
            var user = await UserService.FindByName(id);
            if (user == null)
                return Json(new {model = "failed", message = "Не вдалося відобразити дані"}, JsonRequestBehavior.AllowGet);
            var roles = RoleService.GetAll();

            var listRoles = new List<BaseFormModel>();
            foreach (var identityUserRole in user.ClientProfile.DutUser.Roles)
            {
                var firstOrDefault = roles.FirstOrDefault(p => p.Id == identityUserRole.RoleId);
                if (firstOrDefault != null)
                    listRoles.Add(new BaseFormModel {Id = identityUserRole.RoleId , Name = firstOrDefault.Name});
            }

            ViewBag.id = "listAccess";

            return PartialView("_PartialDefaultTable", listRoles);
        }
    }
}