using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUni.WebServices.Helpers
{
    public static class LoggerHelper
    {
        public static string GetActionString(string username, string action)
        {
            return String.Format("User: {0}, Action: {1}, Date: {2}", username, action, DateTime.Now);
        }
    }
}