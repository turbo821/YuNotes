using YuNotes.Models;

namespace YuNotes.Repositories
{
    public interface IUsersReposiroty
    {
        bool CheckNickname(string nickname);
        Task SignUpUser(User user);
        
        Task<bool> LoginUser(string email, string password);
    }
}
