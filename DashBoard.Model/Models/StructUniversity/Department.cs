using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Model.Models
{
    public enum TypeUStruct
    {
        eInstitute,
        eFaculty
    }

    /// <summary>
    /// Клас таблиці спіальності
    /// </summary>
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("Faculty")]
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        [ForeignKey("Institute")]
        public int? InstituteId { get; set; }
        public virtual Institute Institute { get; set; }

        public bool? isAdmission { get; set; }
    }
}