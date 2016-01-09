using System.Collections.Generic;
using System.Web.Http;
using eUni.WebServices.Helpers;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        public string GetFromToken([FromBody]TokenIdentifierViewModel tokenIdentifierViewModel)
        {
            return TokenHelper.GetFromToken(tokenIdentifierViewModel.Token, tokenIdentifierViewModel.Identifier);
        }

        public class TokenIdentifierViewModel
        {
            public string Token { get; set; }
            public string Identifier { get; set; }
        }
    }
}
