using System.Security.Claims;
using YuNotes.ViewModels;

namespace YuNotes.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> CheckLogin(LogInViewModel request);
        ClaimsIdentity GetClaimsIdentity(LogInViewModel request);
        bool NicknameIsRetryOrNull(string nickname);
        Task SignUpUser(SignInViewModel data);
    }
}
