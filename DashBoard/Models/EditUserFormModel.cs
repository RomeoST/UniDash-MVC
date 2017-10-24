using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DashBoard.Models
{
    public class EditUserFormModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Не вірно вказага пошта")]
        public string Email { get; set; }
    }
}