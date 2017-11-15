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
        [RegularExpression("\\d{0,5} ", ErrorMessage = "Не вірний формат відправлення спеціальностей до серверу")]
        public string Speciality { get; set; }
        public string MarkResult { get; set; }
    }
}