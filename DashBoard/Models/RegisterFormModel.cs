using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace DashBoard.Models
{
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [EmailAddress(ErrorMessage = "Не коректний Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [RegularExpression("^[0,1]", ErrorMessage = "Не обраний тип")]
        public string Type { get; set; }
    }
}