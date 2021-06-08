using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public static class HashingHelper
    {
        public static string GenerateBase64String()
        {
            byte[] buffer = new byte[64];
            new Random().NextBytes(buffer);
            return System.Convert.ToBase64String(buffer);
        }
        public static void GeneratedHash(out byte[] generatedHash,out byte[] generatedSalt,string data)
        {
            using(var hmac = new HMACSHA512())
            {
                generatedSalt = hmac.Key;
                generatedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        public static bool ValidateHash(byte[] passwordHash,byte[] passwordSalt,string password)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var generatedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for(int x = 0; x < generatedHash.Length; x++)
                {
                    if (generatedHash[x] != passwordHash[x])
                        return false;
                }
            }
            return true;
        }
    }
}
