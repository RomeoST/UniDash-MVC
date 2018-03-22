using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Model.Models
{
    public class PermissionRoles
    {
        [Key]
        public int PermissionId { get; set; }
        [ForeignKey("DutRole")]
        public string RoleId { get; set; }
        [ForeignKey("Controller")]
        public int? ControllerId { get; set; }
        [ForeignKey("Action")]
        public int? ActionId { get; set; }

        public virtual DutRole DutRole { get; set; }
        public virtual TController Controller { get; set; }
        public virtual TAction Action { get; set; }
    }
}
