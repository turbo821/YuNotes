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

        public async Task<bool> CheckLogin(string email, string password)
        {
            return email != null
                && password != null
                && await _repo.LoginUser(email, password);
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
        public bool EmailIsRetryOrNull(string email)
        {
            return _repo.RetryEmail(email) || email == null;
        }
        public async Task SignUpUser(SignInViewModel data)
        {
            List<NoteGroup> defaultGroups = new List<NoteGroup>()
            {
                new NoteGroup{ Id = Guid.NewGuid(), Name = "Личные" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Работа" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Путешествия" },
                new NoteGroup { Id = Guid.NewGuid(), Name = "Жизнь" },
                new NoteGroup{ Id = Guid.NewGuid(), Name = "Без категории" }
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
        public async Task UpdatePassword(string email, string password)
        {
            await _repo.UpdatePassword(email, password.Encrypt());
        }
    }
}
