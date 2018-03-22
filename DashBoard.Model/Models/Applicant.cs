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
    /// Класс таблиці роботи з абітурієнтами
    /// </summary>
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }
        [ForeignKey("DutUser")]
        public string UserId { get; set; }
        public string NameFound { get; set; }
        public string NameApplicant { get; set; }
        public string MailApplicant { get; set; }
        public string PhoneApplicant { get; set; }
        public string SchoolCollege { get; set; }
        public string Address { get; set; }
        public string Speciality { get; set; }
        public string MarkResult { get; set; }
        public DateTime DateEdit { get; set; }
        public DateTime DateAdd { get; set; }

        public virtual DutUser DutUser { get; set; }

        public Applicant() => DateAdd = DateTime.Now;
    }
}
