using System.Web;

namespace EUni_Client.Services
{
    public static class Extensions
    {
        public static ApiService GetApiService(this HttpSessionStateBase session)
        {
            return session[ServiceNames.ApiService] as ApiService;
        }
    }
}