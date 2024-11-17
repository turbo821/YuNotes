using YuNotes.Models;

namespace YuNotes.Repositories.Interfaces
{
    public interface IUsersReposiroty
    {
        bool RetryNickname(string nickname);
        Task SignUpUser(User user);

        Task<bool> LoginUser(string email, string password);
    }
}
