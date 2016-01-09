using System.Collections.Generic;
using System.Web.Http;
using eUni.WebServices.Helpers;

namespace eUni.WebServices.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
//        [WebServices.Authorize("student")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
//        [WebServices.Authorize("teacher")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public string GetFromToken([FromBody]string token, [FromBody]string identifier)
        {
            return TokenHelper.GetFromToken(token, identifier);
        }
    }
}
