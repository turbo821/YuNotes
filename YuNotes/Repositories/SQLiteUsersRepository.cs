using Microsoft.EntityFrameworkCore;
using YuNotes.Auth;
using YuNotes.Data;
using YuNotes.Models;
using YuNotes.Repositories.Interfaces;

namespace YuNotes.Repositories
{
    public class SQLiteUsersRepository : IUsersReposiroty
    {
        NotesContext db;

        public SQLiteUsersRepository(NotesContext context)
        {
            db = context;
        }

        public bool RetryNickname(string nickname)
        {
            return db.Users.Any(u => u.Nickname == nickname);
        }
        public async Task SignUpUser(User user)
        {
            db.Users.Add(user);

            await db.SaveChangesAsync();
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && user.Password == password.Encrypt())
            {
                return true;
            }
            return false;
        }
    }
}
