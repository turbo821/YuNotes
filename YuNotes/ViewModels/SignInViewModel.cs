using System.ComponentModel.DataAnnotations;
using System.Security;

namespace YuNotes.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина от 3 до 30 символов")]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string? PasswordConfirm { get; set; }
    }
}
