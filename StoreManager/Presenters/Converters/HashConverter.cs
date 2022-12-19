using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace StoreManager.Presenters.Converters
{
    public static class HashConverter
    {
        public static string GetMD5Hash(string value)
        {
            var result = string.Empty;
            var Algorithm = MD5.Create();
            var MD5HashBytes = Algorithm.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));
            for (int i = 0; i < 16; i += 2) result += MD5HashBytes[i].ToString("x2");
            return result;
        }
    }
}
