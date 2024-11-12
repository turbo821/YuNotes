using System.Security.Cryptography;
using System.Text;

namespace YuNotes.Auth
{
    public static class PassowdMd5StringExtension
    {
        public static string Encrypt(this string str)
        {
            MD5 md5 = MD5.Create();
            byte[] hashValue;
            hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            string hash = Convert.ToBase64String(hashValue);
            return hash;
        }
    }
}
