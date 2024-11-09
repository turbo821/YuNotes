using System.ComponentModel.DataAnnotations;

namespace YuNotes.Models
{
    public class User : BaseModel
    {
        public string? Nickname { get; set; }
        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
