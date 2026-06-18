using System;
using System.Security.Cryptography;
using System.Text;

namespace HYMAPSOPIR
{
    public static class SecurityHelper
    {
        public static string HashSHA256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // KISS
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}