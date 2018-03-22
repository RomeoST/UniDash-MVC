using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.Model.Models
{
    public class DutRole : IdentityRole
    {
        public DutRole() { }
        public string Description { get; set; }
    }
}
