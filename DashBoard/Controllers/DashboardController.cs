using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using DashBoard.Model.Models;
using DashBoard.Models;
using Microsoft.AspNet.Identity.Owin;

namespace DashBoard.Controllers
{
    //[LoggedOrAuth]
    [Authorize]
    public class DashboardController : Controller
    {
        private IUserService UserService;

        public DashboardController(IUserService userService)
        {
            UserService = userService;
        }

        private async Task<ActionResult> InitDefaultViewBag(string actionOut)
        {
            var user = await Task.Run(()=>UserService.FindByName(User.Identity.Name));
            if (user == null)
                return RedirectToAction("Login", "Account");

            Session["UserFullName"] = user.ClientProfile.FullName;
            return View(actionOut);
        }

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            return await InitDefaultViewBag("Index");
        }

        public async Task<ActionResult> Welcome()
        {
            return await InitDefaultViewBag("Welcome");
        }

        [HttpGet]
        public async Task<ActionResult> UserList()
        {
            var users = await UserService.GetAll();
            var model = users.Select(Mapper.Map<ClientProfile, UsersViewModel>).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var user = await UserService.FindByName(User.Identity.Name);
            if (user == null)
                return RedirectToAction("Login", "Account");
            var model = Mapper.Map<DutUser, EditUserFormModel>(user);
            return View(model);
        }
    }
}