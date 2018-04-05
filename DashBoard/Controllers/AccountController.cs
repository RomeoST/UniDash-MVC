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
using Ninject;

namespace DashBoard.Controllers
{
    [Authorize, Permission]
    public class AccountController : Controller
    {
        [Inject]
        public IUserService UserService { get; set; }
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

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
        [HttpPost, AllowAnonymous, ValidateAjax, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginFormModel model)
        {
            var claim = await UserService.Authenticate(model.UserName, model.Password);

            if (claim == null)
                return Json(new {model = "failed", modelList = new[] {"Невірний логін або пароль"}},
                    JsonRequestBehavior.AllowGet);

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties{IsPersistent = true}, claim);

            return Json(new { model = "confirmed", href = "/dashboard" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AllowAnonymous, ValidateAjax, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterFormModel model)
        {
            var userDto = Mapper.Map<RegisterFormModel, DutUser>(model);
            var details = await UserService.Create(userDto, model.Password);
            if (!details.Successed)
                return Json(new {model = "failed", modelList = new[] {details.Message}}, JsonRequestBehavior.AllowGet);

            var claim = await Task.Run(() => UserService.Authenticate(model.UserName, model.Password));
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true}, claim);

            return Json(new {model = "confirmed", href = "/dashboard/welcome"}, JsonRequestBehavior.AllowGet);
        }

		[HttpGet]
        public ActionResult Logout()
		{
            AuthenticationManager.SignOut();
			return Redirect("Login");
		}

        [HttpPost, ValidateAjax, ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(EditUserFormModel model)
        {
            var newData = Mapper.Map<EditUserFormModel, DutUser>(model);
            var result = await Task.Run((() => UserService.EditProfile(newData)));

            return Json(new {model = result.Successed?"confirmed":"failed", message=result.Message}, JsonRequestBehavior.AllowGet);
        }
    }
}