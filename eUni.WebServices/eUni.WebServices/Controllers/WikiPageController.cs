using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Helpers;
using eUni.WebServices.Models;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/WikiPage")]
    public class WikiPageController : ApiController
    {
        private IUserProvider _userProvider;
        private IWikiPageProvider _wikiPageProvider;

        public WikiPageController(IUserProvider userProvider, IWikiPageProvider wikiPageProvider)
        {
            _userProvider = userProvider;
            _wikiPageProvider = wikiPageProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(WikiPageViewModel wikiPageViewModel)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            WikiPageDTO dtoWikiPage = Mapper.Map<WikiPageDTO>(wikiPageViewModel);

            dtoWikiPage.Module = new ModuleDTO
            {
                ModuleId = wikiPageViewModel.ModuleId
            };

            _wikiPageProvider.CreateWikiPage(dtoWikiPage);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Wiki Page created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(int wikiId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
           _wikiPageProvider.DeleteWikiPageWithId(wikiId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Wiki Page deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

    }
}
