using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using eUni.BusinessLogic.IProviders;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        private IModuleProvider _moduleProvider;
        //private iquestio
    }
}