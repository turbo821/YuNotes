using System.ComponentModel.DataAnnotations;

namespace YuNotes.ViewModels
{
    public class PasswordRecoveryViewModel
    {
        public string Email { get; init; } = null!;
        public string Code { get; init; } = null!;
        [Required(ErrorMessage = "Введите код")]
        public string InputCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Длина от 4 до 100 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль повторно")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Длина от 6 до 100 символов")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
