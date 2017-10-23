using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Model.Models
{
    /// <summary>
    /// Клас таблиці роботи з подачою документів від абітурієнтів
    /// </summary>
    public class SubmissionDoc
    {
        [Key]
        public int Id { get; set; }
        [StringLength(128)]
        [ForeignKey("DutUser")]
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int Zno1 { get; set; }
        public int Zno2 { get; set; }
        public int Zno3 { get; set; }
        public int Sertificate { get; set; }
        public int Ball { get; set; }
        public bool Privilege { get; set; }
        public string Speciality { get; set; }
        public float Sum { get; set; }

        public virtual DutUser DutUser { get; set; }
    }
}
