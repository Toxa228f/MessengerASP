using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RegexCheckLibrary.MyRegex;

namespace Letter.Domain.ViewModels
{
    public class NewPrivateChatViewModel
    { 
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
                if (IsStringEmail(value))
                {
                    email = value;
                }
            }
        }
    }
}
