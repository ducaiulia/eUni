using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace EUni_Client.Services
{
    public class ApiService
    {
        #region Constructor

        public ApiService(string token, string username, UserRole userRole)
        {
            authToken = token;
            Username = username;
            UserRole = userRole;
        }

        #endregion

        #region Methods

        public async Task<T> GetAsync<T>(string action)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.GetAsync(RequestBuilder.Build(action));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task<TU> GetAsync<TU,T>(string action, string objectName, T data)
        {
            var builder = new UriBuilder(RequestBuilder.Build(action));
            var query = HttpUtility.ParseQueryString(builder.Query);
            query[objectName] = JsonConvert.SerializeObject(data);
            builder.Query = query.ToString();
            var url = builder.ToString();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.GetAsync(url);

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TU>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task<TU> GetAsync<TU, T>(string action, Dictionary<string, object> parameters)
        {
            var builder = new UriBuilder(RequestBuilder.Build(action));
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var parameter in parameters)
            {
                query[parameter.Key] = JsonConvert.SerializeObject(parameter.Value);
            }
            builder.Query = query.ToString();
            var url = builder.ToString();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.GetAsync(url);

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TU>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task DeleteAsync(string action, int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.DeleteAsync(RequestBuilder.Build(action + "/" + id));
                if (!result.IsSuccessStatusCode)
                {
                    throw new ApiException(result.StatusCode, result.Content.ToString());
                }
            }
        }

        public async Task DeleteAsync<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var serializedData = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializedData);
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                var request = new HttpRequestMessage(HttpMethod.Delete, RequestBuilder.Build(action))
                {
                    Content = content
                };
                var result = await client.SendAsync(request);
                if (!result.IsSuccessStatusCode)
                {
                    throw new ApiException(result.StatusCode, result.Content.ToString());
                }
            }
        }

        public async Task PutAsync<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.PutAsync(RequestBuilder.Build(action), data, typeFormatter);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task<T> PutAsyncWithReturn<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.PutAsync(RequestBuilder.Build(action), data, typeFormatter);
                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<T>(json);
                    return res;
                }

                var error = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, error);
            }
        }

        public async Task PostAsync<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.PostAsync(RequestBuilder.Build(action), data, typeFormatter);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task<T> PostAsyncWithReturn<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.PostAsync(RequestBuilder.Build(action), data, typeFormatter);
                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                //string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task<TU> PostAsyncWithReturn<TU, T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authToken);
                var result = await client.PostAsync(RequestBuilder.Build(action), data, typeFormatter);
                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TU>(json);
                }

                //string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }

        #endregion Methods

        #region Properties

        public UserRole UserRole { get; private set; }
        public string Username { get; private set; }

        #endregion Properties

        #region Fields

        private readonly string authToken;
        private readonly JsonMediaTypeFormatter typeFormatter = new JsonMediaTypeFormatter { SerializerSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore } };

        #endregion Fields
    }

    #region Helpers

    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, string jsonData)
        {
            StatusCode = statusCode;
            JsonData = jsonData;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public string JsonData { get; private set; }
    }

    #endregion Helpers
}

