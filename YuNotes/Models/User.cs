using System.ComponentModel.DataAnnotations;

namespace YuNotes.Models
{
    public class User : BaseModel
    {
        public string Nickname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public ICollection<Note>? Notes { get; set; }
        public ICollection<NoteGroup>? Groups { get; set; }
    }
}
