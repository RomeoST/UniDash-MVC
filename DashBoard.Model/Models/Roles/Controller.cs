using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Model.Models
{
    public class TController
    {
        [Key]
        public int ControllerId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TAction> Actions { get; set; }

        public TController() => Actions = new List<TAction>();
    }
}
