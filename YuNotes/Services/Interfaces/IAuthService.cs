using System.Security.Claims;
using YuNotes.ViewModels;

namespace YuNotes.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> CheckLogin(string email, string password);
        ClaimsIdentity GetClaimsIdentity(LogInViewModel request);
        bool NicknameIsRetryOrNull(string nickname);
        bool EmailIsRetryOrNull(string email);
        Task SignUpUser(SignInViewModel data);
        Task UpdatePassword(string email, string password);
    }
}
