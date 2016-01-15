using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EUni_Client.Services
{
    public static class LoginService
    {
        public static async Task<ApiService> Login(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(RequestBuilder.Build("/Account/Login"),
                    new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Email", email),
                        new KeyValuePair<string, string>("Password", password)
                    }));

                if (!result.IsSuccessStatusCode)
                {
                    throw new ApiException(result.StatusCode, result.Content.ToString());
                }

                var token = await result.Content.ReadAsStringAsync();
                token = token.Substring(1, token.Length - 2);
                var role = await GetRole(token);
                return new ApiService(token, email, role);
            }
        }

        private static async Task<UserRole> GetRole(string token)
        {
            using (var client = new HttpClient())
            {
                var tokenRequest = new TokenIdentifierViewModel
                {
                    Token = token,
                    Identifier = "role"
                };

                var result = await client.PostAsync(RequestBuilder.Build("/Values"), tokenRequest, TypeFormatter);

                if (!result.IsSuccessStatusCode)
                {
                    throw new ApiException(result.StatusCode, result.Content.ToString());
                }

                var role = await result.Content.ReadAsStringAsync();
                role = role.Replace("\"", String.Empty);
                if (role.Equals("Student", StringComparison.InvariantCultureIgnoreCase))
                {
                    return UserRole.Student;
                }
                if (role.Equals("Teacher", StringComparison.InvariantCultureIgnoreCase))
                {
                    return UserRole.Teacher;
                }
                if (role.Equals("Admin", StringComparison.InvariantCultureIgnoreCase))
                {
                    return UserRole.Admin;
                }
                return UserRole.Student;
            }
        }

        public static async Task<bool> Register(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(RequestBuilder.Build("/Account/Register"),
                    new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Email", email),
                        new KeyValuePair<string, string>("Password", password),
                        new KeyValuePair<string, string>("ConfirmPassword", password)
                    }));

                if (!result.IsSuccessStatusCode)
                {
                    throw new ApiException(result.StatusCode, result.Content.ToString());
                }

                //var jsonTokenRespone = await result.Content.ReadAsStringAsync();
                //var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonTokenRespone);
                return true;
            }
        }

        private static readonly JsonMediaTypeFormatter TypeFormatter = new JsonMediaTypeFormatter { SerializerSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore } };
    }

    public class TokenIdentifierViewModel
    {
        public string Token { get; set; }
        public string Identifier { get; set; }
    }

    public enum UserRole { Student, Teacher, Admin }
}