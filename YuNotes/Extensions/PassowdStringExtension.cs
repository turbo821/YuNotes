using System.Security.Cryptography;
using System.Text;

namespace FromboardDelivery.Extensions
{
    public static class PassowdStringExtension
    {
        public static string Encrypt(this string str)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashValue;
            hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));

            string hash = Convert.ToBase64String(hashValue);
            return hash;
        }
    }
}
