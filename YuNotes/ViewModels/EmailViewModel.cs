using System.ComponentModel.DataAnnotations;

namespace YuNotes.ViewModels
{
    public class EmailViewModel
    {
        [Required(ErrorMessage = "Введите почту")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Длина от 4 до 100 символов")]
        [EmailAddress(ErrorMessage = "Адрес должен содержать символ @")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
