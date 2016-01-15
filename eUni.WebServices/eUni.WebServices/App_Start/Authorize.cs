using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using eUni.WebServices.Helpers;
using Microsoft.Ajax.Utilities;

namespace eUni.WebServices
{
    public class Authorize : AuthorizationFilterAttribute
    {
        public string Role { get; set; }

        public Authorize(string role)
        {
            Role = role;
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            string token = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();

            if (token.IsNullOrWhiteSpace())
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Not allowed.");
            }

            string role = TokenHelper.GetFromToken(token, Role);

            if(role.IsNullOrWhiteSpace() || !role.Equals(Role))
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Not allowed.");

            //string resDate = TokenHelper.GetFromToken(token, "datetime");

            //DateTime date = DateTime.Parse(TokenHelper.GetFromToken(token, "datetime"));

            //if ((DateTime.Now - date).Hours > 2)
            //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Not allowed. Token Expired.");
            
            //User is Authorized, complete execution
            return Task.FromResult<object>(null);

        }
    }
}