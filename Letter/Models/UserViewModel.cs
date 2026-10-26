using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System;
using RegexCheckLibrary;

namespace Letter.Models
{
    public class UserViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Неверный формат логина")]
        [MaxLength(16,ErrorMessage = "Слишком длинное имя")]
        [MinLength(3, ErrorMessage = "Слишком короткое имя")]
        public string Name { get; set; }


        private string email;
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Неверный формат email")]
        [MaxLength(50, ErrorMessage = "Слишком длинный Email")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (MyRegex.IsStringEmail(value))
                {
                    email = value;
                }
            }
        }

        [Display(Name = "Местоположение")]
        [Required(ErrorMessage = "Неверный формат локации")]
        [MaxLength(128, ErrorMessage = "Слишком длинное описание локации")]
        [MinLength(5, ErrorMessage = "Слишком короткое описание локации")]
        public string Location { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Неверный формат даты")]
        public DateTime BornDate { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Неверный формат пароля")]
        [MaxLength(32, ErrorMessage = "Слишком длинный пароль")]
        [MinLength(5, ErrorMessage = "Слишком короткий пароль")]
        public string Password { get; set; }

        [Display(Name = "Повторный пароль")]
        [Required(ErrorMessage = "Пароли не совпадают")]
        [MaxLength(32, ErrorMessage = "Слишком длинный пароль")]
        [MinLength(5, ErrorMessage = "Слишком короткий пароль")]
        public string SecondPass { get; set; }
    }
}
