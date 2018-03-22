using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Model.Models
{
    public class TAction
    {
        [Key]
        public int ActionId { get; set; }
        [ForeignKey("Controller")]
        public int? ControllerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual TController Controller { get; set; }
    }
}
