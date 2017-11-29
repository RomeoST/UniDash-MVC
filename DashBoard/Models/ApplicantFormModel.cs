using System;
using System.ComponentModel.DataAnnotations;

namespace DashBoard.Models
{
    public class ApplicantFormModel
    {
        public int? ApplicantId { get; set; }
        public string NameFound { get; set; }
        [Required]
        public string NameApplicant { get; set; }
        public string MailApplicant { get; set; }
        [Required]
        public string PhoneApplicant { get; set; }
        [Required]
        public string SchoolCollege { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Speciality { get; set; }
        public string MarkResult { get; set; }

        public string DateAdd { get; set; }
        public string DateEdit { get; set; }
    }
}