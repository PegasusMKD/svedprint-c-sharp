using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Utility
{
    public static class Encryption
    {
        public static string EncryptSHA256(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return string.Join("", bytes.ToList().Select(b => b.ToString("x2")));
            }
        }
    }
}
