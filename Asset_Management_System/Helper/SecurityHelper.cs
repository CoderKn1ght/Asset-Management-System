using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Asset_Management_System.Helper
{
    public class SecurityHelper
    {
        public static string HashPassword(string password, ref string salt)
        {
            salt = "ASS3TM4N4G3M3NT!";
            var passwordByte = Encoding.Unicode.GetBytes(salt + password);
            var hashedBytes = SHA512.Create().ComputeHash(passwordByte);
            var hashedPassword = Convert.ToBase64String(hashedBytes);
            return hashedPassword;
        }

        /// <summary>
        /// Computes Hash 
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ComputeHash(string messageKey, string message)
        {
            var key = Encoding.UTF8.GetBytes(messageKey.ToUpper());
            string hashString;

            using (var hmac = new HMACSHA512(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString;
        }
    }
}