using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace eUni.WebServices.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateToken(string username, string role)
        {
            return Convert.ToBase64String(GetBytes($"username-{username},role-{role},datetime-{DateTime.Now}"));
        }

        public static string GetFromToken(string token, string identifier)
        {
            var bytes = Convert.FromBase64String(token);

            string result = GetString(bytes);

            return (from pair in result.Split(',') where pair.Contains(identifier) select pair.Split('-')[1]).FirstOrDefault();
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}