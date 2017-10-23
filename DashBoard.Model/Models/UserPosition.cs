using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Model.Models
{
    public class UserPosition
    {
        [Key]
        [ForeignKey("DutUser")]
        public int UserId { get; set; }

        public int InstituteId { get; set; }
        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
    }
}
