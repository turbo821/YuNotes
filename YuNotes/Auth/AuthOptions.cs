using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace YuNotes.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "YuAuthServer";
        public const string AUDIENCE = "YuNotes";
        const string KEY = "ytrewq_Password_129_generatekey!!321";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
