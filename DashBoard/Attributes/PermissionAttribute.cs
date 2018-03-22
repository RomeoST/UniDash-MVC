using System.Web.Mvc;
using DashBoard.BLL.Interfaces;

namespace DashBoard.Attributes
{
    public class PermissionAttribute : FilterAttribute { }

    /// <summary>
    /// Система прав доступу до методів
    /// </summary>
    public class PermissionFilter : IActionFilter
    {
        private readonly IUserService UserService;

        public PermissionFilter(IUserService userService) => UserService = userService;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requiredPermission = $"{filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}-{filterContext.ActionDescriptor.ActionName}";
            var userName = filterContext.HttpContext.User.Identity.Name;

            if (UserService.HasPermission(userName, requiredPermission)) return;

            if(filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new JsonResult()
                {
                    Data = new {model="failed", message="Ви не маєте доступу до цієї функції!"},
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            else
                filterContext.Result = new HttpNotFoundResult();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}