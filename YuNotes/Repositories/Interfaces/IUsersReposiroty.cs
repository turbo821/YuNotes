using YuNotes.Models;

namespace YuNotes.Repositories.Interfaces
{
    public interface IUsersReposiroty
    {
        bool RetryNickname(string nickname);
        bool RetryEmail(string email);
        Task SignUpUser(User user);

        Task<bool> LoginUser(string email, string password);
    }
}
