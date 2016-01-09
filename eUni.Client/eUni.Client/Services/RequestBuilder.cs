using System.Web.Configuration;

namespace EUni_Client.Services
{
    public static class RequestBuilder
    {
        public static string Build(string action)
        {
            return WebConfigurationManager.AppSettings["WebServicesUrl"] + "api" + action;
        }
    }
}