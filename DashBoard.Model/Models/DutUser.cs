using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.Model.Models
{
    public class DutUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
