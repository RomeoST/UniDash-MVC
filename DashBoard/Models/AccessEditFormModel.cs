using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DashBoard.Models
{
    public class AccessEditFormModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public List<string> UserRolesList { get; set; } 
        public List<string> Roles { get; set; }
    }
}