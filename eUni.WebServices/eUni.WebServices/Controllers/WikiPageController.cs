using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Helpers;
using eUni.WebServices.Models;
using eUni.WebServices.Models.OutputModels;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/WikiPage")]
    public class WikiPageController : ApiController
    {
        private IWikiPageProvider _wikiPageProvider;

        public WikiPageController(IWikiPageProvider wikiPageProvider)
        {
            _wikiPageProvider = wikiPageProvider;
        }

        [Route("GetAllWikiPagesByModule")]
        public async Task<IHttpActionResult> GetAllByModule(int? moduleId)
        {

            if (moduleId == null)
            {
                return BadRequest("Module Id not found");
            }

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var fileDtos = _wikiPageProvider.GetByModule(moduleId.Value);
            var fileOutModels = Mapper.Map<List<WikiPageOutModel>>(fileDtos);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Wiki Pages for Module "));
            return Content(HttpStatusCode.OK, fileOutModels);
        }

        [Route("Add")]
        [@Authorize("teacher")]
        [@Authorize("admin")]
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
        [@Authorize("teacher")]
        public async Task<IHttpActionResult> Remove(int wikiId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
           _wikiPageProvider.DeleteWikiPageWithId(wikiId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Wiki Page deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

    }
}
