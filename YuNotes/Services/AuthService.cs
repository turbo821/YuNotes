using System.Security.Claims;
using YuNotes.Auth;
using YuNotes.Models;
using YuNotes.Repositories.Interfaces;
using YuNotes.Services.Interfaces;
using YuNotes.ViewModels;

namespace YuNotes.Services
{
    public class AuthService : IAuthService
    {
        private IUsersReposiroty _repo;

        public AuthService(IUsersReposiroty repo) 
        {
            _repo = repo;
        }

        public async Task<bool> CheckLogin(LogInViewModel request)
        {
            return request.Email != null
                && request.Password != null
                && await _repo.LoginUser(request.Email, request.Password);
        }

        public ClaimsIdentity GetClaimsIdentity(LogInViewModel request)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, request.Email) };

            return new ClaimsIdentity(claims, "Cookies");
        }

        public bool NicknameIsRetryOrNull(string nickname)
        {
            return _repo.RetryNickname(nickname) || nickname == null;
        }

        public async Task SignUpUser(SignInViewModel data)
        {
            List<NoteGroup> defaultGroups = new List<NoteGroup>()
            {
                new NoteGroup{ Id = Guid.NewGuid(), Name = "Work" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Life" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Personal" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Travel" }
            };

            User user = new User 
            {
                Nickname = data.Nickname!, 
                Email = data.Email, 
                Password = data.Password.Encrypt(),
                Groups = defaultGroups
            };
            await _repo.SignUpUser(user);
        }
    }
}
