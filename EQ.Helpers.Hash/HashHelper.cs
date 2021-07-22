using System.Security.Cryptography;
using System.Text;

namespace EQ.Helpers.Hash
{
    public static class HashHelper
    {
        public static string GetPasswordHash(string password)
        {
            var hash = string.Empty;

            using (SHA512 shaM = new SHA512Managed())
            {
                var passBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = shaM.ComputeHash(passBytes);

                var hashedInputStringBuilder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                hash = hashedInputStringBuilder.ToString();
            }

            return hash;
        }
    }
}
