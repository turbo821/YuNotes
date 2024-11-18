using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security;
using YuNotes.Services.Interfaces;

namespace YuNotes.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Введите ник")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина от 3 до 100 символов")]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите почту")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Длина от 4 до 100 символов")]
        [EmailAddress(ErrorMessage = "Адрес должен содержать символ @")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Длина от 4 до 100 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль повторно")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Длина от 6 до 100 символов")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
