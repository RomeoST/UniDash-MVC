using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Model.Models
{
    /// <summary>
    /// Клас таблиці факультету
    /// </summary>
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        public Faculty() => Departments = new List<Department>();
    }
}
