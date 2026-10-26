using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Letter.Models
{
    public class ChatModel
    {
        [Required(ErrorMessage = "Недопустимое название чата")]
        [MaxLength(32, ErrorMessage = "Слишком длинное название чата")]
        [MinLength(2, ErrorMessage = "Слишком короткое название чата")]
        public string Name { get; set; }
        [MaxLength(128,ErrorMessage = "Слишком длинное описание чата")]
        public string Description { get; set; }
    }
}
