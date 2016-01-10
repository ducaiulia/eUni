using System.IO;
using System.Web;

namespace EUni_Client.Services
{
    public static class Extensions
    {
        public static ApiService GetApiService(this HttpSessionStateBase session)
        {
            return session[ServiceNames.ApiService] as ApiService;
        }

        public static byte[] ToByteArray(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}