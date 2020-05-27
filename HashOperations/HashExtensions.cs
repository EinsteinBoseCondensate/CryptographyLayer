using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyLayer.HashOperations
{
    public static class HashExtensions
    {
        public static string Hash(this string chars, string salt)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(salt)))
                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(chars)));
        }
    }
}
