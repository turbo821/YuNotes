using System.Security.Claims;
using YuNotes.ViewModels;

namespace YuNotes.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> CheckLogin(LogInViewModel request);
        ClaimsIdentity GetClaimsIdentity(LogInViewModel request);
        bool NicknameIsRetryOrNull(string nickname);
        bool EmailIsRetryOrNull(string email);
        Task SignUpUser(SignInViewModel data);
    }
}
